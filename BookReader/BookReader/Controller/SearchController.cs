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
            List<HeaderEntity> headers = new List<HeaderEntity>()
            {
                new HeaderEntity()
                {
                    Key = "X-Requested-With",
                    Value = "XMLHttpRequest"
                }
            };
            var resuponse = HtmlManager.GetHtml(string.Format("http://book.km.com/index.php?c=search&a=thinkup&keyword={0}&atype=", key), headers);
            if (resuponse.responseCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resuponse.response) )
            {
                return JsonParser.GetList(resuponse.response, "$.list[*].title_def");
            }
            return null;
        }

        public IEnumerable<HotSearchEntity> GetHotSearch(Action<IEnumerable<HotSearchEntity>> updateSuccessAction)
        {
            Task.Factory.StartNew(() =>
            {
                List<HeaderEntity> headers = new List<HeaderEntity>()
                {
                    new HeaderEntity()
                    {
                        Key = "X-Requested-With",
                        Value = "XMLHttpRequest"
                    }
                };
                var resuponse = HtmlManager.GetHtml("http://book.km.com/index.php?c=search&a=thinkup&keyword=&atype=", headers);
                if (resuponse.responseCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resuponse.response))
                {
                    var hots = JsonParser.GetList(resuponse.response, "$.list[*].title_def").Select(x => new HotSearchEntity() { Name = x });
                    SearchManager.ReplaceHotSearch(hots);
                    //return hots;
                    updateSuccessAction?.Invoke(hots);
                }
            });    
            return SearchManager.GetAllHotSearch();
        }

        public List<SearchResultEntity> GetSearchResult(string key)
        {
            var searchrule = RuleManager.GetSearchRule(x => x.IsBasePage);
            if (searchrule == null)
            {
                throw new Exception("未找到基础搜索规则");
            }
            searchrule.BaseUrl = String.Format(searchrule.BaseUrl, key);
            var response = HtmlManager.GetHtml(searchrule.BaseUrl, searchrule.Headers);
            if (response.responseCode == HttpStatusCode.OK && !string.IsNullOrEmpty(response.response))
            {
                List<SearchResultEntity> searchResults = new List<SearchResultEntity>();
                foreach (SearchRuleEntity rule in searchrule.SearchRules)
                {
                    if (searchrule.SearchType == 0)
                    {
                        List<string> xpaths = new List<string>()
                        {
                            rule.BookNamePath,
                            rule.CoverUrl,
                            rule.BookUrlPath,
                            rule.AuthorPath,
                            rule.SummaryPath
                        };
                        var results = HtmlParser.GetList(response.response, rule.BasePath, xpaths);
                        foreach (var result in results)
                        {
                            SearchResultEntity searchResult = new SearchResultEntity()
                            {
                                BookName = result[0],
                                CoverUrl = result[1],
                                BookUrl = result[2],
                                Author = result[3],
                                Summany = result[4]
                            };
                            if (rule.NeedAddBaseUrl == 0 && !searchResult.BookUrl.StartsWith("http"))
                            {
                                searchResult.BookUrl = $"{rule.BaseUrl}/{searchResult.BookUrl}";
                            }

                            if (rule.NeedAddBaseUrl == 1)
                            {
                                searchResult.BookUrl = $"{rule.BaseUrl}/{searchResult.BookUrl}";
                            }
                            searchResults.Add(searchResult);
                        } 
                    }   
                }
                return searchResults;
            }

            return null;
        }
    }
}