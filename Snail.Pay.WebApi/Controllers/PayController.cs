using Snail.Pay.Interceptor;
using Snail.Pay.Model;
using Snail.Pay.Model.ClientModels;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Snail.Pay.WebApi.Controllers
{
    /// <summary>
    /// 统一支付平台对外接口
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
        [Route("Pay/{platform}/{actionType}")]
        public async Task<MethodResult> Pay(string platform, string actionType, [FromBody]OrderInfo order)
        {
            return await PayProxy.InitiatePay(platform, actionType, order);
        }

        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="payType">支付类型，默认为return</param>
        /// <returns>返回处理结果</returns>
        [HttpGet]
        [Route("CallBack/{platform}/{actionType?}")]
        [Monitor(EnableRequest = true, EnableResponse = true)]
        public async Task<IHttpActionResult> Return(string platform, string actionType = "callback")
        {
            var result = await PayProxy.Return(platform, actionType, Request.RequestUri.Query);

            return Redirect(result.Data);
        }

        /// <summary>
        /// 支付后端异步通知
        /// </summary>
        /// <param name="queryOrder">订单实体</param>
        /// <param name="platform">支付平台</param>
        /// <param name="payType">支付类型，默认为notify</param>
        /// <returns>返回处理结果</returns>
        [AcceptVerbs("GET", "POST")]
        [Route("Notify/{platform}/{actionType?}")]
        [Monitor(EnableRequest = true, EnableResponse = true)]
        public async Task<MethodResult> Notify(string platform, string actionType = "notify")
        {
            var queryOrder = Request.RequestUri.Query;
            if (Request.Method == HttpMethod.Post)
            {
                queryOrder = await Request.Content.ReadAsStringAsync();
            }
            return await PayProxy.Notify(platform, actionType, queryOrder);
        }

        /// <summary>
        /// 查询交易结果
        /// </summary>
        /// <param name="orderQuery">查询对象</param>
        /// <param name="payType">查询类型，默认为query</param>
        /// <returns>返回交易结果</returns>
        [HttpPost]
        [Route("Query/{actionType?}")]
        [Monitor(EnableRequest = true, EnableResponse = true)]
        public async Task<MethodResult> Query([FromBody]OrderQueryInfo orderQuery, string actionType = "query")
        {
            return await PayProxy.Query(orderQuery, actionType);
        }

        /// <summary>
        /// 订单退款
        /// </summary>
        /// <param name="orderRefund">订单退款请求对象</param>
        /// <param name="actionType">退款类型，默认为refund</param>
        /// <returns>返回退款结果</returns>
        [HttpPost]
        [Route("Refund/{actionType?}")]
        public async Task<MethodResult> Refund([FromBody]OrderRefundInfo orderRefund, string actionType = "refund")
        {
            return null;
        }
    }
}
