using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using BookReader.Entity;
using LiteDB;
using Xamarin.Forms;
using XamStorage;

namespace BookReader.DB
{
    public class LiteDBHelper
    {
        private readonly LiteDatabase _db;

        private static LiteDBHelper _instance;

        public static LiteDBHelper Instance => _instance ?? (_instance = new LiteDBHelper());

        private LiteDBHelper()
        {
            var dbUrl = Path.Combine(FileSystem.Current.PersonalStorage.Path, "db.db"); 
            _db = new LiteDatabase(dbUrl);
        }

        /// <summary>
        /// 插入或更新数据库内容
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="data">要处理的数据</param>
        public void InsertOrUpdate<T>(string tableName, T data) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            if (data.Id == 0)
            {
                col.Insert(data);
            }
            else
            {
                col.Update(data);
            }
        }

        public void InsertOrUpdateBatch<T>(string tableName, IEnumerable<T> datas) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            var insertDatas = datas.Where(x => x.Id == 0);
            if (insertDatas.Any())
            {
                col.Insert(insertDatas);
            }
            var updateDatas = datas.Where(x => x.Id != 0);
            if (updateDatas.Any())
            {
                col.Update(updateDatas);
            }
        }

        public void Delete<T>(string tableName, T data) where T : IBaseEntity
        {
            Delete<T>(tableName, data.Id);
        }

        public void Delete<T>(string tableName, int id) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            col.Delete(id);
        }

        internal void Delete<T>(string tableName, IEnumerable<T> datas) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            col.Delete(Query.In("_id", IDBDaoToBsonArray(datas)));
        }

        private BsonArray IDBDaoToBsonArray<T>(IEnumerable<T> datas) where T : IBaseEntity
        {
            BsonArray ba = new BsonArray();
            foreach (var data in datas)
            {
                ba.Add(data.Id);
            }
            return ba;
        }

        public void DeleteAll<T>(string tableName) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            col.Delete(Query.All());
        }

        public T GetFirstData<T>(string tableName) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            return col.FindOne(Query.All());
        }

        public IEnumerable<T> GetAllData<T>(string tableName) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            return col.FindAll();
        }

        public IEnumerable<T> GetAllData<T>(string tableName, string includePath1) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            return col.Include(includePath1).FindAll();
        }

        public IEnumerable<T> GetAllData<T>(string tableName, string includePath1, string includePath2) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            return col.Include(includePath1).Include(includePath2).FindAll();
        }

        public IEnumerable<T> GetAllData<T>(string tableName, params string[] includePaths) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            foreach (var includePath in includePaths)
            {
                col = col.Include(includePath);
            }
            return col.FindAll();
        }

        public List<T> GetAllDataToList<T>(string tableName) where T : IBaseEntity
        {
            return GetAllData<T>(tableName).ToList();
        }

        public T GetDataById<T>(string tableName, int id) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            return col.FindById(id);
        }

        public IEnumerable<T> GetData<T>(string tableName, Expression<Func<T, bool>> predicate, int skip = 0, int limit = Int32.MaxValue) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            return col.Find(predicate, skip, limit);
        }

        public IEnumerable<T> GetData<T>(string tableName, Expression<Func<T, bool>> predicate,int skip = 0, int limit = Int32.MaxValue, params string[] includePaths) where T : IBaseEntity
        {
            var col = _db.GetCollection<T>(tableName);
            foreach (var includePath in includePaths)
            {
                col.Include(includePath);
            }
            return col.Find(predicate, skip, limit);
        }
    }
}