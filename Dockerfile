# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY *.sln ./
COPY indigoLibrary.API/*.csproj ./indigoLibrary.API/
COPY indigoLibrary.Application/*.csproj ./indigoLibrary.Application/
COPY indigoLibrary.Infrastructure/*.csproj ./indigoLibrary.Infrastructure/

COPY . .
RUN dotnet publish indigoLibrary.API/indigoLibrary.API.csproj -c Release -o /app/publish

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
CMD ["dotnet", "indigoLibrary.API.dll", "--urls", "http://0.0.0.0:8080"]
