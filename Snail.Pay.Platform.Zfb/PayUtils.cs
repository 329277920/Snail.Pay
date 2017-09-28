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
        /// 存储 IAopClient 实例成员，只需初始化一次
        /// </summary>
        private static Lazy<IAopClient> LazyAopClient = new Lazy<IAopClient>(() =>
        {
            return new DefaultAopClient(
               PayConfig.Instance.PayUrl,
               PayConfig.Instance.AppId,
               PayConfig.Instance.RsaPrivateKey,
               PayConfig.Instance.DataFormat,
               PayConfig.Instance.Version,
               PayConfig.Instance.EncryptType,
               PayConfig.Instance.RsaPublicKey,
               PayConfig.Instance.Charset,
               false);
        }, true);

        /// <summary>
        /// 创建支付宝调用客户端
        /// </summary>
        /// <returns></returns>
        public static IAopClient CreateClient()
        {
            return LazyAopClient.Value;
        }

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
