FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR ./

# Keep the project list and command dotnet restore identical in all Dockfiles to maximize image cache utilization
COPY Shared/Common/ Shared/Common/

#COPY Libraries/Library.Communication/

COPY Libraries/Library.EventBus/EventBus.Infrastructure/ Libraries/Library.EventBus/EventBus.Infrastructure/
COPY Libraries/Library.EventBus/EventBus.RabbitMQ/ Libraries/Library.EventBus/EventBus.RabbitMQ/

COPY Services/FolderFilesService/FolderFilesService.API/ Services/FolderFilesService/FolderFilesService.API/
COPY Services/FolderFilesService/FolderFilesService.Business/ Services/FolderFilesService/FolderFilesService.Business/
COPY Services/FolderFilesService/FolderFilesService.Persistence/ Services/FolderFilesService/FolderFilesService.Persistence/
COPY Services/FolderFilesService/FolderFilesService.Domain/ Services/FolderFilesService/FolderFilesService.Domain/

RUN dotnet restore Services/FolderFilesService/FolderFilesService.API/FolderFilesService.API.csproj 

COPY . .
WORKDIR /Services/FolderFilesService/FolderFilesService.API
RUN dotnet publish --no-restore -c Release -o /app

#FROM build as unittest
#WORKDIR /Services/FolderFilesService/FolderFilesService.UnitTests

#FROM build as functionaltest
#WORKDIR /Services/FolderFilesService/FolderFilesService.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "FolderFilesService.API.dll"]
