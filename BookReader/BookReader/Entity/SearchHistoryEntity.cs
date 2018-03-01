using System;

namespace BookReader.Entity
{
    public class SearchHistoryEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastSearchTime { get; set; }
    }
}