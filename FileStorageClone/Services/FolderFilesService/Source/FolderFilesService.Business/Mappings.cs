using AutoMapper;
using FolderFilesService.Business.Commands.File.Create;
using FolderFilesService.Business.Commands.Folder.Create;
using FolderFilesService.Domain.Entities;
using FolderFilesService.Persistence.DTOModels;

namespace FolderFilesService.Business
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<CreateFileCommand, File>().ReverseMap();
            CreateMap<File, FileDto>().ReverseMap();
            
            CreateMap<CreateFolderCommand, Folder>().ReverseMap();
            CreateMap<Folder, FolderDto>().ReverseMap();
        }
    }
}
