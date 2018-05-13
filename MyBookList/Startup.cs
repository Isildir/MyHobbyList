using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyHobbyList.Startup))]
namespace MyHobbyList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
