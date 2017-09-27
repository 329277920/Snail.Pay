using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Config
{    
    public class PayConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("dataLayer", IsRequired = true)]
        public DataLayerSetting DataLayer
        {
            get { return (DataLayerSetting)this["dataLayer"]; }
            set { this["dataLayer"] = value; }
        }

        [ConfigurationProperty("payProviders", IsRequired = true)]
        public PayPlatformCollection PayPlatformProviders
        {
            get { return (PayPlatformCollection)this["payProviders"]; }
            set { this["payProviders"] = value; }
        }
    }
}
