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
    /// 使用表单格式化Http请求参数
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class StringDataAttribute : Attribute, IDataFormatter
    {
        /// <summary>
        /// 获取内容类型
        /// </summary>
        public string ContentType { get { return HttpContentType.String; } }

        /// <summary>
        /// 将参数写入Http请求中
        /// </summary>
        /// <param name="parameter">当前参数</param>
        /// <param name="value">参数值</param>
        /// <param name="context">Http请求上下文</param>
        /// <returns>返回请求内容</returns>
        public string Format(ParameterInfo parameter, object value, RequestContext httpContext)
        {
            return value == null ? string.Empty : value.ToString();
            //var content = new StringContent(value == null ? string.Empty : value.ToString());
            //content.Headers.Clear();
            //content.Headers.Add("Content-Type", string.Format("text/plain; charset={0}", httpContext.Encoder.ToString()));
            //content.Headers.Add("Content-Disposition", string.Format("form-data; name=\"0\"", parameter.Name));
            //return content;
        }
    }
}
