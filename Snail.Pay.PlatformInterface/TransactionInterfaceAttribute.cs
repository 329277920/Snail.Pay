using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.PlatformInterface
{
    /// <summary>
    /// 支付接口属性定义
    /// </summary>
    public class TransactionInterfaceAttribute : Attribute
    {
        /// <summary>
        /// 定义交易平台
        /// </summary>
        public string TransactionPlatform { get; set; }

        /// <summary>
        /// 定义交易行为
        /// </summary>
        public string TransactionActionType { get; set; }
    }
}
