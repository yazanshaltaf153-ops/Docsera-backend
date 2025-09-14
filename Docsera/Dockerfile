# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY Docsera/Docsera.csproj ./Docsera.csproj
RUN dotnet restore Docsera.csproj
COPY Docsera/. .
RUN dotnet publish Docsera.csproj -c Release -o /app

# run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENTRYPOINT ["dotnet", "Docsera.dll"]