using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BookReader.DB;
using BookReader.Entity;
using BookReader.Network;
using BookReader.Parser;

namespace BookReader.Controller
{
    public class SearchController
    {
        public List<string> GetSearchList(string key)
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
            return null;
        }

        public IEnumerable<HotSearchEntity> GetHotSearch(Action<IEnumerable<HotSearchEntity>> updateSuccessAction)
        {
            Task.Factory.StartNew(() =>
            {
                Dictionary<string, string> headers = new Dictionary<string, string>()
                {
                    {"Method", "Get"},
                    {"Referer", "http://book.km.com/"},
                    {"Host", "book.km.com"},
                    {"X-Requested-With", "XMLHttpRequest"}
                };
                var resuponse = HtmlManager.GetHtml("http://book.km.com/index.php?c=search&a=thinkup&keyword=&atype=", headers);
                if (resuponse.responseCode == HttpStatusCode.OK || !string.IsNullOrEmpty(resuponse.response))
                {
                    var hots = JsonParser.GetList(resuponse.response, "$.list[*].title_def").Select(x => new HotSearchEntity() { Name = x });
                    SearchManager.ReplaceHotSearch(hots);
                    //return hots;
                    updateSuccessAction?.Invoke(hots);
                }
            });
            
            
            return SearchManager.GetAllHotSearch();
        }
    }
}