using System;
using System.Linq;
using System.Net;

namespace Teaminator.TeamcityService
{
    public class TeamcityListener
    {

        public event EventHandler<EventArgs> BuildError;
        public event EventHandler<EventArgs> BuildSuccess;
        public event EventHandler<EventArgs> BuildRunning;

        public String[] TargetProjects { get; set; }
        public TeamcityClient Client;
        public TeamcityListener(string[] targetProjects)
        {
            this.TargetProjects = targetProjects;
            this.Client = new TeamcityClient("http://teamcity/", new BuildHandler(targetProjects));

            this.Client.GetBuilds().ContinueWith(result =>
            {
                foreach (var build in result.Result.Where(b => b.status == "SUCCESS"))
                {
                    Client.GetBuild(build.id);
                    BuildSuccess?.Invoke(build, null);
                }

                foreach (var build in result.Result.Where(b => b.status == "FAILURE"))
                {
                    BuildError?.Invoke(build, null);
                }
            });
        }
    }
}
