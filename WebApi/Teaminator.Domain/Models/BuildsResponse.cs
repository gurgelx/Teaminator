using System.Collections.Generic;

namespace Teaminator.Domain.Models
{
    public class BuildsResponse
    {
        public int count { get; set; }
        public string href { get; set; }
        public string nextHref { get; set; }
        public List<Build> build { get; set; }
    }
}
