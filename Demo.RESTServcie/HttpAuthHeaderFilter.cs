﻿using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace Demo.RESTServcie
{
    public class HttpAuthHeaderFilter : IOperationFilter
    {
         /// <summary>
         /// 应用
         /// </summary>
         /// <param name="operation"></param>
         /// <param name="schemaRegistry"></param>
         /// <param name="apiDescription"></param>
         public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
 
         {
             if (operation.parameters == null)
                 operation.parameters = new List<Parameter>();
             var filterPipeline = apiDescription.ActionDescriptor.GetFilterPipeline(); //判断是否添加权限过滤器
             var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Instance).Any(filter => filter is IAuthorizationFilter); //判断是否允许匿名方法 
             var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
             //if (isAuthorized && !allowAnonymous)
             //{
                 operation.parameters.Add(new Parameter { name = "client_secret", @in = "header", description = "client secret(default value: CLIENT_SECRET)", required = true, type = "string" });
                 operation.parameters.Add(new Parameter { name = "pathospcode", @in = "header", description = "pathospcode(default value: VH)", required = true, type = "string" });
            //}
        }
    }
}