using System.Configuration;

namespace Snail.Pay.Config
{
    /// <summary>
    /// 支付平台配置
    /// </summary>    
    public class PayPlatformSetting : ConfigurationElement
    {
        /// <summary>
        /// 获取或设置平台名称
        /// </summary>
        [ConfigurationProperty(name: "name")]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        /// <summary>
        /// 获取或设置程序集
        /// </summary>
        [ConfigurationProperty(name: "assembly")]
        public string Assembly
        {
            get { return this["assembly"].ToString(); }
            set { this["assembly"] = value; }
        }

        /// <summary>
        /// 获取或设置是否启用
        /// </summary>
        [ConfigurationProperty(name: "enable")]
        public bool Enable
        {
            get { return (bool)this["enable"]; }
            set { this["enable"] = value; }
        }
    }
}
