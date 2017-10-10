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
            data.SetValue("body", extendInfo.Description ?? PayConfig.Current.Description);//商品描述
            // data.SetValue("attach", "test");//附加数据
            data.SetValue("out_trade_no", tradeInfo.OuterOrderId);//随机字符串
            data.SetValue("total_fee", tradeInfo.Amount);//总金额
            data.SetValue("time_start", PayUtils.FormatDateTime(tradeInfo.PayStartTime));//交易起始时间
            data.SetValue("time_expire", PayUtils.FormatDateTime(tradeInfo.PayStartTime.AddMinutes(extendInfo.ExpiredTime <= 0 ?
                PayConfig.Current.ExpiredTime : extendInfo.ExpiredTime)));//交易结束时间
            data.SetValue("goods_tag", extendInfo.Title ?? PayConfig.Current.Title);//商品标记
            data.SetValue("trade_type", "NATIVE");//交易类型
            data.SetValue("product_id", extendInfo.ProductId);//商品ID
            WxPayData result = WxPayApi.UnifiedOrder(data);//调用统一下单接口

            // 校验是否完成
            var retCode = result.GetValue("return_code") as string;
            if (retCode != "SUCCESS")
            {
                return new MethodResult(MethodResultCode.CallFailed, string.Format("the order wx h5pay interface call failed.code:{0},msg:{1}",
                    retCode, result.GetValue("return_msg")));
            }

            var resCode = result.GetValue("result_code") as string;
            if (resCode != "SUCCESS")
            {
                return new MethodResult(MethodResultCode.CallFailed, string.Format("the order wx h5pay interface call failed.err_code:{0},err_code_des:{1}",
                    result.GetValue("err_code"), result.GetValue("err_code_des")));
            }
           
            // 返回统一下单接口返回的二维码链接
            return new MethodResult(MethodResultCode.Success, 
                MethodResultMessage.Success, 
                result.GetValue("code_url"));
        }
    }
}
