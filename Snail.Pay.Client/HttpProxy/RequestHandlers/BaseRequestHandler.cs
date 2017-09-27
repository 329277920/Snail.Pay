using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    public abstract class BaseRequestHandler
    {
        /// <summary>
        /// 获取Http请求参数列表
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected KeyValuePair<ParameterInfo, object>[] GetArguments(RequestContext context)
        {
            var i = 0;
            return (from item in (from p in context.Invocation.Method.GetParameters()
                                  select new KeyValuePair<ParameterInfo, object>(
                                      p,
                                      this.GetHttpDataFormatter(p).Format(p, context.Invocation.Arguments[i++], context)
                                      ))
                    where item.Key.GetCustomAttribute<DataIgnoreAttribute>() == null
                    select item).ToArray();
        }

        /// <summary>
        /// 通过参数信息获取格式化对象
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        protected virtual IDataFormatter GetHttpDataFormatter(ParameterInfo parameter)
        {
            var formatter = ProxyUnity.GetSingleCustomAttribute<IDataFormatter>(parameter);
            if (formatter == null)
            {
                if (DataConverter.IsBasicType(parameter.ParameterType))
                {
                    formatter = new StringDataAttribute();
                }
                else
                {
                    formatter = new JsonDataAttribute();
                }
            }
            return formatter;
        }

        protected virtual bool IsAppendToUrl(RequestContext context)
        {
            return !(context.Method == System.Net.Http.HttpMethod.Put || context.Method == System.Net.Http.HttpMethod.Post);
        }         
    }
}
