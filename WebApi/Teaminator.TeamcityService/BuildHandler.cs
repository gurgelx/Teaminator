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
         
        public List<string> FilterIds { get; set; }

        public BuildHandler(string[] filterIds)
        {
            this.FilterIds = filterIds.ToList();
        }

        public List<BuildDetails> GetUpdates()
        {
            try
            {
                var builds = _client.GetBuilds().Result.Where(b => b.id > _lastUpdateId && FilterIds.Contains(b.buildTypeId)).ToList();

                if (_firstRun)
                {
                    _lastUpdateId = builds.Max(b => b.id);
                    _firstRun = false;
                    return null;
                }

                if (builds.Any())
                {
                    _lastUpdateId = builds.Max(b => b.id);
                    return GetDetails(builds);
                }
            }
            catch (Exception){}

            return null;
        }

        private List<BuildDetails> GetDetails(List<Build> builds)
        {
            _lastUpdateId = builds.Max(b => b.id);
            return builds.Select(b => _client.GetBuild(b.id).Result).ToList();
        }
    }
}
