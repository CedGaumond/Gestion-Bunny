# Guide Complet d'Installation et Configuration - Gestion-Bunny 🚀

## Prérequis Système 🖥️

* Windows 10 ou plus récent
* Minimum 8 GB RAM
* 10 GB d'espace disque disponible
* Connexion Internet stable

## 1. Clonage du Projet 📂

```powershell
# Ouvrir PowerShell en mode administrateur
git clone https://github.com/CedGaumond/Gestion-Bunny.git
cd Gestion-Bunny/Source
```

## 2. Installation des Logiciels Requis 💻

### Visual Studio Code
1. **Téléchargement**:
   * Visiter [https://code.visualstudio.com/](https://code.visualstudio.com/)
   * Cliquer sur "Download for Windows"
   * Choisir la version 64-bit

2. **Installation**:
   * Exécuter le fichier `.exe`
   * Cocher toutes les options:
     * "Ajouter l'action 'Ouvrir avec Code' au menu contextuel"
     * "Ajouter au PATH"
     * "Enregistrer Code comme éditeur par défaut"

### Extensions Essentielles ⚙️

1. **Installation de .NET MAUI**:
   * Ouvrir VS Code
   * Appuyer sur `Ctrl + Shift + X`
   * Rechercher ".NET MAUI"
   * Installer ".NET MAUI Extension Pack"

2. **Installation de C# Dev Kit**:
   * Rechercher "C# Dev Kit"
   * Installer "C# Dev Kit"
   * Installer les dépendances suggérées

3. **Installation de PostgreSQL**:
   * Rechercher "PostgreSQL"
   * Installer "PostgreSQL" par Chris Kolkman

### PgAdmin
1. **Installation**:
   * Télécharger depuis [https://www.pgadmin.org/download/](https://www.pgadmin.org/download/)
   * Suivre l'assistant d'installation
   * Mot de passe maître: **UlGBqeXlkG**

## 3. Configuration de la Base de Données 🗄️

1. **Création de la Base**:
   * Ouvrir PgAdmin
   * Clic droit sur "Databases"
   * Create > Database
   * Nom: `bunny_db`

2. **Localisation des Scripts**:
   * Naviguer vers le dossier: `Gestion-Bunny/Source/Sql`
   * Vous trouverez deux fichiers:
     * `2025-02-10-0920.DatabaseInitialScript.sql`
     * `2025-02-12-1618.Addingredients.sql`

3. **Exécution des Scripts**:
   * Dans PgAdmin:
     * Clic droit sur `bunny_db`
     * Sélectionner "Query Tool"
     * Copier le contenu de `DatabaseInitialScript.sql`
     * Coller dans Query Tool
     * Exécuter avec le bouton ▶️ ou `F5`
     * Répéter pour `Addingredients.sql`
   * Vérifier les messages de succès

## 4. Installation du SDK .NET 🔧

1. **Téléchargement**:
   * Visiter [https://dotnet.microsoft.com/en-us/download/dotnet/9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
   * Télécharger le SDK
   * Installer avec les options par défaut

2. **Vérification**:
   ```powershell
   dotnet --version
   ```

## 5. Configuration du Projet 📝

1. **Restauration des Packages**:
   ```powershell
   dotnet workload restore
   ```

2. **Installation des Dépendances**:
   ```powershell
   dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
   dotnet add package QuestPDF
   ```

## 6. Compilation et Exécution ▶️

```powershell
dotnet build
dotnet run
```

## Dépannage Courant 🔍

* **Erreur PostgreSQL**: 
  * Vérifier le service PostgreSQL dans les services Windows
  * Vérifier le mot de passe

* **Erreur Compilation**: 
  ```powershell
  dotnet clean
  dotnet build
  ```

* **Fichiers SQL introuvables**:
  * Vérifier dans tous les sous-dossiers du projet
  * Utiliser la commande de recherche Windows

* **Erreur d'exécution des scripts SQL**:
  * Vérifier les droits d'accès à PostgreSQL
  * Vérifier la connexion à la base de données

## Support 📚

* Documentation .NET MAUI
* Guide PostgreSQL
* Manuel Gestion-Bunny

## Notes Importantes ⚠️

* Sauvegarder régulièrement la base de données
* Changer le mot de passe par défaut en production
* Mettre à jour régulièrement les composants
