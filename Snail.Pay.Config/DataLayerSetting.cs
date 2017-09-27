using System.Configuration;

namespace Snail.Pay.Config
{
    /// <summary>
    /// 数据访问层配置
    /// </summary>
    public class DataLayerSetting : ConfigurationElement
    {
        /// <summary>
        /// 获取或设置备注名称
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
        [ConfigurationProperty(name: "assembly", IsRequired = true)]
        public string Assembly
        {
            get { return this["assembly"].ToString(); }
            set { this["assembly"] = value; }
        }
    }
}
