using Cryptoexch.Models;
using System;
using System.Linq;
using System.Web.UI;

namespace Cryptoexch
{
    public partial class ChartsControl : UserControl
    {
        private int cpSelectedID = 5;
        private readonly ClientContext _db = new ClientContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (ddlCurrencyPairs.SelectedItem != null)
                cpSelectedID = Convert.ToInt32(ddlCurrencyPairs.SelectedItem.Value);
        }

        public IQueryable<CurrencyPair> LvCurrencyPairInfo_GetData() =>
            _db.CurrencyPairs.Where(cp => cp.ID == cpSelectedID);
    }
}