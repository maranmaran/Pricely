FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# Keep the project list and command dotnet restore identical in all Dockfiles to maximize image cache utilization
COPY Pricely.sln .
COPY docker-compose.dcproj .
COPY Shared/Common/
COPY Libraries/Library.Communication/
COPY Libraries/EventBus/EventBus.Infrastructure/
COPY Libraries/EventBus/EventBus.RabbitMQ/
COPY Services/ItemService/ItemService.API/
COPY Services/ItemService/ItemService.Business/
COPY Services/ItemService/ItemService.Persistence/
COPY Services/ItemService/ItemService.Domain/

RUN dotnet restore Pricely.sln

COPY . .
WORKDIR /Services/ItemService/ItemService.API
RUN dotnet publish --no-restore -c Release -o /app

#FROM build as unittest
#WORKDIR /Services/ItemService/ItemService.UnitTests

#FROM build as functionaltest
#WORKDIR /Services/ItemService/ItemService.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ItemService.API.dll"]