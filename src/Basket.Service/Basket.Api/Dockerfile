#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Basket.Service/Basket.Api/Basket.Api.csproj", "src/Basket.Service/Basket.Api/"]
COPY ["src/Basket.Service/Basket.Infra.Data/Basket.Infra.Data.csproj", "src/Basket.Service/Basket.Infra.Data/"]
COPY ["src/Basket.Service/Basket.Domain/Basket.Domain.csproj", "src/Basket.Service/Basket.Domain/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
RUN dotnet restore "src/Basket.Service/Basket.Api/Basket.Api.csproj"
COPY . .
WORKDIR "/src/src/Basket.Service/Basket.Api"
RUN dotnet build "Basket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Basket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Basket.Api.dll"]