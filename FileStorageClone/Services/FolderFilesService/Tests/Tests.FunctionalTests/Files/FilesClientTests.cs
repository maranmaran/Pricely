using FolderFilesService.API;
using FolderFilesService.Business.Commands.File.Create;
using FolderFilesService.Persistence.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.FunctionalTests.Files
{
    public class FilesClientTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly FilesClient _client;

        public FilesClientTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = new FilesClient(factory);
        }

        [Fact]
        public async Task Get_FromRootAnyName_GetsAllFiles_StatusOk()
        {
            var response = await _client.Search(null, null);

            response.EnsureSuccessStatusCode();

            var data = await Utilities.GetResponseContent<IEnumerable<FileDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(10, data.Count());
        }

        [Fact]
        public async Task Get_FromRootAnyName_SearchFiles_StatusOk_RetrievesOnlySomeFiles()
        {
            var response = await _client.Search("File_Subfolder_", null);

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<IEnumerable<FileDto>>(response);

            Assert.NotEmpty(data);
            Assert.Equal(3, data.Count());
        }

        [Fact]
        public async Task Get_FromRootAnyName_SearchFilesInFolder_StatusOk_RetrievesOnlySomeFiles()
        {
            var response = await _client.Search(null, new Guid("0344FC6C-81D7-4808-91B4-66DD7F8FEF26"));

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<IEnumerable<FileDto>>(response);

            Assert.NotEmpty(data);
            Assert.Single(data);
        }

        [Fact]
        public async Task Create_AddToRoot_StatusOk()
        {
            var response = await _client.Create(new CreateFileCommand() { Name = "test", ParentFolderId = null });

            response.EnsureSuccessStatusCode();
            var data = await Utilities.GetResponseContent<Guid>(response);

            Assert.NotEqual(Guid.Empty, data);;
        }

        [Fact]
        public async Task Delete_Valid_StatusOk()
        {
            var response = await _client.Delete(new Guid("4AAEECA1-A5BD-409A-8722-0B904E2307A4"));

            response.EnsureSuccessStatusCode();
        }
    }
}
