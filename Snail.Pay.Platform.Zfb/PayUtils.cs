using Aop.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Platform.Zfb
{
    public class PayUtils
    {         
        /// <summary>
        /// 支付金额分转元
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static decimal GetAmount(int amount)
        {
            return ((decimal)amount / 100);
        }

        /// <summary>
        /// 支付金额元转分
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static int GetAmount(decimal amount)
        {
            return (int)(amount * 100);
        }

        /// <summary>
        /// 支付金额元转分
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static int GetAmount(string amount)
        {
            decimal decAmount = 0.0m;
            if (decimal.TryParse(amount, out decAmount))
            {
                return GetAmount(decAmount);
            }
            return 0;
        }
    }
}
