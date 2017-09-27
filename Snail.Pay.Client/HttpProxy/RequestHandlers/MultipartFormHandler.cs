using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 多表单提交
    /// </summary>
    internal class MultipartFormHandler : BaseRequestHandler, IRequestHandler
    {
        /// <summary>
        /// 处理Request请求
        /// </summary>
        /// <param name="context">HttpProxyRequestContext 对象</param>
        public void Handle(RequestContext context)
        {
            var content = new System.Net.Http.MultipartFormDataContent(ProxyUnity.GenerateMultipartBoundary());
         
            var i = -1;
            foreach (var parameter in context.Invocation.Method.GetParameters())
            {
                i++;

                // 自定义HttpContent
                var customFormatter = ProxyUnity.GetSingleCustomAttribute<ICustomDataFormatter>(parameter);                
                if (customFormatter != null)
                {
                    content.Add(customFormatter.Format(parameter, context.Invocation.Arguments[i], context));
                    continue;
                }
                var formatter = base.GetHttpDataFormatter(parameter);

                var value = formatter.Format(parameter, context.Invocation.Arguments[i], context);

                var subContent = new StringContent(value == null ? string.Empty : value.ToString());
                subContent.Headers.Clear();
                subContent.Headers.Add("Content-Type", string.Format("{0}; charset={1}", 
                    formatter.ContentType,
                    context.Encoder.ToString()));
                subContent.Headers.Add("Content-Disposition", string.Format("form-data; name=\"{0}\"", parameter.Name));

                content.Add(subContent);      
            }

            context.Content = content;
        }
    }
}
