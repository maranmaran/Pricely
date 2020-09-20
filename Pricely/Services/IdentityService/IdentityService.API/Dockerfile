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
COPY Libraries/Library.EventBus/EventBus.Azure/ Libraries/Library.EventBus/EventBus.Azure/

COPY Services/IdentityService/IdentityService.API/ Services/IdentityService/IdentityService.API/
COPY Services/IdentityService/IdentityService.Business/ Services/IdentityService/IdentityService.Business/
COPY Services/IdentityService/IdentityService.Persistence/ Services/IdentityService/IdentityService.Persistence/
COPY Services/IdentityService/IdentityService.Domain/ Services/IdentityService/IdentityService.Domain/

RUN dotnet restore Services/IdentityService/IdentityService.API/IdentityService.API.csproj 

COPY . .
WORKDIR /Services/IdentityService/IdentityService.API
RUN dotnet publish --no-restore -c Release -o /app

#FROM build as unittest
#WORKDIR /Services/IdentityService/IdentityService.UnitTests

#FROM build as functionaltest
#WORKDIR /Services/IdentityService/IdentityService.FunctionalTests

FROM build AS publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IdentityService.API.dll"]
