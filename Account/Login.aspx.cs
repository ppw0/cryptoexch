using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cryptoexch
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                if (userMgr.IsInRole(User.Identity.GetUserId(), "Administrator"))
                    Response.Redirect("~/Manage");
                else
                    Response.Redirect("~/Portfolio");
            }

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
                SignupLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var manager = new UserManager();
                ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                if (user != null)
                {
                    IdentityHelper.SignIn(manager, user, false);

                    Session["ClientID"] = user.Id;

                    IdentityHelper.RedirectToReturnUrl("~/", Response);
                }
                else
                {
                    FailureText.Text = "Error while logging in";
                    ErrorMessage.Visible = true;
                }
            }
        }

        protected void CheckBoxRequired_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = NoRobot.Checked;
        }
    }
}