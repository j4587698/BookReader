using System.Collections.Generic;
using BookReader.Entity;
using BookReader.Util;

namespace BookReader.DB
{
    public class BookManager
    {
        /// <summary>
        /// 获取所有的书信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<BookInfoEntity> GetAllBookInfos()
        {
            return LiteDBHelper.Instance.GetAllData<BookInfoEntity>(Constant.BookInfoTable);
        }

    }
}