using Microservices.Catalog.Settings.Abstract;

namespace Microservices.Catalog.Settings.Concrete
{
    public class DatabaseSettings : IDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
    }
}
