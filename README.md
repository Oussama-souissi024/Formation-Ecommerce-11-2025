# Projet de formation — E‑Commerce Full Stack (ASP.NET Core MVC) avec Clean Architecture

## Objectif
Ce projet est le livrable d’une formation .NET visant à construire une application **Full Stack** en **ASP.NET Core MVC** (UI + logique applicative + accès aux données), structurée selon les principes de la **Clean Architecture**.

L’objectif pédagogique est double :
- Comprendre comment construire une application web MVC complète (UI, authentification, CRUD, panier, commandes).
- Apprendre à **structurer** le code pour le rendre **maintenable**, **testable**, et **évolutif** (niveau “pro”).

## Valeur ajoutée pour un développeur junior
Ce projet est conçu pour accélérer la montée en compétence sur des sujets indispensables en entreprise.

### Compétences techniques développées
- Architecture en couches (Clean Architecture) et séparation des responsabilités
- ASP.NET Core MVC (Controllers, Views, Model Binding, Validation)
- Accès aux données avec Entity Framework Core (DbContext, migrations, LINQ)
- Authentification / Autorisation (ASP.NET Core Identity, rôles, [Authorize])
- Mapping DTO/ViewModel (ex: AutoMapper selon l’implémentation)
- Gestion d’erreurs et bonnes pratiques (statuts, validations, conventions)

### Compétences “software engineering”
- Lecture d’un projet structuré comme en entreprise
- SOLID / DIP (Dependency Inversion) appliqués concrètement
- Code plus modulaire : remplaçable, testable, plus facile à faire évoluer
- Meilleure maîtrise des dépendances et de la configuration

## Fonctionnalités (exemples)
Selon l’avancement de la formation, l’application couvre généralement :
- Catalogue produits (CRUD côté admin + affichage côté client)
- Catégories
- Coupons / promotions
- Panier (ajout/suppression/modification)
- Checkout / commandes (création, historique, détails)
- Authentification (inscription, connexion, confirmation email, reset password)
- Autorisation par rôles (ex: Admin / User)

## Architecture (Clean Architecture)
Le principe central : **les règles métier ne dépendent pas de l’UI ni de la base de données**.

### Découpage typique
- **Presentation (MVC)**
  - Controllers + Views
  - ViewModels / validation UI
  - Expérience utilisateur et navigation

- **Application**
  - Cas d’usage (services applicatifs)
  - DTOs, interfaces, logique de workflow
  - Orchestration (ex: créer commande, appliquer coupon…)

- **Core (Domain)**
  - Entités métier
  - Interfaces métier (si applicable)
  - Règles et invariants du domaine

- **Infrastructure**
  - EF Core (DbContext, configurations, migrations)
  - Services externes (email, stockage, etc.)
  - Implémentations concrètes des interfaces

### Pourquoi c’est important
- **Évolutivité** : faciliter l’ajout de nouvelles fonctionnalités sans casser l’existant.
- **Testabilité** : isoler la logique applicative et la tester plus facilement.
- **Maintenabilité** : chaque couche a une responsabilité claire.

## Stack technique
- .NET / ASP.NET Core MVC
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- Razor Views
- (Optionnel selon projet) AutoMapper, FluentValidation, etc.

## Lancer le projet en local
### Pré-requis
- .NET SDK installé
- SQL Server (LocalDB ou SQL Server)

### Configuration
- Vérifie les paramètres dans `appsettings.json` / `appsettings.Development.json`.
- Configure la chaîne de connexion SQL Server.
- Si l’envoi d’emails est activé : renseigne les paramètres SMTP (ou désactive en dev).

### Base de données
- Appliquer les migrations EF Core (si le projet en contient).

### Exécution
- Lancer le projet depuis Visual Studio / `dotnet run`.

## Périmètre pédagogique
Ce repository sert de base pour :
- Comprendre une vraie architecture “projet réel”
- Reproduire les patterns sur d’autres applications


## Licence / Usage
Projet destiné à un usage pédagogique.
