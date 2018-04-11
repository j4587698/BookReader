using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BookReader.Entity;
using BookReader.Util;

namespace BookReader.DB
{
    public class RuleManager
    {
        #region 搜索相关规则

        /// <summary>
        /// 更新搜索规则
        /// </summary>
        /// <param name="baseSearchRules"></param>
        public static void UpdateSearchRules(params BaseSearchRuleEntity[] baseSearchRules)
        {
            LiteDBHelper.Instance.DeleteAll<BaseSearchRuleEntity>(Constant.BaseSearchRuleTable);
            LiteDBHelper.Instance.InsertOrUpdateBatch(Constant.BaseSearchRuleTable, baseSearchRules);
        }

        public static IEnumerable<BaseSearchRuleEntity> GetAllSearchRules()
        {
            return LiteDBHelper.Instance.GetAllData<BaseSearchRuleEntity>(Constant.BaseSearchRuleTable);
        }

        public static BaseSearchRuleEntity GetSearchRule(Expression<Func<BaseSearchRuleEntity, bool>> predicate)
        {
            return LiteDBHelper.Instance.GetData(Constant.BaseSearchRuleTable, predicate).FirstOrDefault();
        }

        public static IEnumerable<BaseSearchRuleEntity> GetSearchRules(Expression<Func<BaseSearchRuleEntity, bool>> predicate)
        {
            return LiteDBHelper.Instance.GetData(Constant.BaseSearchRuleTable, predicate);
        }

        #endregion
    }
}