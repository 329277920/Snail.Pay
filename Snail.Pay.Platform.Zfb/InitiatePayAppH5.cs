using Aop.Api;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Snail.Pay.Model;
using Snail.Pay.PlatformInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Snail.Pay.Platform.Zfb
{
    /// <summary>
    /// 支付宝手机网站支付
    /// </summary>
    [TransactionInterface(TransactionPlatform = TransactionPlatform.ZFB, TransactionActionType = TransactionActionType.H5)]
    public class InitiatePayAppH5 : IInitiatePay
    {
        public MethodResult Pay(TransactionInfo tradeInfo, TransactionExtendInfo extendInfo)
        {             
            // 组装业务参数model
            AlipayTradeWapPayModel model = new AlipayTradeWapPayModel();
            model.Body = extendInfo.Description ?? PayConfig.Current.Description;
            model.Subject = extendInfo.Title ?? PayConfig.Current.Title;
            model.TotalAmount = PayUtils.GetAmount(tradeInfo.Amount).ToString();
            model.OutTradeNo = tradeInfo.TransactionId;
            model.ProductCode = "QUICK_WAP_PAY";

            // 设置支付异常中断回调地址
            // model.QuitUrl = "";
            AlipayTradeWapPayRequest request = new AlipayTradeWapPayRequest();
            // 设置支付完成同步回调地址
            request.SetReturnUrl(PayConfig.Current.ReturnUrl);
            // 设置支付完成异步通知接收地址
            request.SetNotifyUrl(PayConfig.Current.NotifyUrl);
            // 将业务model载入到request
            request.SetBizModel(model);

            AlipayTradeWapPayResponse response = PayConfig.Current.NewClient().pageExecute(request, null, "post");
            return new MethodResult(MethodResultCode.Success,
                MethodResultMessage.Success, HttpUtility.HtmlEncode(response.Body));
        }
    }
}
