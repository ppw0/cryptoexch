using Cryptoexch.Logic;
using Cryptoexch.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cryptoexch
{
    public partial class Portfolio : Page
    {
        private string clientId;
        private readonly ClientContext _db = new ClientContext();

        private string dest, src, _dest, _src;
        private string limit, amt, _limit, _amt;
        private string bs, ml, _bs, _ml;

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ClientId"] != null)
                clientId = Session["ClientId"].ToString();
            else
                clientId = User.Identity.GetUserId();

            if (!Page.IsPostBack)
            {
                // initialize state
                ViewState["dest"] = "BTC";
                ViewState["src"] = "EUR";
                ViewState["limit"] = "";
                ViewState["amt"] = "";
                ViewState["bs"] = "buy";
                ViewState["ml"] = "market";
                ViewState["inv"] = false; // invert exchange rate
                ViewState["offset"] = 0;
                ViewState["exp"] = 0;
                ViewState["id"] = 3;
                ViewState["total"] = "";
            }
            else
            {
                #region PostBack

                // read stored currency pair settings
                var destSrc = new HashSet<string>();
                dest = ViewState["dest"].ToString();
                src = ViewState["src"].ToString();
                destSrc.Add(dest);
                destSrc.Add(src);

                // read currency pair selections
                var _destSrc = new HashSet<string>();
                _dest = ddlPair.SelectedItem.Text.Substring(0, 3);
                _src = ddlPair.SelectedItem.Text.Substring(4);
                _destSrc.Add(_dest);
                _destSrc.Add(_src);

                // read togglebutton selections
                bs = ViewState["bs"].ToString();
                ml = ViewState["ml"].ToString();
                _bs = bs;
                _ml = ml;
                Control c = GetPostBackControl(this.Page);
                if (c is RadioButton)
                {
                    RadioButton rb = (RadioButton)c;
                    switch (rb.ID)
                    {
                        case "rbBuy": ViewState["bs"] = _bs = "buy"; break;
                        case "rbSell": ViewState["bs"] = _bs = "sell"; break;
                        case "rbMarket": ViewState["ml"] = _ml = "market"; break;
                        case "rbLimit": ViewState["ml"] = _ml = "limit"; break;
                        default: break;
                    }
                }

                // read text input
                limit = ViewState["limit"].ToString();
                amt = ViewState["amt"].ToString();
                _limit = tbLimit.Text;
                _amt = tbAmount.Text;

                if (!destSrc.SetEquals(_destSrc))
                { // check if user switched to a different currency pair
                    // change currency pair and reset state
                    ViewState["dest"] = _dest;
                    ViewState["src"] = _src;
                    ViewState["limit"] = tbLimit.Text = "";
                    ViewState["amt"] = tbAmount.Text = "";
                    ViewState["bs"] = "buy";
                    ViewState["ml"] = "market";
                    ViewState["inv"] = false;
                    ViewState["offset"] = 0;
                    ViewState["exp"] = 0;
                    ViewState["id"] = 3;
                    ViewState["total"] = "";
                }
                else if (limit != _limit || amt != _amt)
                { // different numbers
                    //c = GetPostBackControl(this.Page);
                    if (c is TextBox)
                    {
                        TextBox t = (TextBox)c;
                        switch (t.ID)
                        {
                            case "tbLimit": ViewState["limit"] = _limit; break;
                            case "tbAmount": ViewState["amt"] = _amt; break;
                            default: break;
                        }
                    }
                }
                else if (bs == _bs && ml == _ml && !(c is Button))
                {
                    // switch currencies only if the user has changed no other properties of the ViewState: hasn't toggled any buttons;
                    // hasn't selected a different currency pair; hasn't changed the contents of the tbLimit and tbAmount fields.
                    ViewState["dest"] = src;
                    ViewState["src"] = dest;
                    ViewState["inv"] = !(bool)ViewState["inv"];
                }

                #endregion PostBack
            }
        }

        #endregion Page_Load

        #region ControlPostBack

        protected void DdlPair_Load(object sender, EventArgs e)
        {
            // set exchange rate
            int id;
            ViewState["id"] = id = (Convert.ToInt32(ddlPair.SelectedItem.Value));
            ViewState["rate"] = (from cp in _db.CurrencyPairs
                                 where cp.ID == id
                                 select cp.Last).ToList().SingleOrDefault();

            if (rbMarket.Checked)
            {
                ViewState["limit"] = (bool)ViewState["inv"] ? (1 / Convert.ToDouble(ViewState["rate"])).ToString() : ViewState["rate"];
                tbLimit.Text = ViewState["limit"].ToString();
                tbLimit.Enabled = false;
            }
            else
            {
                tbLimit.Enabled = true;
                ViewState["limit"] = tbLimit.Text;
            }
        }

        protected void DdlCurrency_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                var item = ddlCurrency.Items[i];
                item.Enabled = ((item.Selected = item.Text == ViewState["dest"].ToString()) || item.Text == ViewState["src"].ToString());
            }
        }

        protected void LblTotal_Load(object sender, EventArgs e)
        {
            string t = ViewState["src"].ToString();
            ViewState["sym"] = (from cur in _db.Currencies
                                where cur.Ticker == t
                                select cur.Symbol).ToList().SingleOrDefault();
            try
            {
                ViewState["total"] = Convert.ToDouble(ViewState["amt"]) * Convert.ToDouble(ViewState["limit"]);
                lblTotal.Text = ViewState["sym"].ToString() + ViewState["total"].ToString();
            }
            catch
            {
                ViewState["total"] = "";
                lblTotal.Text = "";
            }
        }

        protected void LblSourceCurrency_Load(object sender, EventArgs e) =>
            lblSourceCurrency.Text = ViewState["src"].ToString();

        protected void BtnSubmit_Load(object sender, EventArgs e) =>
            btnSubmit.Text = (rbBuy.Checked ? "Buy" : "Sell") + " " + ViewState["dest"].ToString() + (rbBuy.Checked ? " with " : " for ") + ViewState["src"].ToString();

        protected void Rb_Load(object sender, EventArgs e, RadioButton rb, string attr, string chk) =>
            rb.CssClass = "btn btn-primary toggle-button" + ((rb.Checked = (ViewState[attr].ToString() == chk)) ? " active" : "");

        protected void RbSell_Load(object sender, EventArgs e) =>
            Rb_Load(sender, e, rbSell, "bs", "sell");

        protected void RbBuy_Load(object sender, EventArgs e) =>
            Rb_Load(sender, e, rbBuy, "bs", "buy");

        protected void RbMarket_Load(object sender, EventArgs e) =>
            Rb_Load(sender, e, rbMarket, "ml", "market");

        protected void RbLimit_Load(object sender, EventArgs e) =>
            Rb_Load(sender, e, rbLimit, "ml", "limit");

        protected void DdlOffsetSelected(object sender, EventArgs e) =>
            ViewState["offset"] = ddlOffset.SelectedIndex;

        protected void DdlExpirationSelected(object sender, EventArgs e) =>
            ViewState["exp"] = ddlExpiration.SelectedIndex;

        #endregion ControlPostBack

        #region IQueryables

        public IQueryable<Transaction> LvTransactions_GetData()
        {
            IQueryable<Transaction> query = _db.Transactions;
            if (!string.IsNullOrEmpty(clientId))
            {
                query = query.Where(t => t.ClientID == clientId);
                return from t in query orderby t.ID descending select t;
            }
            return null;
        }

        public IQueryable<CurrencyPair> LvCurrencyPairs_GetData() =>
            from cp in _db.CurrencyPairs orderby cp.ID select cp;

        public IQueryable<ClientPortfolioView_Result> LvBalances_GetData() =>
            _db.ClientPortfolioView(clientId); // table-valued function

        public IQueryable<OrderView_Result> LvOrders_GetData()
        {
            var query = from t1 in _db.Orders
                        join t2 in _db.CurrencyPairs on
                        t1.CurrencyPairID equals t2.ID
                        where t1.ClientID == clientId
                        orderby t1.ID
                        select new
                        {
                            t1.ID,
                            CurrencyPair = t2.Heading,
                            t1.Beginning,
                            t1.Type,
                            t1.Amount,
                            t1.Total,
                            t1.Expiration
                        };
            var list = query.ToList();
            var result = new List<OrderView_Result>();
            foreach (var item in list)
            {
                result.Add(new OrderView_Result
                {
                    ID = item.ID,
                    CurrencyPair = item.CurrencyPair,
                    Beginning = item.Beginning.ToString("yyyy-MM-dd HH:mm"),
                    Type = item.Type,
                    Amount = item.Amount,
                    Total = item.Total,
                    Expiration = item.Expiration == null ? "none" : ((DateTimeOffset)item.Expiration).ToString("yyyy-MM-dd HH:mm")
                });
            }
            return result.AsQueryable();
        }

        #endregion IQueryables

        #region Helper Methods

        public static Control GetPostBackControl(Page page)
        {
            Control control = null;
            string ctrlname = page.Request.Params.Get("__EVENTTARGET");
            if (!String.IsNullOrEmpty(ctrlname))
                control = page.FindControl(ctrlname);
            else
                foreach (string ctl in page.Request.Form)
                {
                    Control c = page.FindControl(ctl);
                    if (c is Button)
                    {
                        control = c;
                        break;
                    }
                }
            return control;
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.Transfer("Error.aspx?handler=Page_Error%20-%20Portfolio", true);
        }

        #endregion Helper Methods

        #region OrderFormSubmit

        private DateTimeOffset orderBeginning;
        private DateTimeOffset? orderExpiration;

        protected void SubmitOrderForm(object sender, EventArgs e)
        {
            try
            {
                switch (Convert.ToInt32(ViewState["offset"]))
                {
                    case 0: orderBeginning = DateTime.Now + new TimeSpan(0); break;
                    case 1: orderBeginning = DateTime.Now + new TimeSpan(24, 0, 0); break;
                    case 2: orderBeginning = DateTime.Now + new TimeSpan(72, 0, 0); break;
                    default: break;
                }

                switch (Convert.ToInt32(ViewState["exp"]))
                {
                    case 0: orderExpiration = null; break;
                    case 1: orderExpiration = orderBeginning + new TimeSpan(7, 0, 0, 0); break;
                    case 2: orderExpiration = orderBeginning + new TimeSpan(30, 0, 0, 0); break;
                    default: break;
                }

                var order = new Order()
                {
                    ClientID = clientId,
                    CurrencyPairID = Convert.ToInt32(ViewState["id"].ToString()),
                    Beginning = orderBeginning,
                    Type = ViewState["bs"].ToString() + ViewState["ml"].ToString(), // {"buymarket" | "sellmarket" | "buylimit" | "selllimit" }
                    Amount = Convert.ToSingle(ViewState["amt"]),
                    Limit = Convert.ToSingle(ViewState["limit"]),
                    Total = Convert.ToSingle(ViewState["total"]),
                    Expiration = orderExpiration,
                    EndType = null
                };

                _db.Orders.Add(order);
                _db.SaveChanges();

                // success

                Response.Redirect("Portfolio.aspx", false);
            }
            catch (Exception ex)
            {
                ExceptionUtility.LogException(ex, "SubmitOrderForm in Portfolio.aspx.cs");
                Response.Redirect("Error.aspx?handler=SubmitOrderForm", false);
            }
        }

        #endregion OrderFormSubmit
    }
}