using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpProxy
{
    public class WebApiHandler : BaseRequestHandler, IRequestHandler
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
                    string.IsNullOrEmpty(postData) ? "" : "/" + postData);
            }
            else
            {
                throw new Exception("webapi format only support get request.");
            }
        }

        private string BuildQueryString(params KeyValuePair<ParameterInfo, object>[] arguments)
        {
            if (arguments?.Length <= 0)
            {
                return string.Empty;
            }

            var queryString = string.Join("/", (from item in arguments
                                                select
                                                HttpUtility.UrlEncode(item.Value == null ? string.Empty :
                                                item.Value.ToString())).ToArray());

            return string.Join("&", queryString);
        }
    }
}
