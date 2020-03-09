using Demo.HL7MessageParser.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Xml.Serialization;

namespace Demo.RESTServcie
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //// Web API configuration and services
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            var xml = GlobalConfiguration.Configuration.Formatters.XmlFormatter;
            xml.SetSerializer<AlertInputParm>(new XmlSerializer(typeof(AlertInputParm)));


            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //https://fluentvalidation.net/aspnet
            // FluentValidationModelValidatorProvider.Configure(config);

            config.Services.Replace(typeof(IExceptionLogger), new GlobalExceptionLogger());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());
        }
    }
}
