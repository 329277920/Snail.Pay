using Snail.Pay.Model;
using Snail.Pay.PlatformInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WxPayAPI;

namespace Snail.Pay.Platform.Wx
{
    /// <summary>
    /// 原生扫码支付
    /// </summary>
    [TransactionInterface(TransactionPlatform = TransactionPlatform.WX, TransactionActionType = TransactionActionType.QRCode)]
    public class InitiatePayNative : IInitiatePay
    {
        public MethodResult Pay(TransactionInfo tradeInfo, TransactionExtendInfo extendInfo)
        {
            WxPayData data = new WxPayData();
            data.SetValue("body", extendInfo.Description);//商品描述
            // data.SetValue("attach", "test");//附加数据
            data.SetValue("out_trade_no", tradeInfo.OuterOrderId);//随机字符串
            data.SetValue("total_fee", tradeInfo.Amount);//总金额
            data.SetValue("time_start", PayUtils.FormatDateTime(tradeInfo.PayStartTime));//交易起始时间
            data.SetValue("time_expire", PayUtils.FormatDateTime(tradeInfo.PayStartTime.AddMinutes(extendInfo.ExpiredTime <= 0 ?
                PayConfig.Current.ExpiredTime : extendInfo.ExpiredTime)));//交易结束时间
            data.SetValue("goods_tag", extendInfo.Title);//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", extendInfo.ProductId);//商品ID

            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口

            string url = result.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接

            // Log.Info(this.GetType().ToString(), "Get native pay mode 2 url : " + url);
            // return url;

            return new MethodResult(MethodResultCode.Success, MethodResultMessage.Success, url);
        }
    }
}
