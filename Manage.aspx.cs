using AjaxControlToolkit;
using Cryptoexch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Cryptoexch
{
    public partial class Manage : Page
    {
        private readonly EmployeeContext _db = new EmployeeContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext context = new ApplicationDbContext();
                var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                if (!userMgr.IsInRole(User.Identity.GetUserId(), "Administrator"))
                    Server.Transfer("Error.aspx?handler=Page_Error%20-%20Manage&msg=401", true);
            }
            else
                Response.Redirect("~/Login");
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.Transfer("Error.aspx?handler=Page_Error%20-%20Manage", true);
        }

        public IQueryable<Employee> GvEmployee_GetData() =>
            _db.Employees;

        public void GvEmployee_DeleteItem(string id)
        {
            Employee e = _db.Employees.Find(id);
            _db.Employees.Remove(e);
            _db.SaveChanges();
        }

        public void GvEmployee_UpdateItem(string id)
        {
            Page.Validate();
            if (!Page.IsValid)
                return;
            Employee e = _db.Employees.Find(id);
            TryUpdateModel(e);
            _db.SaveChanges();
        }

        protected static string Read(GridViewRow r, string ID) =>
            (r.FindControl(ID) as TextBox).Text;

        protected DateTimeOffset? ReadDate(GridViewRow r, string ID) =>
            (r.FindControl(ID) as CalendarExtender).SelectedDate;

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (IsValid)
            {
                try
                {
                    GridViewRow r = gvEmployee.FooterRow;

                    Employee em = new Employee
                    {
                        ID = Guid.NewGuid().ToString(),
                        EmploymentDate = ReadDate(r, "calEmployNew") ?? DateTimeOffset.Now,
                        FirstName = Read(r, "tbFirstNameNew"),
                        LastName = Read(r, "tbLastNameNew"),
                        Address = Read(r, "tbAddressNew"),
                        City = Read(r, "tbCityNew"),
                        Email = Read(r, "tbEmailNew"),
                        Division = Read(r, "tbDivisionNew"),
                        Title = Read(r, "tbTitleNew")
                    };

                    if (ReadDate(r, "calTermNew") != null)
                        em.TerminationDate = ReadDate(r, "calTermNew");

                    _db.Employees.Add(em);
                    _db.SaveChanges();
                    Response.Redirect("~/Manage.aspx");
                }
                catch
                {
                    Exception exc = Server.GetLastError();
                    Server.Transfer("Error.aspx?handler=Page_Error%20-%20Manage", true);
                }
            }
        }
    }
}