using Snail.Pay.Business;
using Snail.Pay.Model;
using Snail.Pay.Model.ClientModels;
using Snail.Pay.PlatformInterface;
using System.Threading.Tasks;

namespace Snail.Pay
{
    /// <summary>
    /// 支付业务处理代理类
    /// </summary>
    public class PayProxy
    {
        /// <summary>
        /// 发起支付
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="actionType">支付行为</param>
        /// <param name="order">订单信息</param>
        /// <returns>返回发起支付成功后的返回信息</returns>
        public static async Task<MethodResult> InitiatePay(string platform, string actionType, OrderInfo order)
        {
            // 获取支付SDK
            var sdk = PayInterfaceFactory.TryGet<IInitiatePay>(platform, actionType);
            if (sdk == null)
            {
                return new MethodResult(MethodResultCode.RequestFailed, "不支持的支付类型");
            }

            // 创建一笔交易
            var bll = new TransactionBll();
            var addResult = await bll.InsertTransaction(platform, actionType, order);
            if (!addResult.IsSuccess)
            {
                return addResult;  
            }

            // 发起支付
            return sdk.Pay(addResult.Data.Item1, addResult.Data.Item2);
        }

        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="actionType">支付行为</param>
        /// <param name="queryOrder">订单信息，为各平台请求的querystring</param>
        /// <returns>返回处理结果</returns>
        public static async Task<MethodResult> Return(string platform, string actionType, string queryOrder)
        {
            // 获取支付SDK
            var sdk = PayInterfaceFactory.TryGet<IReturn>(platform, actionType);
            if (sdk == null)
            {
                return new MethodResult(MethodResultCode.RequestFailed, "不支持的支付类型");
            }

            // 校验签名
            var signResult = sdk.Return(queryOrder);
            if (!signResult.IsSuccess)
            {
                return signResult;
            }

            // 获取支付订单

            // 处理后续系统业务，包括跳转到业务系统前端地址
            return signResult;
        }

        /// <summary>
        /// 支付后端回调
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="actionType">支付行为</param>
        /// <param name="queryOrder">订单信息，为各平台请求的querystring</param>
        /// <returns>返回处理结果</returns>
        public static async Task<MethodResult> Notify(string platform, string actionType, string queryOrder)
        {
            // 获取支付SDK
            var sdk = PayInterfaceFactory.TryGet<INotify>(platform, actionType);
            if (sdk == null)
            {
                return new MethodResult(MethodResultCode.RequestFailed, "不支持的支付类型");
            }

            // 校验签名
            var signResult = sdk.Notify(queryOrder);
            if (!signResult.IsSuccess)
            {
                return signResult;
            }

            // 获取支付订单

            // 处理后续系统业务，包括跳转到业务系统前端地址
            return signResult;
        }
    }
}
