# 🚀 Guide de Déploiement DevOps - Formation E-Commerce

## Objectif Pédagogique
Ce guide vous accompagne dans la mise en place d'un pipeline DevOps complet pour déployer l'application **Formation-Ecommerce** (ASP.NET Core MVC + Clean Architecture) avec **Docker** et **Azure**.

---

## 📋 Table des Matières

1. [Prérequis](#-prérequis)
2. [Phase 1 : Dockerisation](#-phase-1--dockerisation)
3. [Phase 2 : Azure Container Registry (ACR)](#-phase-2--azure-container-registry-acr)
4. [Phase 3 : Azure SQL Database](#-phase-3--azure-sql-database)
5. [Phase 4 : Azure App Service](#-phase-4--azure-app-service)
6. [Phase 5 : CI/CD avec GitHub Actions](#-phase-5--cicd-avec-github-actions)
7. [Phase 6 : Monitoring et Logs](#-phase-6--monitoring-et-logs)
8. [Annexes](#-annexes)

---

## ✅ Prérequis

### Outils à Installer

- [ ] **Docker Desktop** - [Télécharger](https://www.docker.com/products/docker-desktop/)
  - Vérifier l'installation : `docker --version`
  - Vérifier que Docker tourne : `docker run hello-world`

- [ ] **Azure CLI** - [Télécharger](https://docs.microsoft.com/fr-fr/cli/azure/install-azure-cli)
  - Vérifier l'installation : `az --version`
  - Se connecter : `az login`

- [ ] **.NET 8 SDK** - [Télécharger](https://dotnet.microsoft.com/download/dotnet/8.0)
  - Vérifier l'installation : `dotnet --version`

- [ ] **Visual Studio Code** ou **Visual Studio 2022** avec extension Docker

### Comptes Nécessaires

- [ ] Compte **Azure** avec abonnement actif (Azure for Students gratuit disponible)
- [ ] Compte **GitHub** avec le repository du projet
- [ ] Compte **Docker Hub** (optionnel, pour images publiques)

---

## 🐳 Phase 1 : Dockerisation

### Étape 1.1 : Créer le Dockerfile

- [ ] Créer un fichier `Dockerfile` à la racine du projet :

```dockerfile
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
```

### Étape 1.2 : Créer le fichier .dockerignore

- [ ] Créer un fichier `.dockerignore` à la racine :

```
**/.vs
**/.vscode
**/bin
**/obj
**/node_modules
**/.git
**/.gitignore
**/Dockerfile*
**/.dockerignore
**/docker-compose*
**/*.md
**/appsettings.Development.json
```

### Étape 1.3 : Créer docker-compose.yml (Développement Local)

- [ ] Créer le fichier `docker-compose.yml` :

```yaml
version: '3.8'

services:
  # Application ASP.NET Core MVC
  web:
    build:
      context: .
      dockerfile: Dockerfile
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=FormationEcommerce;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;
    depends_on:
      sqlserver:
        condition: service_healthy
    networks:
      - formation-network

  # SQL Server
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourStrong@Passw0rd
      - MSSQL_PID=Express
    ports:
      - "1433:1433"
    volumes:
      - sqlserver-data:/var/opt/mssql
    healthcheck:
      test: /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -C -Q "SELECT 1" || exit 1
      interval: 10s
      timeout: 5s
      retries: 5
    networks:
      - formation-network

networks:
  formation-network:
    driver: bridge

volumes:
  sqlserver-data:
```

### Étape 1.4 : Tester en Local

- [ ] Construire l'image Docker :
  ```powershell
  docker build -t formation-ecommerce:latest .
  ```

- [ ] Lancer avec docker-compose :
  ```powershell
  docker-compose up -d
  ```

- [ ] Vérifier les logs :
  ```powershell
  docker-compose logs -f web
  ```

- [ ] Tester l'application : Ouvrir http://localhost:8080

- [ ] Arrêter les conteneurs :
  ```powershell
  docker-compose down
  ```

---

## 📦 Phase 2 : Azure Container Registry (ACR)

### Étape 2.1 : Créer un Resource Group

- [ ] Créer le groupe de ressources :
  ```powershell
  az group create --name rg-formation-ecommerce --location francecentral
  ```

### Étape 2.2 : Créer l'Azure Container Registry

- [ ] Créer le registre (SKU Basic pour formation) :
  ```powershell
  az acr create --resource-group rg-formation-ecommerce --name acrformationecommerce --sku Basic
  ```
  > ⚠️ Le nom du registre doit être globalement unique (lettres et chiffres seulement)

### Étape 2.3 : Se Connecter à ACR

- [ ] Activer l'admin user :
  ```powershell
  az acr update -n acrformationecommerce --admin-enabled true
  ```

- [ ] Se connecter à ACR :
  ```powershell
  az acr login --name acrformationecommerce
  ```

### Étape 2.4 : Pousser l'Image vers ACR

- [ ] Tagger l'image :
  ```powershell
  docker tag formation-ecommerce:latest acrformationecommerce.azurecr.io/formation-ecommerce:v1.0
  ```

- [ ] Pousser l'image :
  ```powershell
  docker push acrformationecommerce.azurecr.io/formation-ecommerce:v1.0
  ```

- [ ] Vérifier l'image dans ACR :
  ```powershell
  az acr repository list --name acrformationecommerce --output table
  ```

---

## 🗄️ Phase 3 : Azure SQL Database

### Étape 3.1 : Créer le Serveur SQL Azure

- [ ] Créer le serveur SQL :
  ```powershell
  az sql server create `
    --name sql-formation-ecommerce `
    --resource-group rg-formation-ecommerce `
    --location francecentral `
    --admin-user sqladmin `
    --admin-password "VotreMotDePasse@123"
  ```
  > ⚠️ Notez le mot de passe admin !

### Étape 3.2 : Configurer le Pare-feu

- [ ] Autoriser les services Azure :
  ```powershell
  az sql server firewall-rule create `
    --resource-group rg-formation-ecommerce `
    --server sql-formation-ecommerce `
    --name AllowAzureServices `
    --start-ip-address 0.0.0.0 `
    --end-ip-address 0.0.0.0
  ```

- [ ] Autoriser votre IP (pour le développement) :
  ```powershell
  # Obtenir votre IP publique
  $myIp = (Invoke-RestMethod http://ipinfo.io/json).ip
  
  az sql server firewall-rule create `
    --resource-group rg-formation-ecommerce `
    --server sql-formation-ecommerce `
    --name AllowMyIP `
    --start-ip-address $myIp `
    --end-ip-address $myIp
  ```

### Étape 3.3 : Créer la Base de Données

- [ ] Créer la base (SKU Basic pour formation) :
  ```powershell
  az sql db create `
    --resource-group rg-formation-ecommerce `
    --server sql-formation-ecommerce `
    --name FormationEcommerceDb `
    --edition Basic `
    --capacity 5
  ```

### Étape 3.4 : Récupérer la Connection String

- [ ] Obtenir la connection string :
  ```powershell
  az sql db show-connection-string `
    --server sql-formation-ecommerce `
    --name FormationEcommerceDb `
    --client ado.net
  ```

- [ ] **Connection String finale** (remplacer `<username>` et `<password>`) :
  ```
  Server=tcp:sql-formation-ecommerce.database.windows.net,1433;Initial Catalog=FormationEcommerceDb;Persist Security Info=False;User ID=sqladmin;Password=VotreMotDePasse@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
  ```

### Étape 3.5 : Appliquer les Migrations EF Core

- [ ] Appliquer les migrations depuis votre machine locale :
  ```powershell
  cd Formation-Ecommerce
  
  # Définir temporairement la connection string Azure
  $env:ConnectionStrings__DefaultConnection="Server=tcp:sql-formation-ecommerce.database.windows.net,1433;Initial Catalog=FormationEcommerceDb;User ID=sqladmin;Password=VotreMotDePasse@123;Encrypt=True;TrustServerCertificate=False;"
  
  dotnet ef database update --project ../Formation-Ecommerce.Infrastructure
  ```

---

## ☁️ Phase 4 : Azure App Service

### Étape 4.1 : Créer l'App Service Plan

- [ ] Créer le plan (Linux, SKU B1 pour formation) :
  ```powershell
  az appservice plan create `
    --name asp-formation-ecommerce `
    --resource-group rg-formation-ecommerce `
    --sku B1 `
    --is-linux
  ```

### Étape 4.2 : Créer l'App Service (Web App for Containers)

- [ ] Créer la Web App :
  ```powershell
  az webapp create `
    --resource-group rg-formation-ecommerce `
    --plan asp-formation-ecommerce `
    --name formation-ecommerce-app `
    --deployment-container-image-name acrformationecommerce.azurecr.io/formation-ecommerce:v1.0
  ```
  > ⚠️ Le nom doit être globalement unique (sera l'URL : formation-ecommerce-app.azurewebsites.net)

### Étape 4.3 : Configurer l'Accès ACR

- [ ] Récupérer les credentials ACR :
  ```powershell
  $acrPassword = az acr credential show --name acrformationecommerce --query "passwords[0].value" -o tsv
  ```

- [ ] Configurer l'accès à ACR :
  ```powershell
  az webapp config container set `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --docker-custom-image-name acrformationecommerce.azurecr.io/formation-ecommerce:v1.0 `
    --docker-registry-server-url https://acrformationecommerce.azurecr.io `
    --docker-registry-server-user acrformationecommerce `
    --docker-registry-server-password $acrPassword
  ```

### Étape 4.4 : Configurer les Variables d'Environnement

- [ ] Configurer la connection string :
  ```powershell
  az webapp config connection-string set `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --connection-string-type SQLAzure `
    --settings DefaultConnection="Server=tcp:sql-formation-ecommerce.database.windows.net,1433;Initial Catalog=FormationEcommerceDb;User ID=sqladmin;Password=VotreMotDePasse@123;Encrypt=True;"
  ```

- [ ] Configurer l'environnement et les settings SMTP :
  ```powershell
  az webapp config appsettings set `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --settings `
      ASPNETCORE_ENVIRONMENT=Production `
      EmailSettings__SmtpServer="votre-smtp.example.com" `
      EmailSettings__SmtpPort="587" `
      EmailSettings__SmtpUsername="votre-email@example.com" `
      EmailSettings__SmtpPassword="votre-mot-de-passe" `
      EmailSettings__SenderEmail="noreply@formation-ecommerce.com" `
      EmailSettings__SenderName="Formation E-Commerce"
  ```

### Étape 4.5 : Configurer le Port

- [ ] Spécifier le port du conteneur :
  ```powershell
  az webapp config appsettings set `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --settings WEBSITES_PORT=8080
  ```

### Étape 4.6 : Activer les Logs

- [ ] Activer les logs de conteneur :
  ```powershell
  az webapp log config `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --docker-container-logging filesystem
  ```

### Étape 4.7 : Tester le Déploiement

- [ ] Redémarrer l'application :
  ```powershell
  az webapp restart --name formation-ecommerce-app --resource-group rg-formation-ecommerce
  ```

- [ ] Vérifier les logs :
  ```powershell
  az webapp log tail --name formation-ecommerce-app --resource-group rg-formation-ecommerce
  ```

- [ ] Ouvrir l'application : https://formation-ecommerce-app.azurewebsites.net

---

## 🔄 Phase 5 : CI/CD avec GitHub Actions

### Étape 5.1 : Créer le Dossier Workflows

- [ ] Créer le dossier `.github/workflows` dans votre projet

### Étape 5.2 : Créer le Workflow de CI/CD

- [ ] Créer le fichier `.github/workflows/azure-deploy.yml` :

```yaml
name: Build and Deploy to Azure

on:
  push:
    branches: [ main ]
  pull_request:
    branches: [ main ]
  workflow_dispatch:

env:
  AZURE_WEBAPP_NAME: formation-ecommerce-app
  ACR_NAME: acrformationecommerce
  IMAGE_NAME: formation-ecommerce

jobs:
  # Job 1: Build et Tests
  build-and-test:
    runs-on: ubuntu-latest
    
    steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Setup .NET 8
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: '8.0.x'

    - name: Restore dependencies
      run: dotnet restore Formation-Ecommerce.sln

    - name: Build
      run: dotnet build Formation-Ecommerce.sln --configuration Release --no-restore

    - name: Run Tests
      run: dotnet test Formation-Ecommerce.Test/Formation-Ecommerce.Test.csproj --configuration Release --no-build --verbosity normal

  # Job 2: Build Docker et Push vers ACR
  docker-build-push:
    runs-on: ubuntu-latest
    needs: build-and-test
    if: github.ref == 'refs/heads/main'
    
    steps:
    - name: Checkout code
      uses: actions/checkout@v4

    - name: Login to Azure
      uses: azure/login@v1
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}

    - name: Login to ACR
      run: az acr login --name ${{ env.ACR_NAME }}

    - name: Build and Push Docker image
      run: |
        docker build -t ${{ env.ACR_NAME }}.azurecr.io/${{ env.IMAGE_NAME }}:${{ github.sha }} .
        docker build -t ${{ env.ACR_NAME }}.azurecr.io/${{ env.IMAGE_NAME }}:latest .
        docker push ${{ env.ACR_NAME }}.azurecr.io/${{ env.IMAGE_NAME }}:${{ github.sha }}
        docker push ${{ env.ACR_NAME }}.azurecr.io/${{ env.IMAGE_NAME }}:latest

  # Job 3: Deploy vers Azure App Service
  deploy:
    runs-on: ubuntu-latest
    needs: docker-build-push
    if: github.ref == 'refs/heads/main'
    
    steps:
    - name: Login to Azure
      uses: azure/login@v1
      with:
        creds: ${{ secrets.AZURE_CREDENTIALS }}

    - name: Deploy to Azure Web App
      uses: azure/webapps-deploy@v2
      with:
        app-name: ${{ env.AZURE_WEBAPP_NAME }}
        images: ${{ env.ACR_NAME }}.azurecr.io/${{ env.IMAGE_NAME }}:${{ github.sha }}
```

### Étape 5.3 : Configurer les Secrets GitHub

- [ ] Créer un Service Principal Azure :
  ```powershell
  az ad sp create-for-rbac `
    --name "sp-formation-ecommerce-github" `
    --role contributor `
    --scopes /subscriptions/<VOTRE_SUBSCRIPTION_ID>/resourceGroups/rg-formation-ecommerce `
    --json-auth
  ```
  > ⚠️ Copiez la sortie JSON complète !

- [ ] Dans GitHub, aller dans : **Settings** → **Secrets and variables** → **Actions**

- [ ] Créer le secret `AZURE_CREDENTIALS` avec le JSON copié

### Étape 5.4 : Activer le Continuous Deployment depuis ACR

- [ ] Activer le webhook ACR :
  ```powershell
  az webapp deployment container config `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --enable-cd true
  ```

- [ ] Récupérer l'URL du webhook :
  ```powershell
  az webapp deployment container show-cd-url `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce
  ```

---

## 📊 Phase 6 : Monitoring et Logs

### Étape 6.1 : Application Insights

- [ ] Créer Application Insights :
  ```powershell
  az monitor app-insights component create `
    --app ai-formation-ecommerce `
    --location francecentral `
    --resource-group rg-formation-ecommerce `
    --kind web
  ```

- [ ] Récupérer la clé d'instrumentation :
  ```powershell
  az monitor app-insights component show `
    --app ai-formation-ecommerce `
    --resource-group rg-formation-ecommerce `
    --query instrumentationKey -o tsv
  ```

- [ ] Configurer la clé dans l'App Service :
  ```powershell
  az webapp config appsettings set `
    --name formation-ecommerce-app `
    --resource-group rg-formation-ecommerce `
    --settings APPINSIGHTS_INSTRUMENTATIONKEY="<VOTRE_CLE>"
  ```

### Étape 6.2 : Ajouter Application Insights au Projet

- [ ] Installer le package NuGet (dans `Formation-Ecommerce.csproj`) :
  ```xml
  <PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.22.0" />
  ```

- [ ] Configurer dans `Program.cs` :
  ```csharp
  // Ajouter après builder.Services.AddControllersWithViews();
  builder.Services.AddApplicationInsightsTelemetry();
  ```

### Étape 6.3 : Configurer les Alertes

- [ ] Créer une alerte sur les erreurs 5xx :
  ```powershell
  az monitor metrics alert create `
    --name "alert-5xx-errors" `
    --resource-group rg-formation-ecommerce `
    --scopes "/subscriptions/<SUB_ID>/resourceGroups/rg-formation-ecommerce/providers/Microsoft.Web/sites/formation-ecommerce-app" `
    --condition "count Http5xx > 10" `
    --window-size 5m `
    --evaluation-frequency 1m
  ```

---

## 📚 Annexes

### A. Structure Finale du Projet

```
Formation-Ecommerce/
├── .github/
│   └── workflows/
│       └── azure-deploy.yml
├── Formation-Ecommerce/              # Projet MVC (UI)
├── Formation-Ecommerce.Application/  # Couche Application
├── Formation-Ecommerce.Core/         # Couche Domain
├── Formation-Ecommerce.Infrastructure/ # Couche Infrastructure
├── Formation-Ecommerce.Test/         # Tests unitaires
├── Dockerfile
├── .dockerignore
├── docker-compose.yml
├── Formation-Ecommerce.sln
└── README.md
```

### B. Commandes Azure Utiles

| Action | Commande |
|--------|----------|
| Voir les logs en temps réel | `az webapp log tail --name formation-ecommerce-app --resource-group rg-formation-ecommerce` |
| Redémarrer l'app | `az webapp restart --name formation-ecommerce-app --resource-group rg-formation-ecommerce` |
| Voir l'état de l'app | `az webapp show --name formation-ecommerce-app --resource-group rg-formation-ecommerce --query state` |
| Lister les images ACR | `az acr repository list --name acrformationecommerce --output table` |
| Supprimer le resource group | `az group delete --name rg-formation-ecommerce --yes` |

### C. Troubleshooting

| Problème | Solution |
|----------|----------|
| Container ne démarre pas | Vérifier les logs : `az webapp log tail` |
| Erreur de connexion SQL | Vérifier les règles de pare-feu Azure SQL |
| Image non trouvée | Vérifier les credentials ACR dans App Service |
| Application lente | Vérifier le SKU de l'App Service Plan |
| Migrations échouent | Vérifier la connection string et les permissions |

### D. Estimation des Coûts Azure (Formation)

| Service | SKU | Coût Estimé/Mois |
|---------|-----|------------------|
| App Service | B1 | ~13€ |
| Azure SQL | Basic (5 DTU) | ~5€ |
| Container Registry | Basic | ~5€ |
| Application Insights | Gratuit (5GB) | 0€ |
| **Total** | | **~23€/mois** |

> 💡 **Astuce** : Utilisez Azure for Students pour 100€ de crédits gratuits !

---

## ✅ Checklist Finale

- [ ] Dockerfile créé et testé localement
- [ ] docker-compose.yml fonctionnel
- [ ] Azure Resource Group créé
- [ ] Azure Container Registry configuré
- [ ] Azure SQL Database créée et migrations appliquées
- [ ] Azure App Service déployé
- [ ] Variables d'environnement configurées
- [ ] CI/CD GitHub Actions opérationnel
- [ ] Application Insights configuré
- [ ] Tests de l'application en production effectués

---

**Dernière mise à jour** : Février 2026  
**Version** : 1.0
