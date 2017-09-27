using Snail.Pay.DataLayerInterface;
using Snail.Pay.Model;
using Snail.Pay.Model.Check;
using Snail.Pay.Model.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Business
{
    /// <summary>
    /// 交易数据业务逻辑类
    /// </summary>
    public class TransactionBll
    {
        /// <summary>
        /// 添加交易订单
        /// </summary>
        /// <param name="platform">支付平台</param>
        /// <param name="actionType">交易类型</param>
        /// <param name="orderEntity">用户提交订单实体</param>
        /// <returns>如果成功，返回新创建的交易实体 TransactionInfo</returns>
        public async Task<MethodResult> InsertTransaction(string platform, string actionType, OrderInfo orderEntity)
        {
            // 校验订单实体
            var chkResult = await CheckDataManager.CheckOrder(orderEntity);
            if (!chkResult.Item1)
            {
                return new MethodResult(MethodResultCode.RequestFailed, chkResult.Item2, null);
            }

            var dal = DataLayerFactory.GetDataLayer<ITransactionDal>();

            // 判断订单是否存在交易记录
            var oldTransaction = dal.Select(orderEntity.AppId, orderEntity.OrderNo);
            if (oldTransaction?.TransactionId?.Length > 0)
            {
                switch (oldTransaction.Status)
                {
                    case TransactionStatus.Unpaid:
                        return new MethodResult(MethodResultCode.OrderExisted, MethodResultMessage.OrderExisted, null);
                    // 如果订单已支付，返回支付信息
                    case TransactionStatus.Paid:
                        return new MethodResult(MethodResultCode.OrderPaid, MethodResultMessage.OrderPaid, oldTransaction);
                }
            }

            // 创建交易实体
            var tradeInfo = new TransactionInfo();
            tradeInfo.AppId = orderEntity.AppId;
            tradeInfo.Amount = orderEntity.Amount;
            tradeInfo.OuterOrderId = orderEntity.OrderNo;
            tradeInfo.PayPlatformNo = platform;
            tradeInfo.PayType = actionType;
            tradeInfo.Status = TransactionStatus.Unpaid;
            tradeInfo.TransactionId = TransactionIdBuilder.NewTransactionId(tradeInfo.AppId);

            // 创建交易扩展实体
            var tradeExInfo = new TransactionExtendInfo();
            tradeExInfo.Description = orderEntity.Description;
            tradeExInfo.ExpiredTime = orderEntity.ExpiredTime;
            tradeExInfo.Title = orderEntity.Title;

            // 写入基础交易信息与扩展交易信息
            var dalExtend = DataLayerFactory.GetDataLayer<ITransactionExtendDal>();
            if (dal.Insert(tradeInfo) <= 0 || dalExtend.Insert(tradeExInfo) <= 0)
            {
                return new MethodResult(MethodResultCode.ServerFailed, "未能创建订单信息", oldTransaction);
            }

            return new MethodResult(MethodResultCode.Success,
                MethodResultMessage.Success,
                new Tuple<TransactionInfo, TransactionExtendInfo>(tradeInfo, tradeExInfo));
        }
    }
}
