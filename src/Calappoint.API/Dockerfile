#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Calappoint.API/Calappoint.API.csproj", "src/Calappoint.API/"]
COPY ["src/Calappoint.Application/Calappoint.Application.csproj", "src/Calappoint.Application/"]
COPY ["src/Calappoint.Domain/Calappoint.Domain.csproj", "src/Calappoint.Domain/"]
COPY ["src/Calappoint.SharedKernel/Calappoint.SharedKernel.csproj", "src/Calappoint.SharedKernel/"]
COPY ["src/Calappoint.Infrastructure/Calappoint.Infrastructure.csproj", "src/Calappoint.Infrastructure/"]
RUN dotnet restore "./src/Calappoint.API/Calappoint.API.csproj"
COPY . .
WORKDIR "/src/src/Calappoint.API"
RUN dotnet build "./Calappoint.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Calappoint.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Calappoint.API.dll"]