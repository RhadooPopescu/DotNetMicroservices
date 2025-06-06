#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Market/Market.API/Market.API.csproj", "Services/Market/Market.API/"]
RUN dotnet restore "Services/Market/Market.API/Market.API.csproj"
COPY . .
WORKDIR "/src/Services/Market/Market.API"
RUN dotnet build "Market.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Market.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Market.API.dll"]
