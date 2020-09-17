FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /src

COPY scraperel.api/scraperel.api.csproj scraperel.api/
COPY scraperel.model/scraperel.model.csproj scraperel.model/
COPY scraperel.scraper/scraperel.scraper.csproj scraperel.scraper/

COPY . .

WORKDIR /src/scraperel.api
RUN dotnet build scraperel.api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish scraperel.api.csproj -c Release --no-restore -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS http://*:8080
ENV ASPNETCORE_ENVIRONMENT Docker
EXPOSE 8080

ENTRYPOINT dotnet scraperel.api.dll