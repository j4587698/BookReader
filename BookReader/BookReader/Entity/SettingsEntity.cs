namespace BookReader.Entity
{
    public class SettingsEntity<T> : IBaseEntity
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public T Value { get; set; }
    }
}