namespace BookReader.Entity
{
    public class SearchRuleEntity : IBaseEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// 基础地址
        /// </summary>
        public string BasePath { get; set; }


        /// <summary>
        /// URL的PATH
        /// </summary>
        public string BookUrlPath { get; set; }

        /// <summary>
        /// 书名的Path
        /// </summary>
        public string BookNamePath { get; set; }

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