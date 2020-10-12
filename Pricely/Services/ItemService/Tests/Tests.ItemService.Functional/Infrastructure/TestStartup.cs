using ItemService.API;
using Microsoft.AspNetCore.Hosting;

namespace Tests.ItemService.Functional.Infrastructure
{
    public class TestStartup : Startup
    {
        public TestStartup(IWebHostEnvironment env) : base(env)
        {

        }
    }
}
