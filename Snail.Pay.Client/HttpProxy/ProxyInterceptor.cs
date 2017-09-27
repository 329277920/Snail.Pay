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
    /// Http调用默认实现类
    /// </summary>
    internal class ProxyInterceptor : ProxyBaseInterceptor
    {        
        /// <summary>
        /// 存储用于发起Http请求的HttpClient
        /// </summary>
        private Lazy<HttpClient> _lazyHttpClient = new Lazy<HttpClient>(() =>
        {
            return new HttpClient();
        }, true);

        private async Task<object> InvokeAsync(RequestContext context)
        {            
            var response = await _lazyHttpClient.Value.SendAsync(context);

            return FormatResult(context, response);
        }

        private object Invoke(RequestContext context)
        {         
            var response = _lazyHttpClient.Value.SendAsync(context).Result;

            return FormatResult(context, response);
        }

        public override bool Intercept(RequestContext context)
        {
            if (context.IsAsyncCall)
            {
                context.Invocation.ReturnValue = this.InvokeAsync(context);
            }
            else
            {
                context.Invocation.ReturnValue = this.Invoke(context);
            }
            return true;
        }
                
        public void Dispose()
        {
            if (this._lazyHttpClient.IsValueCreated)
            {
                this._lazyHttpClient.Value.Dispose();
            }
        }
    }
}
