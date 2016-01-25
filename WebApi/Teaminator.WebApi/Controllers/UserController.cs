using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Teaminator.Domain.Models;

namespace Teaminator.WebApi.Controllers
{
    public class UserController: ApiController
    {
        public IEnumerable<User> Get()
        {
            return Settings.SettingsManager.Settings.Users;
        }

        public User Get(int id)
        {
            return Settings.SettingsManager.Settings.Users.FirstOrDefault(u => u.Id == id);
        }

        public int Post([FromBody] string userName)
        {
            var user = new User() {Username = userName, Id = Settings.SettingsManager.Settings.Users.Count};
            Settings.SettingsManager.Settings.Users.Add(user);
            Settings.SettingsManager.Save();
            return user.Id;
        }
    }
}
