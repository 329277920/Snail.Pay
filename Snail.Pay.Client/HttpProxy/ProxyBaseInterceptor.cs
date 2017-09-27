using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 处理Http请求父类
    /// </summary>
    public abstract class ProxyBaseInterceptor : IInterceptor
    {                      
        /// <summary>
        /// 格式化Http返回结果
        /// </summary>
        /// <param name="context">Http请求对象</param>
        /// <param name="response">Http返回对象</param>
        /// <returns>返回任意类型</returns>
        protected virtual object FormatResult(RequestContext context, HttpResponseMessage response)
        {
            var returnParameter = context.Invocation.Method.ReturnParameter;

            var formatter = ProxyUnity.GetSingleCustomAttribute<IResultFormatter>(returnParameter);
            if (formatter == null)
            {
                if (returnParameter.ParameterType.FullName.Equals("System.String"))
                {
                    formatter = new StringResultAttribute();
                }
                else
                {
                    formatter = new JsonResultAttribute();
                }
            }
            return formatter.Format(response, context);
        }

        public abstract bool Intercept(RequestContext context);
    }
}
