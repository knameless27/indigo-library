# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.sln ./
COPY indigoLibrary.API/*.csproj ./indigoLibrary.API/
COPY indigoLibrary.Application/*.csproj ./indigoLibrary.Application/
COPY indigoLibrary.Infrastructure/*.csproj ./indigoLibrary.Infrastructure/
COPY indigoLibrary.Domain/*.csproj ./indigoLibrary.Domain/
COPY indigoLibrary.Tests/*.csproj ./indigoLibrary.Tests/
# Restaurar solo proyecto API
RUN dotnet restore indigoLibrary.API/indigoLibrary.API.csproj

# Copiar todo el código
COPY . .

# Publicar en Release para runtime más ligero
RUN dotnet publish indigoLibrary.API/indigoLibrary.API.csproj -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# Copiar solo los archivos publicados
COPY --from=build /app/publish .

EXPOSE 8080

# Ejecutar la app
CMD ["dotnet", "indigoLibrary.API.dll", "--urls", "http://0.0.0.0:8080"]

