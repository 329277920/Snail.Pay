using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Config
{
    [ConfigurationCollection(typeof(PayPlatformSetting))]
    public class PayPlatformCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PayPlatformSetting();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PayPlatformSetting)element).Name;
        }
    }
}
