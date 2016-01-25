using System.Runtime.InteropServices;
using System.Web.Http;
using Teaminator.MissileService;

namespace Teaminator.WebApi.Controllers
{
    public class MissileController : ApiController
    {
        private Launcher _laucher;
        public MissileController()
        {
            this._laucher = new Launcher();
        }

        [Route("missile/aim/current")]
        public int[] Current()
        {
            return this._laucher.GetCurrentDirection();
        }

        [Route("missile/aim/reset")]
        public void Reset()
        {
            this._laucher.Reset();
        }

        [Route("missile/aim/{x}/{y}")]
        public void Aim(int x, int y)
        {
            this._laucher.Aim(x,y, false);
        }

        [Route("missile/aim/{pos}")]
        public void Aim(int pos)
        {
            
        }

        [Route("missile/fire")]
        public void Fire(int pos)
        {
            this._laucher.Fire();
        }
    }
}
