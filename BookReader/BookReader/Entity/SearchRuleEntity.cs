namespace BookReader.Entity
{
    public class SearchRuleEntity
    {
        /// <summary>
        /// 基础地址
        /// </summary>
        public string BasePath { get; set; }

        /// <summary>
        /// URL是否需要添加网站地址:0 自动检测(判断前7为是否为http://) 1 全部添加 2 全部不添加
        /// </summary>
        public int NeedAddBaseUrl { get; set; }

        /// <summary>
        /// 要添加的基础地址
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// URL的PATH
        /// </summary>
        public string BookUrlPath { get; set; }

        /// <summary>
        /// 书名的Path
        /// </summary>
        public string BookNamePath { get; set; }

        /// <summary>
        /// 封面地址
        /// </summary>
        public string CoverUrl { get; set; }

        /// <summary>
        /// 简介的Path
        /// </summary>
        public string SummaryPath { get; set; }

        /// <summary>
        /// 作者的Path
        /// </summary>
        public string AuthorPath { get; set; }
    }
}