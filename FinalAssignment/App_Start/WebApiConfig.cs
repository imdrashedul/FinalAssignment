using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace FinalAssignment
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Filters.Add(new BasicAuthenticationAttribute());

            config.Routes.MapHttpRoute(
                name: "SingleUserApi",
                routeTemplate: "api/users/{username}/{password}",
                defaults: new { controller = "User", action = "Get" }
            );

            config.Routes.MapHttpRoute(
                name: "SingleCommentApi",
                routeTemplate: "api/posts/{id}/comments/{commentId}",
                defaults: new { controller = "Post", action = "GetComment" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();

            EnableCorsAttribute cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);
        }
    }
}
