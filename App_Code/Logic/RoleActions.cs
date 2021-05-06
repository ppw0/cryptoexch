using Cryptoexch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cryptoexch.Logic
{
    public class RoleActions
    {
        public void AddClient(string clientId, string FirstName, string LastName, string Address, string City,
            string Country, string BankAccount)
        {
            var client = new Client()
            {
                ID = clientId,
                FirstName = FirstName,
                LastName = LastName,
                Address = Address,
                City = City,
                Country = Country,
                BankAccount = BankAccount,
                Verified = false
            };

            using (ClientContext _db = new ClientContext())
            {
                _db.Clients.Add(client);
                _db.SaveChanges();
            }
        }

        public void CreateAdmin()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMgr.RoleExists("Administrator"))
                roleMgr.Create(new IdentityRole("Administrator"));

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser()
            {
                UserName = "Admin"
            };

            if (userMgr.Create(appUser, "Pa$$word").Succeeded)
                userMgr.AddToRole(appUser.Id, "Administrator");
        }
    }
}