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
        [Route("missile/fire")]
        public bool Fire()
        {
            Laucher.Fire();
            return true;
        }
    }
}
