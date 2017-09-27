using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 支付平台定义
    /// </summary>
    public class TransactionPlatform
    {
        /// <summary>
        /// 微信支付
        /// </summary>
        public const string WX = "wx";

        /// <summary>
        /// 支付宝支付
        /// </summary>
        public const string ZFB = "zfb";
    }
}
