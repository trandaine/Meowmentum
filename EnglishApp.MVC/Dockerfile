# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EnglishApp.MVC/EnglishApp.MVC.csproj", "EnglishApp.MVC/"]
COPY ["EnglishApp.BusinessLogic/EnglishApp.BusinessLogic.csproj", "EnglishApp.BusinessLogic/"]
COPY ["EnglishApp.Infrastructure/EnglishApp.Infrastructure.csproj", "EnglishApp.Infrastructure/"]
COPY ["EnglishApp.ApplicationCore/EnglishApp.ApplicationCore.csproj", "EnglishApp.ApplicationCore/"]
COPY ["EnglishApp.Common/EnglishApp.Common.csproj", "EnglishApp.Common/"]
RUN dotnet restore "./EnglishApp.MVC/EnglishApp.MVC.csproj"
COPY . .
WORKDIR "/src/EnglishApp.MVC"
RUN dotnet build "./EnglishApp.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EnglishApp.MVC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EnglishApp.MVC.dll"]