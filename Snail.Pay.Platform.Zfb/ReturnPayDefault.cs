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
    /// 支付前端回调，默认实现
    /// </summary>
    [TransactionInterface(TransactionPlatform = TransactionPlatform.ZFB, TransactionActionType = TransactionActionType.Return)]
    public class ReturnPayDefault : IReturn
    {
        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="queryOrder">支付结果</param>      
        /// <returns>返回处理结果</returns>
        public MethodResult Return(string queryOrder)
        {
            var kv = Serializer.DictionaryDeserialize(queryOrder);
            if (kv?.Count <= 0)
            {
                return new MethodResult(MethodResultCode.RequestFailed, MethodResultMessage.RequestFailed);
            }
            bool flag = AlipaySignature.RSACheckV1(kv,
                PayConfig.Instance.RsaPublicKey,
                PayConfig.Instance.Charset,
                PayConfig.Instance.EncryptType, false);
            if (flag)
            {
                return new MethodResult(MethodResultCode.SignError, MethodResultMessage.SignError);
            }
            return new MethodResult(MethodResultCode.Success, MethodResultMessage.Success);
        }
    }
}
