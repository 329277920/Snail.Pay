using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// Http代理拦截器接口
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// 处理Http请求
        /// </summary>
        /// <param name="context">方法调用上下文对象</param>
        /// <returns>返回是否继续执行后续拦截器</returns>
        bool Intercept(RequestContext context);
    }
}
