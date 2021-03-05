using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using System.Web.Http;
using Owin;

[assembly: OwinStartup(typeof(Core.Startup))]

namespace Core
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
