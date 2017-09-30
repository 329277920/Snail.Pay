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
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            // 记录系统日志
            var logMsg = GetLogContent(actionExecutedContext);

            // 定义异常默认返回值
            var result = GetResult(actionExecutedContext);

            // 输出错误信息
            actionExecutedContext.ActionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(Serializer.JsonSerialize(result),
                ConfigManager.Current.Encoding, "application/json")
            };

            await FitterUtility.ErrorAsync(logMsg, actionExecutedContext.Request);
        }

        private string GetLogContent(HttpActionExecutedContext actionExecutedContext)
        {
            var req = actionExecutedContext.Request;
            var log = new StringBuilder("failed:url is " + req.RequestUri.ToString());                        
            var error = actionExecutedContext.Exception;
            if (error != null)
            {
                string errMsg = error.ToString();
                if (error is KnownException kEx)
                {
                    errMsg = kEx.Message;
                }
                log.AppendFormat(",error : {0}", errMsg);
            }
            return log.ToString();
        }

        private MethodResult GetResult(HttpActionExecutedContext actionExecutedContext)
        {
            var error = actionExecutedContext.Exception;
            if (error != null && error is KnowResultException kEx)
            {
                return kEx.Result;
            }
            return new MethodResult(MethodResultCode.ServerFailed,
                MethodResultMessage.ServerFailed);
        }
    }
}
