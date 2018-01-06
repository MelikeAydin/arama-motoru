using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(yazlabYam1.Startup))]
namespace yazlabYam1
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
