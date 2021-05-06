using Cryptoexch.Logic;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cryptoexch
{
    public partial class Signup : Page
    {
        protected void CreateUser_OnClick(object sender, EventArgs e)
        {
            if (IsValid)
            {
                var manager = new UserManager();
                var user = new ApplicationUser()
                {
                    UserName = tbUserName.Text,
                    Email = tbEmail.Text
                };
                IdentityResult result = manager.Create(user, tbPassword.Text);
                if (result.Succeeded)
                {
                    RoleActions roleActions = new RoleActions();
                    roleActions.AddClient(user.Id, "", "", "", "", ddlCountry.SelectedItem.Text, "");

                    IdentityHelper.SignIn(manager, user, isPersistent: false);
                    IdentityHelper.RedirectToReturnUrl("~/Portfolio", Response);
                }
                else
                {
                    FailureText.Text = result.Errors.FirstOrDefault();
                    ErrorMessage.Visible = true;
                }
            }
        }

        protected void CheckBoxRequired_ServerValidate(object source, ServerValidateEventArgs args) =>
            args.IsValid = cbTermsOfUse.Checked;
    }
}