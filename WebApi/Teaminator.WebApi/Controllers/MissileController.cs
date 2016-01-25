using System.Linq;
using System.Web.Http;
using Teaminator.MissileService;

namespace Teaminator.WebApi.Controllers
{
    public class MissileController : ApiController
    {
        private static readonly Launcher Laucher = new Launcher();
        public MissileController()
        {
        }

        [HttpGet]
        [Route("missile/current")]
        public int[] Current()
        {
            return Laucher.GetCurrentDirection();
        }

        [HttpGet]
        [Route("missile/reset")]
        public bool Reset()
        {
            Laucher.Reset();
            return true;
        }

        [HttpGet]
        [Route("missile/aim/{x},{y}")]
        public bool Aim(int x, int y)
        {
            Laucher.Aim(x, y);
            return true;
        }

        [HttpGet]
        [Route("missile/aim/{pos}")]
        public bool Aim(int pos)
        {
            return true;
        }

        [HttpGet]
        [Route("missile/attack/{userName}")]
        public bool Fire(string userName)
        {
            var posId = Settings.SettingsManager.Settings.Users.First(u => u.Username == userName).Id;
            return Fire(posId);
        }
        public bool Fire(int userId)
        {
            Laucher.Reset();
            var posId = Settings.SettingsManager.Settings.UserPositionMappings.First(m => m.UserId == userId).PositionId;
            var pos = Settings.SettingsManager.Settings.Positions.FirstOrDefault(p => p.Id == posId);
            Laucher.Aim(pos.X,pos.Y);
            //Laucher.Fire();
            return true;
        }

        [HttpGet]
        [Route("missile/fire")]
        public bool Fire()
        {
            Laucher.Fire();
            return true;
        }
    }
}
