using Snail.Pay.Interceptor;
using Snail.Pay.Model;
using Snail.Pay.Model.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Snail.Pay.WebApi.Controllers
{
    /// <summary>
    /// 发起支付接口
    /// </summary>
    public class PayController : ApiController
    {
        /// <summary>
        /// 发起支付请求
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="payType">支付类型</param>
        /// <param name="order">订单实体</param>
        /// <returns>返回处理结果</returns>
        [HttpPost]
        [Route("Pay/{platform}/{payType}")]          
        public async Task<MethodResult> Pay(string platform, string payType, [FromBody]OrderInfo order)
        {
            return await PayProxy.InitiatePay(platform, payType, order);
        }

        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="payType">支付类型，默认为return</param>
        /// <returns>返回处理结果</returns>
        [HttpGet]
        [Route("Return/{platform}/{payType?}")]
        [Monitor(EnableRequest = true, EnableResponse = true)]
        public async Task<MethodResult> Return(string platform, string payType = "return")
        {
            return await PayProxy.Return(platform, payType, Request.RequestUri.Query);
        }

        /// <summary>
        /// 支付后端异步通知
        /// </summary>
        /// <param name="queryOrder">订单实体</param>
        /// <param name="platform">支付平台</param>
        /// <param name="payType">支付类型，默认为notify</param>
        /// <returns>返回处理结果</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("Notify/{platform}/{payType?}")]
        [Monitor(EnableRequest = true, EnableResponse = true)]
        public async Task<MethodResult> Notify(string platform, string payType = "notify")
        {
            var queryOrder = Request.RequestUri.Query;
            if (Request.Method == HttpMethod.Post)
            {
                queryOrder = await Request.Content.ReadAsStringAsync();
            }
            return await PayProxy.Notify(platform, payType, queryOrder);
        }
    }
}
