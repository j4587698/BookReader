namespace BookReader.Entity
{
    public class SettingsEntity : IBaseEntity
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public object Value { get; set; }
    }
}