using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sokuuhotu.Startup))]
namespace Sokuuhotu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
