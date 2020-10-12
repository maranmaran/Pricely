

using Common.Models;
using ItemService.Persistence.DTOModels;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tests.ItemService.Functional.Clients;
using Xunit;

namespace Tests.ItemService.Functional.Scenarios
{
    public class ItemScenarios : ItemScenarioBase
    {
        [Fact]
        public async Task GetAll_SuccessStatusCode_GetsAll()
        {
            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync(Get.GetAll());

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PagedList<ItemDto>>(content);

            // two seeded Items..
            Assert.Equal(3, result.TotalItems);
        }

        [Fact]
        public async Task Get_SuccessStatusCode_Gets()
        {
            var id = Guid.Parse("d3b56d57-453c-4382-9e49-437022e47f2a");

            using var client = (await CreateHost()).GetTestClient();
            var response = await client.GetAsync(Get.GetById(id));

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ItemDto>(content);

            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Create_SuccessStatusCode_Creates()
        {
            var Item = new ItemDto()
            {
                Id = new Guid("d8b66e56-7a21-4166-98ac-ecefc3040a7f"),
                Name = "New Item",
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(Item),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PostAsync(Post.CreateItem, requestContent);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guid>(content);

            Assert.NotEqual(Guid.Empty, result);
        }

        [Fact]
        public async Task Update_SuccessStatusCode_Updates()
        {
            var id = Guid.Parse("d3b56d57-453c-4382-9e49-437022e47f2a");

            var Item = new ItemDto()
            {
                Id = id,
                Name = "Update",
                Category = new CategoryDto()
                {
                    Id = Guid.Parse("9a542d51-7aa0-488e-87f2-9aef980680cb"),
                }
            };

            using var client = (await CreateHost()).GetTestClient();

            var requestContent = new StringContent(
                JsonConvert.SerializeObject(Item),
                Encoding.UTF8,
                "application/json"
            );

            var response = await client.PutAsync(Put.UpdateItem, requestContent);

            response.EnsureSuccessStatusCode();

            // get the entity from DB again
            var updatedItemResponse = await client.GetAsync(Get.GetById(Item.Id));
            var updatedItemContent = await updatedItemResponse.Content.ReadAsStringAsync();
            var updatedItem = JsonConvert.DeserializeObject<ItemDto>(updatedItemContent);

            // assert updated fields
            Assert.Equal("Update", updatedItem.Name);
        }

        [Fact]
        public async Task Delete_SuccessStatusCode_Deletes()
        {
            var id = Guid.Parse("d3b56d57-453c-4382-9e49-437022e47f2a");

            using var client = (await CreateHost()).GetTestClient();

            var response = await client.DeleteAsync(Delete.DeleteItem(id));

            response.EnsureSuccessStatusCode();

            // get the entity from DB again
            var deleteItemResponse = await client.GetAsync(Get.GetById(id));

            Assert.Equal(HttpStatusCode.NotFound, deleteItemResponse.StatusCode);

        }

    }
}
