using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 记录Http请求内容类型
    /// </summary>
    public sealed class HttpContentType
    {
        public const string FormData = "application/x-www-form-urlencoded";

        public const string WebApi = "application/webapi";

        public const string Json = "application/json";

        public const string Xml = "application/xml";

        public const string String = "text/plain";

        public const string MultipartFormData = "multipart/form-data";   
    }
}

