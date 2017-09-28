using HttpProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Client
{
    /// <summary>
    /// 支付服务
    /// </summary>
    public interface IPayService
    {
        /// <summary>
        /// 发起支付请求
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="actionType">支付行为</param>
        /// <param name="order">订单信息</param>
        /// <returns>返回发起支付成功后的返回信息</returns>
        [ProxyMethod(Url = "Pay", ContentType = HttpContentType.Json, Method = HttpMethod.Post)]
        [return: StringResult]
        Task<dynamic> InitiatePay([DataIgnore]string platform, [DataIgnore]string actionType, object order);

        /// <summary>
        /// 支付后端异步通知
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="payType">支付类型，默认为notify</param>
        /// <returns>返回处理结果</returns>     
        [ProxyMethod(Url = "Notify", ContentType = HttpContentType.FormData, Method = HttpMethod.Post)]
        Task<dynamic> Notify([DataIgnore]string platform, [DataIgnore]string payType, string orderNo, int amount);

        /// <summary>
        /// 查询交易结果
        /// </summary>
        /// <param name="orderQuery">查询对象</param>
        /// <param name="payType">查询类型，默认为query</param>
        /// <returns>返回交易结果</returns>
        [ProxyMethod(Url = "Query", ContentType = HttpContentType.Json, Method = HttpMethod.Post)]
        Task<dynamic> Query(object queryOrder);
    }

    public class DefaultIntercept : IInterceptor
    {
        public bool Intercept(RequestContext context)
        {
            context.Url = "http://192.168.10.82/" + context.Url;

            if (context.Invocation.Method.Name == "InitiatePay"
                || context.Invocation.Method.Name == "Notify")
            {
                context.Url = string.Format("{0}/{1}/{2}",
                    context.Url,
                    context.Invocation.Arguments[0].ToString(),
                    context.Invocation.Arguments[1].ToString());
            }
            return true;
        }
    }    
}
