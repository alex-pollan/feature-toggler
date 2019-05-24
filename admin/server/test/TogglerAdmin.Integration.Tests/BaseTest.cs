using System.Net.Http;
using NUnit.Framework;
using TogglerAdmin.Integration.Tests.Utils;

namespace TogglerAdmin.Integration.Tests
{
    public class BaseTest
    {
        protected HttpClient Client;
        protected MongoDbDataSeed Seed;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var factory = new CustomWebApplicationFactory<Api.Startup>();
            Client = factory.CreateClient();

            Seed = new MongoDbDataSeed(
                GetParameter("mongodb_connection_string"),
                GetParameter("mongodb_database"));

            Seed.Cleanup();

            //seed.Seed();
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
