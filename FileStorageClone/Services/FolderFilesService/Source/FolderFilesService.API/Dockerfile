FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR ./

COPY Shared/Common/ Shared/Common/

COPY Services/FolderFilesService/Source/FolderFilesService.API/ Services/FolderFilesService/Source/FolderFilesService.API/
COPY Services/FolderFilesService/Source/FolderFilesService.Business/ Services/FolderFilesService/Source/FolderFilesService.Business/
COPY Services/FolderFilesService/Source/FolderFilesService.Persistence/ Services/FolderFilesService/Source/FolderFilesService.Persistence/
COPY Services/FolderFilesService/Source/FolderFilesService.Domain/ Services/FolderFilesService/Source/FolderFilesService.Domain/

RUN dotnet restore Services/FolderFilesService/Source/FolderFilesService.API/FolderFilesService.API.csproj 

COPY . .
WORKDIR /Services/FolderFilesService/Source/FolderFilesService.API
RUN dotnet publish --no-restore -c Release -o /app

FROM build as tests_business
WORKDIR /Services/FolderFilesService/Tests/Tests.Business

FROM build as tests_persistence
WORKDIR /Services/FolderFilesService/Tests/Tests.Persistence

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FolderFilesService.API.dll"]
