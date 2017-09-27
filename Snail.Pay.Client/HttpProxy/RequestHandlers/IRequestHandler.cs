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
    /// Http请求类型接口
    /// </summary>
    public interface IRequestHandler
    {
        /// <summary>
        /// 处理Request请求
        /// </summary>
        /// <param name="context">HttpProxyRequestContext 对象</param>
        void Handle(RequestContext context);        
    }
}
