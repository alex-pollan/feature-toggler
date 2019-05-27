using System;
using System.Net;
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

        protected void AssertResponse(HttpResponseMessage response, HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.BadRequest:
                    AssertBadRequestResponse(response);
                    break;
                case HttpStatusCode.Conflict:
                    AssertConflictResponse(response);
                    break;
                case HttpStatusCode.NoContent:
                    AssertNoContentResponse(response);
                    break;
                case HttpStatusCode.OK:
                    AssertOkResponse(response);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected void AssertOkResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            Assert.AreEqual("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        protected void AssertNoContentResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        
        protected void AssertBadRequestResponse(HttpResponseMessage response)
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("application/problem+json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        protected void AssertConflictResponse(HttpResponseMessage response)
        {
            Assert.AreEqual(HttpStatusCode.Conflict, response.StatusCode);
            Assert.AreEqual("text/plain; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }
    }
}
