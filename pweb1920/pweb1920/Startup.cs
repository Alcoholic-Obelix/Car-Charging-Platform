using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pweb1920.Startup))]
namespace pweb1920
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
