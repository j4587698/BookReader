using System.Linq;
using BookReader.Entity;
using BookReader.Util;

namespace BookReader.DB
{
    public class SettingsManager
    {
        /// <summary>
        /// 获取设置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            return LiteDBHelper.Instance.GetAllData<SettingsEntity<T>>(Constant.SettingsTable).Where(x => x.Key == key).Select(s => s.Value).FirstOrDefault();
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(string key, T value)
        {
            var id = LiteDBHelper.Instance.GetAllData<SettingsEntity<T>>(Constant.SettingsTable).Where(x => x.Key == key).Select(x => x.Id).FirstOrDefault();
            SettingsEntity<T> se = new SettingsEntity<T>()
            {
                Id = id,
                Key = key,
                Value = value
            };
            LiteDBHelper.Instance.InsertOrUpdate(Constant.SettingsTable, se);
        }
    }
}