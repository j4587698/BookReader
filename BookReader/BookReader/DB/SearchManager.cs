using System;
using System.Collections.Generic;
using System.Linq;
using BookReader.Entity;

namespace BookReader.DB
{
    public class SearchManager
    {
        public const string HistoryTable = "historysearch";

        /// <summary>
        /// 获取所有的历史搜索
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<HistoryEntity> GetAllHistory()
        {
            return LiteDBHelper.Instance.GetAllData<HistoryEntity>(HistoryTable).OrderByDescending(x => x.LastSearchTime);
        }

        public static void AddHistory(string historyStr)
        {
            int id = LiteDBHelper.Instance.GetAllData<HistoryEntity>(HistoryTable).Where(x => x.Name == historyStr).Select(x => x.Id).FirstOrDefault();
            var history = new HistoryEntity()
            {
                Id = id,
                Name = historyStr,
                LastSearchTime = DateTime.Now
            };
            LiteDBHelper.Instance.InsertOrUpdate(HistoryTable, history);
        }

        public static void DelAllHistory()
        {
            LiteDBHelper.Instance.DeleteAll<HistoryEntity>(HistoryTable);
        }
    }
}