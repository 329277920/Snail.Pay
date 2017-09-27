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
    /// Http请求数据格式化接口
    /// </summary>
    public interface IDataFormatter
    {
        /// <summary>
        /// 获取内容类型
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// 将参数写入Http请求中
        /// </summary>
        /// <param name="parameter">当前参数</param>
        /// <param name="value">参数值</param>
        /// <param name="httpContext">Http请求上下文</param>
        /// <returns>返回请求内容</returns>
        string Format(ParameterInfo parameter, object value, RequestContext httpContext);
    }
}
