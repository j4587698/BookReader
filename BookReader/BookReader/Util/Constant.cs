namespace BookReader.Util
{
    public class Constant
    {
        #region 表名常量

        /// <summary>
        /// 书籍信息表
        /// </summary>
        public const string BookInfoTable = "bookinfo";

        /// <summary>
        /// 历史搜索表
        /// </summary>
        public const string HistoryTable = "historysearch";

        /// <summary>
        /// 热门搜索表
        /// </summary>
        public const string HotSearchTable = "hotsearch";

        /// <summary>
        /// 基本搜索规则表
        /// </summary>
        public const string BaseSearchRuleTable = "basesearchrule";

        /// <summary>
        /// 设置表
        /// </summary>
        public const string SettingsTable = "settings";

        #endregion


        #region 字段常量

        /// <summary>
        /// 是否为第一次启动
        /// </summary>
        public const string IsFirstStartup = "IsFirstStartup";

        #endregion
    }
}