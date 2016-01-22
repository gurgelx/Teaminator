using System.Web.Http;

namespace Teaminator.WebApi.Controllers
{
    public class MissileController : ApiController
    {
        [Route("missile/aim/{pos}")]
        public void Aim(int pos)
        {
            
        }

        [Route("missile/fire")]
        public void Fire(int pos)
        {

        }
    }
}
