# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copier les fichiers csproj et restaurer les dépendances
COPY ["Formation-Ecommerce/Formation-Ecommerce.csproj", "Formation-Ecommerce/"]
COPY ["Formation-Ecommerce.Application/Formation-Ecommerce.Application.csproj", "Formation-Ecommerce.Application/"]
COPY ["Formation-Ecommerce.Core/Formation-Ecommerce.Core.csproj", "Formation-Ecommerce.Core/"]
COPY ["Formation-Ecommerce.Infrastructure/Formation-Ecommerce.Infrastructure.csproj", "Formation-Ecommerce.Infrastructure/"]

RUN dotnet restore "Formation-Ecommerce/Formation-Ecommerce.csproj"

# Copier tout le code source
COPY . .

# Build du projet
WORKDIR "/src/Formation-Ecommerce"
RUN dotnet build "Formation-Ecommerce.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "Formation-Ecommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Créer un utilisateur non-root pour la sécurité
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .

# Port exposé
EXPOSE 8080

# Variables d'environnement par défaut
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Formation-Ecommerce.dll"]
