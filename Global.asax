<%@ Application Language="C#" %>
<%@ Import Namespace="Cryptoexch" %>
<%@ Import Namespace="Cryptoexch.Models" %>
<%@ Import Namespace="Cryptoexch.Logic" %>
<%@ Import Namespace="System.Data.Entity" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>

<script RunAt="server">

    void AddScript(string name, string path) =>
        ScriptManager.ScriptResourceMapping.AddDefinition(name, new ScriptResourceDefinition { Path = path });

    void Application_Start(object sender, EventArgs e) {
        // script mapping
        AddScript("jquery", "~/Scripts/jquery-3.3.1.js");
        AddScript("bootstrap", "~/Scripts/bootstrap.bundle.js");
        AddScript("Chart.js", "~/Scripts/Chart.js");
        AddScript("DataTables", "~/Scripts/jquery.DataTables.js");
        AddScript("AbsoluteSort", "~/Scripts/absolute.js");
        AddScript("myScript", "~/Scripts/script.js");

        // create admin role and user
        RoleActions roleActions = new RoleActions();
        roleActions.CreateAdmin();

        // add routes
        RouteConfig.RegisterRoutes(RouteTable.Routes);
        BundleConfig.RegisterBundles(BundleTable.Bundles);
        RegisterCustomRoutes(RouteTable.Routes);
    }

    void RegisterCustomRoutes(RouteCollection routes) {
        routes.MapPageRoute("default", "Default", "~/");
        routes.MapPageRoute("login", "Login", "~/Account/Login.aspx");
        routes.MapPageRoute("signup", "Signup", "~/Account/Signup.aspx");
        routes.MapPageRoute("portfolio", "Portfolio", "~/Portfolio.aspx");
        routes.MapPageRoute("charts", "Charts", "~/Charts.aspx");
        routes.MapPageRoute("error", "Error", "~/Error.aspx");
        routes.MapPageRoute("support", "Support", "~/Support.aspx");
        routes.MapPageRoute("deposit", "Deposit", "~/Deposit.aspx");
        routes.MapPageRoute("withdraw", "Withdraw", "~/Withdraw.aspx");
        routes.MapPageRoute("about", "About", "~/About.aspx");
        routes.MapPageRoute("verify", "Verify", "~/Account/Verify.aspx");
    }

    void Application_BeginRequest(object sender, EventArgs e) {
        CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
        Thread.CurrentThread.CurrentUICulture = culture;
        Thread.CurrentThread.CurrentCulture = culture;
    }

    void Application_Error(object sender, EventArgs e) {
        Exception exc = Server.GetLastError();
        if (exc is HttpUnhandledException)
            if (exc.InnerException != null)
                exc = new Exception(exc.InnerException.Message);
        Server.Transfer("Error.aspx?handler=Application_Error%20-%20Global.asax", true);
    }

</script>