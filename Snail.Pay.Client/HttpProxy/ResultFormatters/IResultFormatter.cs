using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// Http返回值格式化接口
    /// </summary>
    public interface IResultFormatter
    {
        /// <summary>
        /// 格式化Http返回值
        /// </summary>
        /// <param name="response">HttpResponseMessage 对象</param>
        /// <param name="context">Request请求上下文</param>
        /// <returns>返回任意类型</returns>
        object Format(HttpResponseMessage response, RequestContext context);
    }
}
