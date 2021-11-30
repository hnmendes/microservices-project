#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Catalog.Service/Catalog.Api/Catalog.Api.csproj", "src/Catalog.Service/Catalog.Api/"]
COPY ["src/Catalog.Service/Catalog.Infra.Data/Catalog.Infra.Data.csproj", "src/Catalog.Service/Catalog.Infra.Data/"]
COPY ["src/Catalog.Service/Catalog.Domain/Catalog.Domain.csproj", "src/Catalog.Service/Catalog.Domain/"]
RUN dotnet restore "src/Catalog.Service/Catalog.Api/Catalog.Api.csproj"
COPY . .
WORKDIR "/src/src/Catalog.Service/Catalog.Api"
RUN dotnet build "Catalog.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Api.dll"]