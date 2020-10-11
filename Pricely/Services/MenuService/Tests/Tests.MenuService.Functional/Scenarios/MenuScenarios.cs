using MenuService.Persistence.DTOModels;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tests.MenuService.Functional.Clients;
using Xunit;

namespace Tests.MenuService.Functional.Scenarios
{
    public class MenuScenarios : MenuScenarioBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync(Get.GetAll);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IEnumerable<MenuDto>>(content);

            // two seeded menus..
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task Get_SuccessStatusCode_Gets()
        {
            var id = Guid.Parse("c8b66e56-7a21-4166-98ac-ecefc3040a7f");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync(Get.GetById(id));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<MenuDto>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var menu = new MenuDto()
            {
                Id = new Guid("d8b66e56-7a21-4166-98ac-ecefc3040a7f"),
                Name = "New menu",
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(menu),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync(Post.CreateMenu, requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guid>(content);

            Assert.NotEqual(Guid.Empty, result);
        }


        [Fact]
        public async Task Update_SuccessStatusCode_Updates()
        {
            var menu = new MenuDto()
            {
                Id = Guid.Parse("c8b66e56-7a21-4166-98ac-ecefc3040a7f"),
                Name = "Update",
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(menu),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync(Put.UpdateMenu, requestContent);

            response.EnsureSuccessStatusCode();

            // get the entity from DB again
            var updatedMenuResponse = await client.GetAsync(Get.GetById(menu.Id));
            var updatedMenuContent = await updatedMenuResponse.Content.ReadAsStringAsync();
            var updatedMenu = JsonConvert.DeserializeObject<MenuDto>(updatedMenuContent);

            // assert updated fields
            Assert.Equal("Update", updatedMenu.Name);
        }


    }
}
