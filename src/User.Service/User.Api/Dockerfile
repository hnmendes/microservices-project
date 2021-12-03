#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/User.Service/User.Api/User.Api.csproj", "src/User.Service/User.Api/"]
COPY ["src/User.Service/User.Infra/User.Infrastructure.csproj", "src/User.Service/User.Infra/"]
COPY ["src/User.Service/User.Application/User.Application.csproj", "src/User.Service/User.Application/"]
COPY ["src/User.Service/User.Domain/User.Domain.csproj", "src/User.Service/User.Domain/"]
RUN dotnet restore "src/User.Service/User.Api/User.Api.csproj"
COPY . .
WORKDIR "/src/src/User.Service/User.Api"
RUN dotnet build "User.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "User.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "User.Api.dll"]