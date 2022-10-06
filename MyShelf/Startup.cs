using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyShelf.Startup))]
namespace MyShelf
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
