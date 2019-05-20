using TogglerAdmin.Abstractions;

namespace TogglerAdmin.Api.Controllers
{
    public class AppOperationContext : IAppOperationContext
    {
        public AppOperationContext(string userName)
        {
            UserName = string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;
        }

        public string UserName { get; }
    }
}