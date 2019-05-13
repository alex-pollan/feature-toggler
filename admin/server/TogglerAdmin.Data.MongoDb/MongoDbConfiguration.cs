namespace TogglerAdmin.Data.MongoDb
{
    public class MongoDbConfiguration
    {
        public MongoDbConfiguration(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }

        public string ConnectionString { get; }
        public string DatabaseName { get; internal set; }
    }
}