using System;

namespace BookReader.Entity
{
    public class HistoryEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime LastSearchTime { get; set; }
    }
}