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
        /// 是否为基础页，只有原始站可以为基础页，有且只有一个
        /// </summary>
        public bool IsBasePage { get; set; } = false;

        /// <summary>
        /// 搜索URL
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// 需要添加的http头
        /// </summary>
        public List<HeaderEntity> Headers { get; set; }

        /// <summary>
        /// 解析类型 0:Xpath 1:JsonPath
        /// </summary>
        public int SearchType { get; set; }

        /// <summary>
        /// 搜索列表
        /// </summary>
        public List<SearchRuleEntity> SearchRules { get; set; }
    }
}