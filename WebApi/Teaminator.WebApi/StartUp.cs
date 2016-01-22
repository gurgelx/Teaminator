using System.Web.Http;
using Owin;

namespace Teaminator.WebApi
{
    public class StartUp
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var listner = new TeamcityService.TeamcityListener(new []{ "", ""});

            app.UseWebApi(config);
        }
    }
}
