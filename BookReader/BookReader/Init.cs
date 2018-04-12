using System.Collections.Generic;
using System.IO;
using BookReader.DB;
using BookReader.Entity;
using BookReader.Util;
using XamStorage;

namespace BookReader
{
    /// <summary>
    /// 初始化类
    /// </summary>
    public class Init
    {
        public static void InitApp()
        {
#if DEBUG
            File.Delete(Path.Combine(FileSystem.Current.PersonalStorage.Path, "db.db"));
#endif
            if (!SettingsManager.Get<bool>(Constant.IsFirstStartup))
            {
                InitSearch();
                SettingsManager.Set(Constant.IsFirstStartup, false);
            }
            
        }

        /// <summary>
        /// 初始化搜索规则
        /// </summary>
        private static void InitSearch()
        {
            var searchRules = new List<SearchRuleEntity>()
            {
                new SearchRuleEntity()
                {
                    BasePath = "//div[@class='book_info clearfix']",
                    BookUrlPath = "//div[@class='op clearfix']/a/@href",
                    CoverUrl = "//div[@class='imgbox']/img/@src",
                    BaseUrl = "http://book.km.com",
                    NeedAddBaseUrl = 0,
                    BookNamePath = "//em[@class='spec']",
                    SummaryPath = "//p[@class='book_con']",
                    AuthorPath = "//p[@class='book_intr']/span[1]/a"
                },
                new SearchRuleEntity()
                {
                    BasePath = "//ul[@class='clearfix lazyload_box']/li",
                    BookUrlPath = "//dl[@class='info']/dt/a/@href",
                    CoverUrl = "//div[@class='imgbox']/a/img/@_src",
                    BaseUrl = "http://book.km.com",
                    NeedAddBaseUrl = 0,
                    BookNamePath = "//dl[@class='info']/dt/a",
                    SummaryPath = "//dl[@class='info']/dd[@class='desc']/text()",
                    AuthorPath = "//dl[@class='info']/dd[1]/span"
                }
            };
            BaseSearchRuleEntity entity = new BaseSearchRuleEntity()
            {
                BaseUrl = "http://book.km.com/search.html?search_type=book&keyword={0}",
                SearchName = "小说大全",
                IsBasePage = true,
                SearchType = 0,
                SearchRules = searchRules
            };
            RuleManager.UpdateSearchRules(entity);
        }
    }
}