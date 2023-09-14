#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OnlineBookStoreProject/WebAPI/WebAPI.csproj", "src/OnlineBookStoreProject/WebAPI/"]
COPY ["src/OnlineBookStoreProject/Persistence/Persistence.csproj", "src/OnlineBookStoreProject/Persistence/"]
COPY ["src/OnlineBookStoreProject/Application/Application.csproj", "src/OnlineBookStoreProject/Application/"]
COPY ["src/corePackages/Core.Application/Core.Application.csproj", "src/corePackages/Core.Application/"]
COPY ["src/corePackages/Core.CrossCuttingConcers/Core.CrossCuttingConcerns.csproj", "src/corePackages/Core.CrossCuttingConcers/"]
COPY ["src/corePackages/Core.Security/Core.Security.csproj", "src/corePackages/Core.Security/"]
COPY ["src/corePackages/Core.Persistence/Core.Persistence.csproj", "src/corePackages/Core.Persistence/"]
COPY ["src/OnlineBookStoreProject/Domain/Domain.csproj", "src/OnlineBookStoreProject/Domain/"]
RUN dotnet restore "src/OnlineBookStoreProject/WebAPI/WebAPI.csproj"
COPY . .
WORKDIR "/src/src/OnlineBookStoreProject/WebAPI"
RUN dotnet build "WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebAPI.dll"]