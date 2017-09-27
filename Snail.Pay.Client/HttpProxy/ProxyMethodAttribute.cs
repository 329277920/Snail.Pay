
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// Http代理属性
    /// </summary>
    public class ProxyMethodAttribute : Attribute
    {
        /// <summary>
        /// 获取或设置HttpMethod，默认为Get请求
        /// </summary>
        public HttpMethod Method { get; set; }

        /// <summary>
        /// 获取或设置调用地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 获取或设置编码方式，默认UTF-8
        /// </summary>
        public Encoding Encoder { get; set; }

        /// <summary>
        /// 获取或设置Http请求内容格式，默认为表单
        /// </summary>
        public string ContentType { get; set; }
         
        public ProxyMethodAttribute()
        {
            this.Method = HttpMethod.Get;
            this.Encoder = Encoding.UTF8;
            this.ContentType = HttpContentType.FormData;        
        }
    }
}
