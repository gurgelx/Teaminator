using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teaminator.Domain.Models
{
    
    public class Settings
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public List<User> Users { get; set; }
        public List<Position> Positions { get; set; }
        public List<UserPositionMapping> UserPositionMappings { get; set; }
        public String[] BuildIds { get; set; }

    }
}
