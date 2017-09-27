using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Snail.Pay.Model.Check
{
    /// <summary>
    /// 改类提供了最常用的数据校验规则
    /// </summary>
    public class CheckUnity
    {
        /// <summary>
        /// 校验不能为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Required(object value)
        {
            var strVal = value?.ToString();
            if (string.IsNullOrWhiteSpace(strVal))
            {
                return false;
            }
            return true;
        }

        private static Lazy<Regex> RegEmail = new Lazy<Regex>(() => { return new Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase); });
        /// <summary>
        /// 校验邮箱地址
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsEmail(object data)
        {
            return RegEmail.Value.IsMatch(data?.ToString());
        }

        /// <summary>
        /// 校验邮箱地址，忽略空值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsEmailIgnoreEmpty(object data)
        {
            return string.IsNullOrWhiteSpace(data?.ToString()) || RegEmail.Value.IsMatch(data?.ToString());
        }

        private static Lazy<Regex> RegMobile = new Lazy<Regex>(() => { return new Regex(@"^(13[0-9]|14[5|7]|15[0|1|2|3|5|6|7|8|9]|18[0|1|2|3|5|6|7|8|9])\d{8}$", RegexOptions.IgnoreCase); });
        /// <summary>
        /// 校验手机号码
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsMobile(object data)
        {
            return RegMobile.Value.IsMatch(data?.ToString());
        }
        /// <summary>
        /// 校验手机号码，忽略空值
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsMobileIgnoreEmpty(object data)
        {
            return string.IsNullOrWhiteSpace(data?.ToString()) || RegMobile.Value.IsMatch(data?.ToString());
        }

        private static bool InvokeCheck(Func<object, bool> method, object value)
        {
            try
            {
                if (!Required(value))
                {
                    return true;
                }
                return method(value);
            }
            // 报异常直接返回false 
            catch { return false; }
        }
    }
}
