using Snail.Pay.Common;
using Snail.Pay.Config;
using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace Snail.Pay.Interceptor
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // 写入系统日志
            var log = new StringBuilder("failed:");
            var req = actionExecutedContext.Request;
            log.AppendFormat("url is {0}", req.RequestUri.ToString());

            var error = actionExecutedContext.Exception;
            if (error != null)
            {
                string errMsg = error.ToString();
                KnownException kEx = error as KnownException;
                if (kEx != null)
                {
                    errMsg = kEx.Message;
                }
                log.AppendFormat(",error : {0}", errMsg);
            }
           
            // 记录系统日志
            await FitterUtility.InfoAsync(actionExecutedContext.Request, log.ToString());

            var result = new MethodResult(MethodResultCode.ServerFailed,
                MethodResultMessage.ServerFailed);
            var res = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            res.Content = new StringContent(Serializer.JsonSerialize(result),
                ConfigManager.Current.Encoding, "application/json");
            actionExecutedContext.ActionContext.Response = res;
        }
    }
}
