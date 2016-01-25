using System.Web.Http;
using Teaminator.MissileService;

namespace Teaminator.WebApi.Controllers
{
    public class MissileController : ApiController
    {
        private readonly Launcher _laucher;
        public MissileController()
        {
            this._laucher = new Launcher();
        }

        [Route("missile/aim/current")]
        public int[] Current()
        {
            return this._laucher.GetCurrentDirection();
        }

        [HttpGet]
        [Route("missile/reset")]
        public void Reset()
        {
            this._laucher.Reset();
        }

        [HttpGet]
        [Route("missile/aim/{x},{y}")]
        public bool Aim(int x, int y)
        {
            this._laucher.Aim(x, y, true);
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
            this._laucher.Fire();
            return true;
        }
    }
}
