using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 交易扩展信息
    /// </summary>
    public class TransactionExtendInfo
    {
        /// <summary>
        /// 交易id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 交易描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 交易过期时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }
}
