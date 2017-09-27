using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// Json提交
    /// </summary>
    internal class JsonHandler : BaseRequestHandler, IRequestHandler
    {
        /// <summary>
        /// 处理Request请求
        /// </summary>
        /// <param name="context">HttpProxyRequestContext 对象</param>
        public void Handle(RequestContext context)
        {
            var postData = this.BuildJson(base.GetArguments(context));
            if (base.IsAppendToUrl(context))
            {
                context.Url = string.Format("{0}{1}",context.Url,
                    string.IsNullOrEmpty(postData) ? "" : "?" + postData);
            }
            else
            {
                context.Content = new StringContent(postData, context.Encoder,
                    HttpContentType.Json);
            }
        }

        public string BuildJson(params KeyValuePair<ParameterInfo, object>[] arguments)
        {
            if (arguments?.Length <= 0)
            {
                return string.Empty;
            }
            if (arguments.Length == 1)
            {
                return arguments[0].Value?.ToString();
            }
            return "{"
                +
                string.Join(",", from item in arguments
                                 select string.Format("\"{0}\":",
                                 item.Key,
                                 item.Key.ParameterType.FullName == "System.String" ? "\"" + item.Value?.ToString() + "\""
                                 : item.Value?.ToString()).ToArray())
            + "}";
        }
    }
}
