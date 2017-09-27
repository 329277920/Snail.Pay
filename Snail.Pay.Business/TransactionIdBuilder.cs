using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Business
{
    /// <summary>
    /// 支付流水号生成器
    /// </summary>
    public class TransactionIdBuilder
    {
        private static Random _rd1;

        private static Random _rd2;

        static TransactionIdBuilder()
        {
            _rd1 = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
            _rd2 = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
        }

        /// <summary>
        /// 生成一个新的流水号
        /// </summary>
        /// <param name="appId">外部系统编号（备用）</param>
        /// <returns></returns>
        public static string NewTransactionId(string appId)
        {
            return string.Format("{0}{1}{2}",
                DateTime.Now.ToString("yyyyMMddhhmmhhsss"),
                _rd1.Next(100, 999),
                _rd2.Next(100, 999));
        }
    }
}
