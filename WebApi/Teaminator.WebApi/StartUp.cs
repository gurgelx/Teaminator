using System;
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
                            missileService.AimAtUser(GetUsername(build));
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
                if(!missileService.AimAtUser(GetUsername(currentThreat)))
                    missileService.Shake();

                killTimer = new Thread(() =>
                {
                    Thread.Sleep(1000 * new Random(DateTime.Now.Millisecond).Next(10));
                    if (currentThreat != null)
                        missileService.AttackUser(GetUsername(currentThreat));
                });
                killTimer.Start();
            };

            listner.Begin();



            app.UseWebApi(config);
        }

        private string GetUsername(BuildDetails build)
        {
            if (build.lastChanges == null || build.lastChanges.change == null) return "";
            return build.lastChanges.change.First().username;
        }
    }
}
