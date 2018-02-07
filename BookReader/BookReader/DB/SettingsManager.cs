using System.Linq;
using BookReader.Entity;

namespace BookReader.DB
{
    public class SettingsManager
    {
        private const string SettingsTable = "settings";

        /// <summary>
        /// 获取设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get<T>(string key)
        {
            return LiteDBHelper.Instance.GetAllData<SettingsEntity<T>>(SettingsTable).Where(x => x.Key == key).Select(s => s.Value).FirstOrDefault();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(string key, T value)
        {
            var id = LiteDBHelper.Instance.GetAllData<SettingsEntity<T>>(SettingsTable).Where(x => x.Key == key).Select(x => x.Id).FirstOrDefault();
            SettingsEntity<T> se = new SettingsEntity<T>()
            {
                Id = id,
                Key = key,
                Value = value
            };
            LiteDBHelper.Instance.InsertOrUpdate(SettingsTable, se);
        }
    }
}