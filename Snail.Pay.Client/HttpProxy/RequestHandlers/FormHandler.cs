
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpProxy
{
    /// <summary>
    /// 表单提交
    /// </summary>
    internal class FormHandler : BaseRequestHandler, IRequestHandler
    {
        /// <summary>
        /// 处理Request请求
        /// </summary>
        /// <param name="context">HttpProxyRequestContext 对象</param>
        public void Handle(RequestContext context)
        {
            var postData = this.BuildQueryString(base.GetArguments(context));
            if (base.IsAppendToUrl(context))
            {
                context.Url = string.Format("{0}{1}", context.Url,
                    string.IsNullOrEmpty(postData) ? "" : "?" + postData);
            }
            else
            {
                context.Content = new StringContent(postData, context.Encoder, HttpContentType.FormData);
            }
        }

        private string BuildQueryString(params KeyValuePair<ParameterInfo, object>[] arguments)
        {
            if (arguments == null || arguments.Length <= 0)
            {
                return string.Empty;
            }

            var queryString = (from item in arguments
                               select string.Format("{0}={1}",
                               item.Key.Name,
                               HttpUtility.UrlEncode(item.Value == null ? string.Empty :
                               item.Value.ToString()))).ToArray();

            return string.Join("&", queryString);
        }
    }
}
