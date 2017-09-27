using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model.Check
{
    /// <summary>
    /// 校验规则实体类
    /// </summary>
    internal class CheckRule<TEntity>
    {
        public Func<TEntity, dynamic> Value { get; set; }

        public Func<dynamic, bool> Rule { get; set; }

        public string ErrorMsg { get; set; }

        public CheckRule(Func<TEntity, dynamic> value, Func<dynamic, bool> rule, string errorMsg)
        {
            this.Value = value;
            this.Rule = rule;
            this.ErrorMsg = errorMsg;
        }
    }
}
