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
    /// App端发起支付
    /// </summary>    
    [TransactionInterface(TransactionPlatform = TransactionPlatform.ZFB, TransactionActionType = TransactionActionType.App)]
    public class InitiatePayApp : IInitiatePay
    {
        public MethodResult Pay(TransactionInfo tradeInfo, TransactionExtendInfo extendInfo)
        {             
            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            model.Body = extendInfo.Description ?? PayConfig.Current.Description;
            model.Subject = extendInfo.Title ?? PayConfig.Current.Title;
            model.TotalAmount = PayUtils.GetAmount(tradeInfo.Amount).ToString();
            model.ProductCode = "QUICK_MSECURITY_PAY";
            model.OutTradeNo = tradeInfo.TransactionId;            
            model.TimeoutExpress = (extendInfo.ExpiredTime <= 0 ? PayConfig.Current.ExpiredTime : extendInfo.ExpiredTime) + "m";
            request.SetBizModel(model);
            request.SetNotifyUrl(PayConfig.Current.NotifyUrl);
            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeAppPayResponse response = PayConfig.Current.NewClient().SdkExecute(request);                        
            return new MethodResult(MethodResultCode.Success,
                MethodResultMessage.Success, HttpUtility.HtmlEncode(response.Body));
        }
    }
}
