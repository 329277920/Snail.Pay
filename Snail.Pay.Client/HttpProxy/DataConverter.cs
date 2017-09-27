using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 数据转换器
    /// </summary>
    public sealed class DataConverter
    {
        public static T Convert<T>(object value, T defValue = default(T))
        {
            if (value == null)
            {
                return defValue;
            }
            var strValue = value.ToString();
            var t = typeof(T);

            switch (t.FullName)
            {
                case "System.String":
                    return (T)System.Convert.ChangeType(value.ToString(), t);
                case "System.Int32":
                    int v1 = 0;
                    if (!Int32.TryParse(strValue, out v1))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v1, t);
                case "System.Int64":
                    Int64 v2 = 0L;
                    if (!Int64.TryParse(strValue, out v2))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v2, t);
                case "System.Double":
                    double v3 = 0.0;
                    if (!double.TryParse(strValue, out v3))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v3, t);
                case "System.Boolean":
                    bool v4 = false;
                    if (!bool.TryParse(strValue, out v4))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v4, t);
                case "System.Decimal":
                    decimal v5 = 0.0m;
                    if (!decimal.TryParse(strValue, out v5))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v5, t);
                case "System.DateTime":
                    DateTime v6 = DateTime.Now;
                    if (!DateTime.TryParse(strValue, out v6))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v6, t);
                case "System.Byte":
                    Byte v7 = 0;
                    if (!Byte.TryParse(strValue, out v7))
                    {
                        return defValue;
                    }
                    return (T)System.Convert.ChangeType(v7, t);
            }
            return defValue;
        }

        /// <summary>
        /// 是否为基础类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsBasicType(Type t)
        {
            if (t.FullName == "System.String" || t.IsPrimitive)
            {
                return true;
            }
            return false;
        }
    }
}
