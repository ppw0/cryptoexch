using System;
using System.Web.UI;

namespace Cryptoexch
{
    public partial class Charts : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void Page_Error(object sender, EventArgs e)
        {
            Exception exc = Server.GetLastError();
            Server.Transfer("Error.aspx?handler=Page_Error%20-%20Portfolio", true);
        }
    }
}