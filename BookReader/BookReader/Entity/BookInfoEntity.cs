using System;

namespace BookReader.Entity
{
    /// <summary>
    /// 书籍信息类
    /// </summary>
    public class BookInfoEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string ConverPath { get; set; }

        public string BookName { get; set; }

        public string Author { get; set; }

        public string LastReaded { get; set; }

        public string LastChapter { get; set; }

        public DateTime LastModify { get; set; }
    }
}