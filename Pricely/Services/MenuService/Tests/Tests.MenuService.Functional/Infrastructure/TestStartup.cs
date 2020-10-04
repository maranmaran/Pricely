using MenuService.API;
using Microsoft.AspNetCore.Hosting;

namespace Tests.MenuService.Functional.Infrastructure
{
    public class TestStartup : Startup
    {
        public TestStartup(IWebHostEnvironment env) : base(env)
        {

        }
    }
}
