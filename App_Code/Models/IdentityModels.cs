using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Web;

namespace Cryptoexch
{
    public class ApplicationUser : IdentityUser { }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
    }

    public class UserManager : UserManager<ApplicationUser>
    {
        public UserManager() : base(new UserStore<ApplicationUser>(new ApplicationDbContext()))
        {
        }
    }

    public static class IdentityHelper
    {
        public const string XsrfKey = "XsrfId";

        public static void SignIn(UserManager manager, ApplicationUser user, bool isPersistent)
        {
            IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        public const string ProviderNameKey = "providerName";

        public static string GetProviderNameFromRequest(HttpRequest request) =>
            request[ProviderNameKey];

        private static bool IsLocalUrl(string url) =>
            !string.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));

        public static void RedirectToReturnUrl(string returnUrl, HttpResponse response) =>
            response.Redirect((!String.IsNullOrEmpty(returnUrl) && IsLocalUrl(returnUrl)) ? returnUrl : "~/");
    }
}