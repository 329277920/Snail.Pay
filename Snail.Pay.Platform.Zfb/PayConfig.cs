using Aop.Api;
using Snail.Pay.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Platform.Zfb
{
    /// <summary>
    /// 支付平台配置信息
    /// </summary>
    public class PayConfig
    {
        #region 私有成员

        private static string FilePath
        {
            get { return Common.PathUnity.GetFilePath("configs/Zfb.PayConfig.xml"); }
        }

        /// <summary>
        /// 存储支付宝支付SDK客户端
        /// </summary>
        private Lazy<IAopClient> _lazyClient = new Lazy<IAopClient>(() =>
        {
            var config = Current;
            return new DefaultAopClient(
              config.PayUrl,
              config.AppId,
              config.RsaPrivateKey,
              config.DataFormat,
              config.Version,
              config.EncryptType,
              config.RsaPublicKey,
              config.Charset,
              false);
        }, true);

        #endregion

        /// <summary>
        /// 获取支付宝SDK客户端，该客户端在配置文件发生变更时自动重建
        /// </summary>
        /// <returns></returns>
        public IAopClient NewClient()
        {
            return _lazyClient.Value;
        }

        /// <summary>
        /// 获取当前的配置文件实例
        /// </summary>
        public static PayConfig Current
        {
            get
            {               
                if (string.IsNullOrEmpty(FilePath))
                {
                    throw new Model.KnownException("can not found the file with path 'configs/Zfb.PayConfig.xml'.");
                }
                return ConfigurationManager.Get<PayConfig>(FilePath);
            }
        }

        /// <summary>
        /// APPID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 支付网关
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// RSA公钥（支付宝）
        /// </summary>
        public string RsaPublicKey { get; set; }

        /// <summary>
        /// RSA私钥，商户
        /// </summary>
        public string RsaPrivateKey { get; set; }

        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 同步通知地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 加密类型（RSA2，AES）
        /// </summary>
        public string EncryptType { get; set; }

        /// <summary>
        /// 数据格式，默认为json
        /// </summary>
        public string DataFormat { get; set; }

        /// <summary>
        /// 接口版本，默认为1.0
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 字符编码格式
        /// </summary>
        public string Charset { get; set; }
     
        /// <summary>
        /// 交易描述（可以由业务系统传入）
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品标题（可以由业务系统传入）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 交易过期时间（单位分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
        
    }
}
