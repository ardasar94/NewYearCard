using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewYearCard.Startup))]
namespace NewYearCard
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
