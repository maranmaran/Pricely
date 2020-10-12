FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR ./

COPY Shared/Common/ Shared/Common/
COPY Libraries/Library.EventBus/EventBus.Infrastructure/ Libraries/Library.EventBus/EventBus.Infrastructure/
COPY Libraries/Library.EventBus/EventBus.RabbitMQ/ Libraries/Library.EventBus/EventBus.RabbitMQ/
COPY Libraries/Library.EventBus/EventBus.Azure/ Libraries/Library.EventBus/EventBus.Azure/
COPY Libraries/Library.DataAccess/DataAccess.NoSql/ Libraries/Library.DataAccess/DataAccess.NoSql/

COPY Services/MenuService/MenuService.API/ Services/MenuService/MenuService.API/
COPY Services/MenuService/MenuService.Business/ Services/MenuService/MenuService.Business/
COPY Services/MenuService/MenuService.Persistence/ Services/MenuService/MenuService.Persistence/
COPY Services/MenuService/MenuService.Domain/ Services/MenuService/MenuService.Domain/

RUN dotnet restore Services/MenuService/MenuService.API/MenuService.API.csproj 
COPY . .
WORKDIR /Services/MenuService/MenuService.API

RUN dotnet build --no-restore -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MenuService.API.dll"]
