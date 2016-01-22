using System;
using System.Configuration;
using System.Net;
using System.Security.Principal;
using System.Text;
using Teaminator.Domain.Models;

namespace Teaminator.TeamcityService
{
    public class TeamcityClient
    {
        public event EventHandler<EventArgs> BuildError;
        public event EventHandler<EventArgs> BuildSuccess;
        public event EventHandler<EventArgs> BuildRunning;

        private string CredentialToken = "";

        private const string builds = "httpAuth/app/rest/builds/";
        private readonly string host;
        public TeamcityClient(string host)
        {
            this.host = host;
            SetCreds();
        }

        public void GetBuilds()
        {
            var uri = new Uri(host + builds);
            var client = CreateWebClient(uri);

            client.DownloadStringAsync(uri);
            client.DownloadStringCompleted += (sender, args) =>
            {
                if (args.Cancelled) return;
                var response = Newtonsoft.Json.JsonConvert.DeserializeObject<BuildsResponse>(args.Result);
            };
        }

        public WebClient CreateWebClient(Uri uri)
        {
            var wc = new WebClient();
            wc.UseDefaultCredentials = true; // This should work without the credential token?? TODO FIX!
            wc.Headers[HttpRequestHeader.Authorization] = this.CredentialToken;
            wc.Headers[HttpRequestHeader.Accept] = "application/json";

            return wc;
        }

        private void SetCreds()
        {
            var user = System.Configuration.ConfigurationManager.AppSettings["Username"];
            var password= System.Configuration.ConfigurationManager.AppSettings["Password"];
            this.CredentialToken = Convert.ToBase64String(Encoding.ASCII.GetBytes(user + ":" + password));
        }

        public void GetBuild(int buildId)
        {
            
        }
    }
}
