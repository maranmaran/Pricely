using System.Net.Http;
using FolderFilesService.API;

namespace Tests.FunctionalTests
{
    public abstract class ClientBase
    {
        public readonly HttpClient _client;

        protected ClientBase(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }
    }
}