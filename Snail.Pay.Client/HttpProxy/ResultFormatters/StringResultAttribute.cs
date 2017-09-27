using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 使用String格式序列化HttpResponse
    /// </summary>
    [AttributeUsage(AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class StringResultAttribute : Attribute, IResultFormatter
    {
        /// <summary>
        /// 格式化Http返回值
        /// </summary>
        /// <param name="response">HttpProxyResponseContext 对象</param>
        /// <param name="context">Request请求上下文</param>
        /// <returns>返回任意类型</returns>
        public virtual object Format(HttpResponseMessage response, RequestContext context)
        {
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
