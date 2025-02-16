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

#### .NET MAUI Extension
1. **Installation**:
   * Ouvrir VS Code
   * Appuyer sur `Ctrl + Shift + X`
   * Rechercher ".NET MAUI"
   * Installer ".NET MAUI Extension Pack"

#### C# Dev Kit
1. **Installation**:
   * Rechercher "C# Dev Kit"
   * Installer "C# Dev Kit"
   * Installer les dépendances suggérées

#### PostgreSQL Extension
1. **Installation**:
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

2. **Exécution des Scripts**:
   * Ouvrir Query Tool
   * Exécuter dans l'ordre:
     ```sql
     2025-02-10-0920.DatabaseInitialScript.sql
     2025-02-12-1618.Addingredients.sql
     ```

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

* **Erreur PostgreSQL**: Vérifier service Windows
* **Erreur Compilation**: 
  ```powershell
  dotnet clean
  dotnet build
  ```
* **Problèmes Extensions**: Réinstaller extensions

## Support 📚

* Documentation .NET MAUI
* Guide PostgreSQL
* Manuel Gestion-Bunny

## Notes Importantes ⚠️

* Sauvegarder régulièrement
* Ne pas utiliser en production
* Mettre à jour régulièrement
