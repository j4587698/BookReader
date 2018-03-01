using System;
using System.Collections.Generic;
using System.Linq;
using BookReader.Entity;

namespace BookReader.DB
{
    public class SearchManager
    {
        public const string HistoryTable = "historysearch";

        public const string HotSearchTable = "hotsearch";

        /// <summary>
        /// 获取所有的历史搜索
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<SearchHistoryEntity> GetAllHistory()
        {
            return LiteDBHelper.Instance.GetAllData<SearchHistoryEntity>(HistoryTable).OrderByDescending(x => x.LastSearchTime);
        }

        public static void AddHistory(string historyStr)
        {
            int id = LiteDBHelper.Instance.GetAllData<SearchHistoryEntity>(HistoryTable).Where(x => x.Name == historyStr).Select(x => x.Id).FirstOrDefault();
            var history = new SearchHistoryEntity()
            {
                Id = id,
                Name = historyStr,
                LastSearchTime = DateTime.Now
            };
            LiteDBHelper.Instance.InsertOrUpdate(HistoryTable, history);
        }

        public static void DelAllHistory()
        {
            LiteDBHelper.Instance.DeleteAll<SearchHistoryEntity>(HistoryTable);
        }

        public static IEnumerable<HotSearchEntity> GetAllHotSearch()
        {
            return LiteDBHelper.Instance.GetAllData<HotSearchEntity>(HotSearchTable);
        }

        public static void ReplaceHotSearch(IEnumerable<HotSearchEntity> hotSearchs)
        {
            LiteDBHelper.Instance.DeleteAll<HotSearchEntity>(HotSearchTable);
            LiteDBHelper.Instance.InsertOrUpdateBatch(HotSearchTable, hotSearchs);
        }
    }
}