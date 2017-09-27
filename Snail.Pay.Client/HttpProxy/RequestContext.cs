using Castle.Core.Interceptor;
using System;
using System.Text;

namespace HttpProxy
{
    /// <summary>
    /// 代理拦截器调用上下文
    /// </summary>
    public class RequestContext : System.Net.Http.HttpRequestMessage
    {
        /// <summary>
        /// 获取或设置编码方式，默认UTF-8
        /// </summary>
        public Encoding Encoder { get; set; }

        /// <summary>
        /// 获取方法调用上下文信息
        /// </summary>
        public IInvocation Invocation { get; private set; }

        /// <summary>
        /// 获取或设置Http请求内容拼装方式，默认为表单提交
        /// </summary>
        public IRequestHandler RequestHandler { get; set; }


        private string _url;
        /// <summary>
        /// 获取或设置请求地址
        /// </summary>
        public string Url
        {
            get { return this._url; }
            set
            {
                this._url = value;
                Uri outUri = null;
                if (Uri.TryCreate(this._url, UriKind.Absolute, out outUri))
                {
                    this.RequestUri = outUri;
                }
            }
        }       

        /// <summary>
        /// 判断当前是否为异步调用
        /// </summary>
        public bool IsAsyncCall
        {
            get
            {
                return this.Invocation.Method.ReturnType.FullName.StartsWith("System.Threading.Tasks.Task");
            }
        }

        public string ContentType { get; set; }

        internal RequestContext(ProxyMethodAttribute settings, IInvocation invocation)
        {
            this.Method = ProxyUnity.ConvertToHttpMethod(settings.Method);
            this.Encoder = settings.Encoder;
            this.Invocation = invocation;
            this.Url = settings.Url;    
        }
    }
}
