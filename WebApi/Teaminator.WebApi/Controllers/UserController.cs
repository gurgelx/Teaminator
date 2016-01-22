using System.Collections.Generic;
using System.Web.Http;

namespace Teaminator.WebApi.Controllers
{
    public class UserController: ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] {"user", "user 2"};
        }

        public string Get(int id)
        {
            return "user";
        }

        public int Post([FromBody] string userName)
        {
            return 0;
        }
    }
}
