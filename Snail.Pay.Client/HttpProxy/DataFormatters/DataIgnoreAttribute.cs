using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 此参数不参与提交
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class DataIgnoreAttribute : Attribute
    {

    }
}
