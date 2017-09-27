﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 使用Xml格式化Http请求参数
    /// </summary>
    public class XmlDataAttribute : Attribute, IDataFormatter
    {
        /// <summary>
        /// 获取内容类型
        /// </summary>
        public string ContentType { get { return HttpContentType.Xml; } }

        /// <summary>
        /// 将参数写入Http请求中
        /// </summary>
        /// <param name="parameter">当前参数</param>
        /// <param name="value">参数值</param>
        /// <param name="context">Http请求上下文</param>
        /// <returns>返回请求内容</returns>
        public string Format(ParameterInfo parameter, object value, RequestContext httpContext)
        {
            return value == null ? string.Empty : Serializer.XmlSerialize(value);          
        }
    }
}
