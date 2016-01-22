using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Teaminator.WebApi.Controllers
{
    [RoutePrefix("Placement")]
    public class PlacementController: ApiController
    {

        public int AddPosition(int x, int y)
        {
            return 0;
        }
        public void UpdatePosition(int pos, int x, int y)
        {
            
        }
        public int AddUser(string user)
        {
            return 0;
        }
        public void MapPosition(int user, int position)
        {
            
        }
        [HttpGet]
        [Route("ListUsers")]
        public List<String> ListUsers()
        {
            return null;
        }
        public List<String> ListPositions()
        {
            return null;
        }
    }
}
