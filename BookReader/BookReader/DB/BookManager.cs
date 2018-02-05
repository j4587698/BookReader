using System.Collections.Generic;
using BookReader.Entity;

namespace BookReader.DB
{
    public class BookManager
    {

        private const string BookInfoTable = "bookinfo";

        /// <summary>
        /// 获取所有的书信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BookInfoEntity> GetAllBookInfos()
        {
            return LiteDBHelper.Instance.GetAllData<BookInfoEntity>(BookInfoTable);
        }

    }
}