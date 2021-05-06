using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cryptoexch.Startup))]

namespace Cryptoexch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}