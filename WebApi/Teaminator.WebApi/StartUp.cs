using System.Web.Http;
using Owin;

namespace Teaminator.WebApi
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            Settings.SettingsManager.Init();
            var buildsToWatch = Settings.SettingsManager.Settings.BuildIds;

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            var listner = new TeamcityService.TeamcityListener(buildsToWatch);
            var launcher = new MissileService.Launcher();
            
            app.UseWebApi(config);
        }
    }
}
