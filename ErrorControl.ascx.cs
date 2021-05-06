using Cryptoexch.Logic;
using System;
using System.Web;
using System.Web.UI;

namespace Cryptoexch
{
    public partial class ErrorControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string generalErrorMsg = "A general error has occured. Please try again.";
            string httpErrorMsg = "An HTTP error has occured. Page not found. Please try again.";
            string unhandledErrorMsg = "The error was not handled by the application.";
            string unauthorizedMsg = "You are not authorized to view this page.";

            FriendlyErrorMsg.Text = generalErrorMsg;

            string errorHandler = Request.QueryString["handler"];
            if (errorHandler == null)
                errorHandler = "Error";

            Exception ex = Server.GetLastError();

            string errorMsg = Request.QueryString["msg"];
            if (errorMsg == "404")
            {
                ex = new HttpException(404, httpErrorMsg, ex);
                FriendlyErrorMsg.Text = ex.Message;
            }

            if (errorMsg == "401")
            {
                ex = new HttpException(401, unauthorizedMsg, ex);
                FriendlyErrorMsg.Text = ex.Message;
            }

            if (ex == null)
                ex = new Exception(unhandledErrorMsg);

            if (Request.IsLocal)
            {
                ErrorDetailedMsg.Text = ex.Message;
                ErrorHandler.Text = errorHandler;
                DetailedErrorPanel.Visible = true;

                if (ex.InnerException != null)
                {
                    InnerMessage.Text = ex.GetType().ToString() + "<br/>" + ex.InnerException.Message;
                    InnerTrace.Text = ex.InnerException.StackTrace;
                }
                else
                {
                    InnerMessage.Text = ex.GetType().ToString();
                    if (ex.StackTrace != null)
                        InnerTrace.Text = ex.StackTrace.ToString().TrimStart();
                }
            }
            ExceptionUtility.LogException(ex, errorHandler);
            Server.ClearError();
        }
    }
}