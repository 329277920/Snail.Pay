using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Pay.Interceptor
{
    /// <summary>
    /// 初始化Http请求
    /// </summary>
    public class InitRequestHandller : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            SetLogId(request, cancellationToken);

            return base.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// 将本次请求的某一时区的唯一ID加入请求头
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        private void SetLogId(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            FitterUtility.TrySetUniqueId(request);
        }
    }
}
