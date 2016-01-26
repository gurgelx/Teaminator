using System.Linq;
using System.Threading;
using System.Web.Http;
using Owin;
using Teaminator.Domain.Models.TeamCity;

namespace Teaminator.WebApi
{
    public class StartUp
    {
        private Thread killTimer;
        public void Configuration(IAppBuilder app)
        {
            Settings.SettingsManager.Init();
            var buildsToWatch = Settings.SettingsManager.Settings.BuildIds;

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();


            BuildDetails currentThreat = null;
            var listner = new TeamcityService.TeamcityListener(buildsToWatch);
            var missileService = new UserMissileService();

            listner.BuildSuccess += (sender, args) =>
            {
                var build = sender as BuildDetails;
                if (currentThreat != null && build.buildTypeId == currentThreat.buildTypeId)
                {
                    currentThreat = null;
                    missileService.Reset();
                    killTimer?.Abort();
                }
                else
                {
                    switch (build.state)
                    {
                        case "running":
                            missileService.AimAtUser(build.lastChanges.change.First().username);
                            break;
                        case "finished":
                            missileService.Nod();
                            break;
                    }
                }
            };

            listner.BuildError += (sender, args) =>
            {
                var build = sender as BuildDetails;
                if (currentThreat == null) currentThreat = build;
                missileService.AimAtUser(currentThreat.lastChanges.change.First().username);

                killTimer = new Thread(() =>
                {
                    Thread.Sleep(300000);
                    if (currentThreat != null)
                        missileService.AttackUser(currentThreat.lastChanges.change.First().username);
                });
                killTimer.Start();
            };

            listner.Begin();



            app.UseWebApi(config);
        }
    }
}
