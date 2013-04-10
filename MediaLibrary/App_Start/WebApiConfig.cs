using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace TheWhitakers.MediaLibrary
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.Routes.MapHttpRoute(
            //    name: "MoviePosters",
            //    routeTemplate: "api/{controller}/{id}/SayHello",
            //    defaults: new { id = RouteParameter.Optional, Action = "SayHello" }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
