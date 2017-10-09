using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Platform.Wx
{
    public class PayUtils
    {
        public static string FormatDateTime(DateTime time)
        {
            return time.ToString("yyyyMMddHHmmss");
        }
    }
}
