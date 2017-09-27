using System.Configuration;
using System.Linq;
using System.Text;

namespace Snail.Pay.Config
{
    /// <summary>
    /// 支付配置管理器
    /// </summary>
    public class ConfigManager
    {
        private PayConfigSection _cfg;

        private ConfigManager()
        {
            _cfg = (PayConfigSection)ConfigurationManager.GetSection("payConfig");
        }

        public static ConfigManager Current = new ConfigManager();

        /// <summary>
        /// 获取支付平台提供者
        /// </summary>
        /// <returns></returns>
        public string[] GetPayPlatformProviders()
        {
            return (from item in _cfg.PayPlatformProviders.Cast<PayPlatformSetting>()
                    where item.Enable
                    select item.Assembly).ToArray();
        }

        /// <summary>
        /// 获取数据层提供者
        /// </summary>
        /// <returns></returns>
        public string GetPayDataLayerProvider()
        {
            return _cfg.DataLayer.Assembly;
        }

        /// <summary>
        /// 获取全局编码属性
        /// </summary>
        public Encoding Encoding { get { return Encoding.UTF8; } }

        /// <summary>
        /// 获取全局编码属性
        /// </summary>
        public string EncodingToString { get { return "utf-8"; } }
    }
}
