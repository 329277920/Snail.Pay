using Aop.Api;
using Aop.Api.Request;
using Aop.Api.Response;
using Snail.Pay.Model;
using Snail.Pay.PlatformInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Platform.Zfb
{
    /// <summary>
    /// 查询交易订单
    /// </summary>
    [TransactionInterface(TransactionPlatform = TransactionPlatform.ZFB, TransactionActionType = TransactionActionType.Query)]
    public class QueryDefault : IQuery
    {
        public MethodResult Query(TransactionInfo trade)
        {
            IAopClient client = PayUtils.CreateClient();

            AlipayTradeQueryRequest request = new AlipayTradeQueryRequest();
            request.BizContent = "{" +
            "\"out_trade_no\":\"" + trade.TransactionId + "\"," +
            "\"trade_no\":\"" + (trade.OuterOrderId ?? "") + "\"" +
            "  }";            
        
            return GetResult(client.Execute(request));
        }

        private MethodResult GetResult(AlipayTradeQueryResponse response)
        {
            if (response == null || response.Code?.Length <= 0)
            {
                return new MethodResult(MethodResultCode.CallFailed, string.Format("the zfb query interface does not return any information."));
            }
            if (response.Code != PayConsts.CallSuccess)
            {
                return new MethodResult(MethodResultCode.CallFailed, string.Format("the order zfb query interface call failed.code:{0},msg:{1},subcode:{2},submsg:{3}.",
                    response.Code, response.Msg, response.SubCode, response.SubMsg));
            }
            if (response.SubCode?.Length > 0)
            {
                return new MethodResult(MethodResultCode.CallFailed, string.Format("the order zfb query interface call failed.subcode:{0},submsg:{1}.", response.SubCode, response.SubMsg));
            }

            var result = new TransactionResultInfo();
            result.PayAmount = PayUtils.GetAmount(response.TotalAmount);
            result.PayPlatformNo = response.TradeNo;
            result.Success = response.TradeStatus == "TRADE_SUCCESS" || response.TradeStatus == "TRADE_FINISHED";

            return new MethodResult(MethodResultCode.Success, MethodResultMessage.Success, result);
        }
    }
}
