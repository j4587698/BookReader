using System;
using System.Collections.Generic;
using System.Linq;
using BookReader.Entity;
using BookReader.Util;

namespace BookReader.DB
{
    public class SearchManager
    {

        /// <summary>
        /// 获取所有的历史搜索
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SearchHistoryEntity> GetAllHistory()
        {
            return LiteDBHelper.Instance.GetAllData<SearchHistoryEntity>(Constant.HistoryTable).OrderByDescending(x => x.LastSearchTime);
        }

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

        public static void DelAllHistory()
        {
            LiteDBHelper.Instance.DeleteAll<SearchHistoryEntity>(Constant.HistoryTable);
        }

        public static IEnumerable<HotSearchEntity> GetAllHotSearch()
        {
            return LiteDBHelper.Instance.GetAllData<HotSearchEntity>(Constant.HotSearchTable);
        }

        public static void ReplaceHotSearch(IEnumerable<HotSearchEntity> hotSearchs)
        {
            LiteDBHelper.Instance.DeleteAll<HotSearchEntity>(Constant.HotSearchTable);
            LiteDBHelper.Instance.InsertOrUpdateBatch(Constant.HotSearchTable, hotSearchs);
        }
    }
}