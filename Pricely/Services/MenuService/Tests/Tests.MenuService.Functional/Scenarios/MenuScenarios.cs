using MenuService.Persistence.DTOModels;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tests.MenuService.Functional.Clients;
using Xunit;

namespace Tests.MenuService.Functional.Scenarios
{
    public class MenuScenarios : MenuScenarioBase
    {
        [Fact]
        public async Task GetAllMenus_SuccessStatusCode_RetrievesTwoSeededMenus()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync(Get.GetAll);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MenuDto>>(content);

            Assert.Equal(2, result.Count());
        }



    }
}
