using System.Collections.Generic;
using BookReader.Util;
using LiteDB;

namespace BookReader.Entity
{
    public class BaseSearchRuleEntity : IBaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 搜索名称
        /// </summary>
        public string SearchName { get; set; }

        /// <summary>
        /// 搜索URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 解析类型 0:Xpath 1:JsonPath
        /// </summary>
        public int SearchType { get; set; }

        /// <summary>
        /// 搜索列表
        /// </summary>
        [BsonRef(Constant.SearchRuleTable)]
        public List<SearchRuleEntity> SearchRules;
    }
}