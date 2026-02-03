# Formation-Ecommerce-11-2025.Test

Ce projet contient les **tests automatisés** (xUnit) de la solution `Formation-Ecommerce-11-2025`.

L’objectif pédagogique est de montrer :
- comment tester la **couche Application** (tests unitaires)
- comment tester l’application MVC “de bout en bout” **sans base SQL Server** (tests d’intégration)

## Structure du projet

- `Unit/`
  - Tests unitaires (services Application, validation DataAnnotations, mapping AutoMapper, etc.)
  - Ils ne démarrent pas le serveur web.

- `Integration/`
  - Tests d’intégration basés sur `WebApplicationFactory<Program>`.
  - Ils démarrent un serveur ASP.NET Core “in-memory” et font des appels HTTP via `HttpClient`.

- `Fakes/`
  - Faux repositories / faux services / faux helpers.
  - Objectif : faire des tests lisibles, sans framework de mock.

- `Common/`
  - Helpers réutilisables pour les tests (ex: validation DataAnnotations).

## Exécuter les tests

Depuis le dossier de la solution (`Formation-Ecommerce-11-2025`) :

```powershell
# 1) Restore

dotnet restore .\Formation-Ecommerce-11-2025.sln

# 2) Lancer tous les tests

dotnet test .\Formation-Ecommerce-11-2025.sln

# Plus rapide après le restore

dotnet test .\Formation-Ecommerce-11-2025.sln --no-restore
```

## Différence entre tests Unit et Integration

- **Unit** :
  - rapides
  - isolent une classe (ex: `ProductServices`) en remplaçant ses dépendances par des fakes
  - ne nécessitent pas de DB réelle

- **Integration** :
  - démarrent l’application ASP.NET Core
  - font de vrais appels HTTP sur des routes MVC
  - utilisent EF Core **InMemory** au lieu de SQL Server (voir `CustomWebApplicationFactory`)

## Pourquoi EF Core InMemory dans les tests d’intégration ?

En tests, on veut :
- éviter la dépendance à SQL Server
- pouvoir exécuter les tests sur n’importe quelle machine
- garder des tests rapides

Le factory configure une DB InMemory avec un **nom unique** pour éviter les interférences entre tests.

## Lecture des messages dans la console

Pendant les tests d’intégration tu peux voir :
- `Saved 0 entities to in-memory store.` (log EF Core)
- `Failed to determine the https port for redirect.` (warning HTTPS redirection)

Ces messages sont habituels en environnement de test et ne sont pas des erreurs si les tests passent.
