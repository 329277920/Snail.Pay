﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Snail.Pay.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            // 注册异常处理     
            config.Filters.Add(new Interceptor.ExceptionHandlerAttribute());

            config.MessageHandlers.Add(new Interceptor.InitRequestHandller());
        }
    }
}
