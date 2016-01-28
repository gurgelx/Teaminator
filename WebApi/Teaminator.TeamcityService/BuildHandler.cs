using System;
using System.Collections.Generic;
using System.Linq;
using Teaminator.Domain.Models;
using Teaminator.Domain.Models.TeamCity;

namespace Teaminator.TeamcityService
{
    public class BuildHandler
    {
        readonly TeamcityClient _client = new TeamcityClient("http://teamcity/");
        private int _lastUpdateId = 0;
        private bool _firstRun = true;
        private Dictionary<int, Build> Watched = new Dictionary<int, Build>();
         
        public List<string> FilterIds { get; set; }

        public BuildHandler(string[] filterIds)
        {
            this.FilterIds = filterIds.ToList();
        }

        public List<BuildDetails> GetUpdates()
        {
            try
            {
                var builds = _client.GetBuilds().Result
                    .Where(b => (b.id > _lastUpdateId || Watched.ContainsKey(b.id) && b.state == "finished") && FilterIds.Contains(b.buildTypeId)).ToList();                
                

                if (builds.Any())
                {
                    _lastUpdateId = builds.Max(b => b.id);
                    if (_firstRun)
                    {
                        _firstRun = false;
                        return null;
                    }

                    UpdateWatchedList(builds);
                    return GetDetails(builds);
                }
            }
            catch (Exception){}

            return null;
        }

        private void UpdateWatchedList(List<Build> builds)
        {
            builds.Where(b => b.state == "finished").ToList().ForEach(b =>
            {
                if(Watched.ContainsKey(b.id))
                    Watched.Remove(b.id);
            });

            builds.Where(b => b.state != "finished").ToList().ForEach(b =>
            {
                if(!Watched.ContainsKey(b.id))
                    Watched.Add(b.id, b);
            });
        }

        private List<BuildDetails> GetDetails(List<Build> builds)
        {
            _lastUpdateId = builds.Max(b => b.id);
            return builds.Select(b => _client.GetBuild(b.id).Result).ToList();
        }
    }
}
