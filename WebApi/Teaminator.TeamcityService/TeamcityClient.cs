using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Teaminator.Domain.Models;

namespace Teaminator.TeamcityService
{
    public class TeamcityClient
    {
        private string CredentialToken = "";

        private const string builds = "httpAuth/app/rest/builds/";
        private readonly string _host;
        private readonly BuildHandler _handler;


        public TeamcityClient(string host, BuildHandler handler)
        {
            _host = host;
            _handler = handler;
            SetCreds();
        }

        public async Task<IEnumerable<Build>> GetBuilds()
        {
            var uri = new Uri(_host + builds);
            var client = CreateWebClient(uri);

            string result = await client.DownloadStringTaskAsync(uri);
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<BuildsResponse>(result);
            return _handler.HandleBuilds(response.build);
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

        public async Task<string> GetBuild(int buildId)
        {
            //TODO parse response etc
            var uri = new Uri(_host + builds + buildId);
            var client = CreateWebClient(uri);
            string result = await client.DownloadStringTaskAsync(uri);
            //var response = Newtonsoft.Json.JsonConvert.DeserializeObject<BuildsResponse>(result);
            //return _handler.HandleBuilds(response.build);
            return result;
        }
    }
}
