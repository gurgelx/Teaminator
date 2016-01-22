using System;
using System.Net;

namespace Teaminator.TeamcityService
{
    public class TeamcityListener
    {
        public String[] TargetProjects { get; set; }
        public TeamcityClient Client;
        public TeamcityListener(string[] targetProjects)
        {
            this.TargetProjects = targetProjects;
            this.Client = new TeamcityClient("http://teamcity/");
            this.Client.GetBuilds();
        }
    }
}
