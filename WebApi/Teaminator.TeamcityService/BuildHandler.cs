using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teaminator.Domain.Models;

namespace Teaminator.TeamcityService
{
    public class BuildHandler
    {
        public List<string> FilterIds { get; set; }

        public BuildHandler(string[] filterIds)
        {
            this.FilterIds = filterIds.ToList();
        }
        public IEnumerable<Build> HandleBuilds(IEnumerable<Build> builds)
        {
            var filtered = builds.Where(b => FilterIds.Contains(b.buildTypeId));
            return filtered;
        }
    }
}
