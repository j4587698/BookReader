using System.Collections.Generic;
using BookReader.Parser;

namespace BookReader.Network
{
    public class BookManager
    {

        public static List<string> GetSearchList(string key)
        {
            HttpHelper helper = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = string.Format("http://book.km.com/index.php?c=search&a=thinkup&keyword={0}&atype=", key),
                Method = "GET",
                Referer = "http://book.km.com/",
                Host = "book.km.com",
                ResultType = ResultType.String
            };
            item.Header.Add("X-Requested-With", "XMLHttpRequest");
            HttpResult result = helper.GetHtml(item);
            string resultStr = result.Html;
            return BookParser.ParseSearchList(resultStr);
        }

        public static void GetBookInfo()
        {

        }
    }
}