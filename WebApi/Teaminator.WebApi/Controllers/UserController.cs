using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web.Http;
using Teaminator.Domain.Models;

namespace Teaminator.WebApi.Controllers
{
    [RoutePrefix("user")]
    public class UserController: ApiController
    {
        [HttpGet]
        [Route("list")]
        public IEnumerable<User> Get()
        {
            return Settings.SettingsManager.Settings.Users;
        }

        [HttpGet]
        [Route("get/{id}")]
        public User Get(int id)
        {
            return Settings.SettingsManager.Settings.Users.FirstOrDefault(u => u.Id == id);
        }

        [HttpGet]
        [Route("add/{userName}")]
        public int Post(string userName)
        {
            if (Settings.SettingsManager.Settings.Users.Any(u => u.Username == userName)) return -1;
            var user = new User() {Username = userName, Id = Settings.SettingsManager.Settings.Users.Count};
            Settings.SettingsManager.Settings.Users.Add(user);
            Settings.SettingsManager.Save();
            return user.Id;
        }
    }
}
