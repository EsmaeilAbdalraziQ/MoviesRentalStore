using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Cinema
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //Using Camel Notation
            var setting = config.Formatters.JsonFormatter.SerializerSettings;
            //To Enable Camel Casing We Need To Set Couple Of Property
            
            //1- setting.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            setting.ContractResolver = new CamelCasePropertyNamesContractResolver();
            
            //2- setting.Formatting = Newtonsoft.Json.Formatting.Indented;
            setting.Formatting = Formatting.Indented;

            //------------------------------------------------------------------------------
            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
