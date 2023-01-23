#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/WebApi/ProductApp.WebApi/ProductApp.WebApi.csproj", "src/WebApi/ProductApp.WebApi/"]
COPY ["src/Core/ProductApp.Application/ProductApp.Application.csproj", "src/Core/ProductApp.Application/"]
COPY ["src/Core/ProductApp.Domain/ProductApp.Domain.csproj", "src/Core/ProductApp.Domain/"]
COPY ["src/Insfrastructure/ProductApp.Persistence/ProductApp.Persistence.csproj", "src/Insfrastructure/ProductApp.Persistence/"]
RUN dotnet restore "src/WebApi/ProductApp.WebApi/ProductApp.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi/ProductApp.WebApi"
RUN dotnet build "ProductApp.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProductApp.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductApp.WebApi.dll"]