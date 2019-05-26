using System.Net.Http;
using NUnit.Framework;
using TogglerAdmin.Integration.Tests.Utils;

namespace TogglerAdmin.Integration.Tests
{
    public class BaseTest
    {
        protected HttpClient Client;
        protected MongoDbDataSeed DataSeed;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory<Api.Startup>();
            Client = factory.CreateClient();

            DataSeed = new MongoDbDataSeed(
                GetParameter("mongodb_connection_string"),
                GetParameter("mongodb_database"));

            DataSeed.Cleanup();
        }

        [TearDown]
        public void TearDown()
        {
            DataSeed.Cleanup();
        }

        protected string GetParameter(string name, string defaultValue = null)
        {
            if (TestContext.Parameters.Exists(name))
            {
                return TestContext.Parameters[name];
            }

            return defaultValue;
        }
    }
}
