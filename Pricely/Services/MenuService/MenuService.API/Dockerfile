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

COPY Services/MenuService/MenuService.API/ Services/MenuService/MenuService.API/
COPY Services/MenuService/MenuService.Business/ Services/MenuService/MenuService.Business/
COPY Services/MenuService/MenuService.Persistence/ Services/MenuService/MenuService.Persistence/
COPY Services/MenuService/MenuService.Domain/ Services/MenuService/MenuService.Domain/

RUN dotnet restore Services/MenuService/MenuService.API/MenuService.API.csproj 

COPY . .
WORKDIR /Services/MenuService/MenuService.API
RUN dotnet publish --no-restore -c Release -o /app

#FROM build as unittest
#WORKDIR /Services/MenuService/MenuService.UnitTests

#FROM build as functionaltest
#WORKDIR /Services/MenuService/MenuService.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MenuService.API.dll"]
