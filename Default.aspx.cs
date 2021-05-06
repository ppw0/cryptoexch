using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace Cryptoexch
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                // verify role

                ApplicationDbContext context = new ApplicationDbContext();
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                if (userMgr.IsInRole(User.Identity.GetUserId(), "Administrator"))
                    Response.Redirect("~/Manage");
                else
                    Response.Redirect("~/Portfolio");
            }
            else
            {
                Response.Redirect("~/Login");
            }
        }
    }
}