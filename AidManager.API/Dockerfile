# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore "AidManager.API/AidManager.API.csproj"
RUN dotnet publish "AidManager.API/AidManager.API.csproj" -c Release -o /out --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /out ./

# Configurar la variable de entorno si usas puertos distintos
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "AidManager.API.dll"]
