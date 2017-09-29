using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 配置文件接口类
    /// </summary>
    public interface IConfiguration
    {
        void Fill(Stream stream);
    }
}
