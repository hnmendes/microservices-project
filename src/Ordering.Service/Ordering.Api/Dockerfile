#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Ordering.Service/Ordering.Api/Ordering.Api.csproj", "src/Ordering.Service/Ordering.Api/"]
COPY ["src/Ordering.Service/Ordering.Application/Ordering.Application.csproj", "src/Ordering.Service/Ordering.Application/"]
COPY ["src/Ordering.Service/Ordering.Domain/Ordering.Domain.csproj", "src/Ordering.Service/Ordering.Domain/"]
COPY ["src/BuildingBlocks/EventBus.Messages/EventBus.Messages.csproj", "src/BuildingBlocks/EventBus.Messages/"]
COPY ["src/Ordering.Service/Ordering.Infrastructure/Ordering.Infrastructure.csproj", "src/Ordering.Service/Ordering.Infrastructure/"]
RUN dotnet restore "src/Ordering.Service/Ordering.Api/Ordering.Api.csproj"
COPY . .
WORKDIR "/src/src/Ordering.Service/Ordering.Api"
RUN dotnet build "Ordering.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.Api.dll"]