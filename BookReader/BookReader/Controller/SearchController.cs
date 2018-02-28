using System.Collections.Generic;
using System.Net;
using BookReader.Network;
using BookReader.Parser;

namespace BookReader.Controller
{
    public class SearchController
    {
        public static List<string> GetSearchList(string key)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                {"Method", "Get"},
                {"Referer", "http://book.km.com/"},
                {"Host", "book.km.com"},
                {"X-Requested-With", "XMLHttpRequest"}
            };
            var resuponse = HtmlManager.GetHtml(string.Format("http://book.km.com/index.php?c=search&a=thinkup&keyword={0}&atype=", key), headers);
            if (resuponse.responseCode == HttpStatusCode.OK || !string.IsNullOrEmpty(resuponse.response) )
            {
                return JsonParser.GetList(resuponse.response, "$.list[*].title_def");
            }
            else
            {
                return null;
            }
        }
    }
}