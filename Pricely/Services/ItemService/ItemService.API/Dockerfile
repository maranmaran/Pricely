FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR ./

# Keep the project list and command dotnet restore identical in all Dockfiles to maximize image cache utilization
COPY Pricely.sln .
COPY docker-compose.dcproj .

COPY Shared/Common/ Shared/Common/

#COPY Libraries/Library.Communication/

COPY Libraries/Library.EventBus/EventBus.Infrastructure/ Libraries/Library.EventBus/EventBus.Infrastructure/
COPY Libraries/Library.EventBus/EventBus.RabbitMQ/ Libraries/Library.EventBus/EventBus.RabbitMQ/

COPY Services/ItemService/ItemService.API/ Services/ItemService/ItemService.API/
COPY Services/ItemService/ItemService.Business/ Services/ItemService/ItemService.Business/
COPY Services/ItemService/ItemService.Persistence/ Services/ItemService/ItemService.Persistence/
COPY Services/ItemService/ItemService.Domain/ Services/ItemService/ItemService.Domain/

RUN dotnet restore Services/ItemService/ItemService.API/ItemService.API.csproj 

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
