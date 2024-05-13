
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ./Adapter.Azure.Blob/Adapter.Azure.Blob.csproj ./Adapter.Azure.Blob/Adapter.Azure.Blob.csproj
COPY ./Hexagonal.Repositories/Hexagonal.Repositories.csproj ./Hexagonal.Repositories/Hexagonal.Repositories.csproj
COPY ./Hexagonal.Domain/Hexagonal.Domain.csproj ./Hexagonal.Domain/Hexagonal.Domain.csproj
COPY ./RauchTech.Logging/RauchTech.Logging.csproj ./RauchTech.Logging/RauchTech.Logging.csproj
COPY ./RauchTech.Logging.Azure/RauchTech.Logging.Azure.csproj ./RauchTech.Logging.Azure/RauchTech.Logging.Azure.csproj
COPY ./Hexagonal.Services/Hexagonal.Services.csproj ./Hexagonal.Services/Hexagonal.Services.csproj
COPY ./Hexagonal.Api/Hexagonal.Api.csproj ./Hexagonal.Api/Hexagonal.Api.csproj
COPY ./Hexagonal.Session/Hexagonal.Session.csproj ./Hexagonal.Session/Hexagonal.Session.csproj
COPY ./Hexagonal.Common/Hexagonal.Common.csproj ./Hexagonal.Common/Hexagonal.Common.csproj
RUN dotnet restore ./Hexagonal.Api/Hexagonal.Api.csproj
COPY ./Adapter.Azure.Blob ./Adapter.Azure.Blob
COPY ./Hexagonal.Repositories ./Hexagonal.Repositories
COPY ./Hexagonal.Domain ./Hexagonal.Domain
COPY ./RauchTech.Logging ./RauchTech.Logging
COPY ./RauchTech.Logging.Azure ./RauchTech.Logging.Azure
COPY ./Hexagonal.Services ./Hexagonal.Services
COPY ./Hexagonal.Api ./Hexagonal.Api
COPY ./Hexagonal.Session ./Hexagonal.Session
COPY ./Hexagonal.Common ./Hexagonal.Common
RUN dotnet build ./Hexagonal.Api/Hexagonal.Api.csproj -c $BUILD_CONFIGURATION -o /app/build

FROM build as publish
RUN dotnet publish ./Hexagonal.Api/Hexagonal.Api.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hexagonal.Api.dll"]