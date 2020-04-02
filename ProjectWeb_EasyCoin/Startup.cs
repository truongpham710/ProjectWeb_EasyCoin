using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectWeb_EasyCoin.Startup))]
namespace ProjectWeb_EasyCoin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
