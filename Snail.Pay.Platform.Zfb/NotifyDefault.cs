using Aop.Api.Util;
using Snail.Pay.Common;
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
    /// 默认异步通知实现类
    /// </summary>
    [TransactionInterface(TransactionPlatform = TransactionPlatform.ZFB, TransactionActionType = TransactionActionType.Notify)]
    public class NotifyDefault : INotify
    {
        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="queryOrder">支付结果</param>      
        /// <returns>返回处理结果</returns>
        public MethodResult Notify(string queryOrder)
        {
            var kv = Serializer.DictionaryDeserialize(queryOrder);
            if (kv?.Count <= 0)
            {
                return new MethodResult(MethodResultCode.RequestFailed, MethodResultMessage.RequestFailed);
            }
            bool flag = AlipaySignature.RSACheckV1(kv,
                PayConfig.Current.RsaPublicKey,
                PayConfig.Current.Charset,
                PayConfig.Current.EncryptType, false);
            if (flag)
            {
                return new MethodResult(MethodResultCode.SignError, MethodResultMessage.SignError);
            }
            return new MethodResult(MethodResultCode.Success, MethodResultMessage.Success);
        }
    }
}
