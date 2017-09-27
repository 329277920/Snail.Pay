using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    [AttributeUsage(AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class XmlResultAttribute : StringResultAttribute, IResultFormatter
    {
        /// <summary>
        /// 格式化Http返回值
        /// </summary>
        /// <param name="response">HttpResponseMessage 对象</param>
        /// <param name="context">Request请求上下文</param>
        /// <returns>返回任意类型</returns>
        public override object Format(HttpResponseMessage response, RequestContext context)
        {
            var value = base.Format(response, context);
            if (value == null)
            {
                return null;
            }
            return Serializer.XmlDeserialize<object>(value.ToString());
        }
    }
}
