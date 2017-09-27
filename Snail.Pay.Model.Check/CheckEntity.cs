using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model.Check
{
    /// <summary>
    /// 校验实体
    /// </summary>
    public class CheckEntity<TEntity>
    {
        private List<CheckRule<TEntity>> _rules = new List<CheckRule<TEntity>>();

        /// <summary>
        /// 为某个属性值添加校验规则
        /// </summary>        
        /// <param name="value">指定获取属性委托</param>
        /// <param name="rule">该属性的校验规则列表</param>
        /// <param name="errorMsg">在校验失败时返回错误提示信息</param>
        public void AddRule(Func<TEntity, dynamic> value, Func<dynamic, bool> rule, string errorMsg)
        {
            this._rules.Add(new CheckRule<TEntity>(value, rule, errorMsg));
        }

        /// <summary>
        /// 校验实体
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="failToExit">是否在某个属性校验失败后，立即退出校验，默认为True</param>
        /// <returns>返回校验是否成功，以及提示信息</returns>
        public Tuple<bool, string> Check(TEntity entity)
        {
            foreach (var rule in this._rules)
            {
                if (!rule.Rule(rule.Value(entity)))
                {
                    return new Tuple<bool, string>(false, rule.ErrorMsg);
                }
            }
            return new Tuple<bool, string>(true, null);
        }
    }
}
