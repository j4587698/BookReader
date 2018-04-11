using System;
using System.Collections.Generic;
using System.Linq;
using BookReader.Entity;
using BookReader.Util;

namespace BookReader.DB
{
    public class SearchManager
    {
        #region 历史记录相关
        /// <summary>
        /// 获取所有的历史搜索
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SearchHistoryEntity> GetAllHistory()
        {
            return LiteDBHelper.Instance.GetAllData<SearchHistoryEntity>(Constant.HistoryTable).OrderByDescending(x => x.LastSearchTime);
        }

        /// <summary>
        /// 添加历史记录
        /// </summary>
        /// <param name="historyStr"></param>
        public static void AddHistory(string historyStr)
        {
            int id = LiteDBHelper.Instance.GetAllData<SearchHistoryEntity>(Constant.HistoryTable).Where(x => x.Name == historyStr).Select(x => x.Id).FirstOrDefault();
            var history = new SearchHistoryEntity()
            {
                Id = id,
                Name = historyStr,
                LastSearchTime = DateTime.Now
            };
            LiteDBHelper.Instance.InsertOrUpdate(Constant.HistoryTable, history);
        }

        /// <summary>
        /// 删除历史记录
        /// </summary>
        public static void DelAllHistory()
        {
            LiteDBHelper.Instance.DeleteAll<SearchHistoryEntity>(Constant.HistoryTable);
        }
        #endregion

        #region 热搜相关

        /// <summary>
        /// 获取所有热搜
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<HotSearchEntity> GetAllHotSearch()
        {
            return LiteDBHelper.Instance.GetAllData<HotSearchEntity>(Constant.HotSearchTable);
        }

        /// <summary>
        /// 更新热搜
        /// </summary>
        /// <param name="hotSearchs"></param>
        public static void ReplaceHotSearch(IEnumerable<HotSearchEntity> hotSearchs)
        {
            LiteDBHelper.Instance.DeleteAll<HotSearchEntity>(Constant.HotSearchTable);
            LiteDBHelper.Instance.InsertOrUpdateBatch(Constant.HotSearchTable, hotSearchs);
        }

        #endregion

    }
}