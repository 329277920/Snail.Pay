using Snail.Pay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Snail.Pay.Interceptor
{
    /// <summary>
    /// 提供对Action的监控
    /// </summary>
    public class MonitorAttribute : ActionFilterAttribute
    {               
        /// <summary>
        /// 获取或设置是否对请求值监控，默认False
        /// </summary>
        public bool EnableRequest { get; set; }

        /// <summary>
        /// 获取或设置是否对返回值监控，默认False
        /// </summary>
        public bool EnableResponse { get; set; }

        /// <summary>
        /// 在Action执行前
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            if (!EnableRequest)
            {
                return;
            }
            var content = await GetRequestString(actionContext);
            if (content?.Length > 0)
            {
                await FitterUtility.InfoAsync(content, actionContext.Request);
            }
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }

        /// <summary>
        /// 在Action执行后
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            if (!EnableResponse)
            {
                return;
            }
            var content = await GetResponseString(actionExecutedContext);
            if (content?.Length > 0)
            {
                await FitterUtility.InfoAsync(content, actionExecutedContext.Request);
            }
            await base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
        
        protected virtual async Task<string> GetRequestString(HttpActionContext actionContext)
        {
            var req = actionContext.Request;
            var reqStr = new StringBuilder();
            reqStr.AppendFormat("{0} {1}", req.Method, req.RequestUri.ToString());
            if (req.Method == HttpMethod.Post || req.Method == HttpMethod.Put)
            {
                var postStr = await req.Content.ReadAsStringAsync();
                if (postStr?.Length > 0)
                {
                    reqStr.AppendFormat("\r\ndata:{0}", postStr);
                }
            }
            if (actionContext.ActionArguments?.Count > 0)
            {
                reqStr.Append("\r\narguments:");
                foreach (var kv in actionContext.ActionArguments)
                {
                    reqStr.AppendFormat("{0},{1}", kv.Key, kv.Value == null ? "" : Serializer.JsonSerialize(kv.Value));
                }
            }
            return reqStr.ToString();
        }

        protected virtual async Task<string> GetResponseString(HttpActionExecutedContext actionContext)
        {
            var resStr = "";
            if (actionContext != null && actionContext.Response != null && actionContext.Response.Content != null)
            {
                resStr = await actionContext.Response.Content.ReadAsStringAsync();
            }          
            return string.Format("res {0}", resStr?.Length > 0 ? resStr : "");         
        }                         
    }
}
