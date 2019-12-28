namespace Base.Framework.Core.CacheManager.Objects.Options
{
    public class RedisOption
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }
        public int DefaultDb { get; set; }
    }
}
