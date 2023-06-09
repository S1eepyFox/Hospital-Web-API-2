using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI;

namespace Hospital_Web_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы Web API

            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
               name: "OpenApi",
               routeTemplate: "api/{controller}/{mass}&{height}",
               defaults: new {  key = RouteParameter.Optional, height = RouteParameter.Optional }
               );

           
                  config.Routes.MapHttpRoute(
               name: "PatientApi",
               routeTemplate: "api/{controller}/last_name&first_name&patronymic&height&mass&age/",
               defaults: new {last_name = RouteParameter.Optional, first_name = RouteParameter.Optional,
                                patronymic = RouteParameter.Optional, height = RouteParameter.Optional,
                                mass = RouteParameter.Optional, age = RouteParameter.Optional, }
               );

            
                  config.Routes.MapHttpRoute(
               name: "StatisticsApi",
               routeTemplate: "api/{controller}/",
               defaults: new {
                    }
               );
           

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
