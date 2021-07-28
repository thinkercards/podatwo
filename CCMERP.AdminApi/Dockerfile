#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CCMERP.AdminApi/CCMERP.AdminApi.csproj", "CCMERP.AdminApi/"]
RUN dotnet restore "CCMERP.AdminApi/CCMERP.AdminApi.csproj"
COPY . .
WORKDIR "/src/CCMERP.AdminApi"
RUN dotnet build "CCMERP.AdminApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CCMERP.AdminApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CCMERP.AdminApi.dll"]