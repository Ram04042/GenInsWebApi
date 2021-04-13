using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Net.Http.Headers;

namespace GenInsWebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            //origin,header,body
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // For output format to be in JSON.
            //config.Formatters.JsonFormatter.SupportedMediaTypes
            //.Add(new MediaTypeHeaderValue("text/html"));

            // we can do the above to change it to json or we can clear the xml formatter so that it will take the json format
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // reverse for xml format
            // config.Formatters.Remove(config.Formatters.XmlFormatter);
            // config.Formatters.Remove(config.Formatters.JsonFormatter);
        }
    }
}
