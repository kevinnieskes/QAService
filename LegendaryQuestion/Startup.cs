using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LegendaryQuestion.Startup))]
namespace LegendaryQuestion
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
