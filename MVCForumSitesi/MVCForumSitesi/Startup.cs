using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCForumSitesi.Startup))]
namespace MVCForumSitesi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
