# Comment Configurer et Exécuter le Projet 🚀 (Windows)

## Première Étape : Cloner le Projet 📂

### 1. Cloner le Dépôt Git

```powershell
# Ouvrez l'Invite de Commandes (CMD)
# ⚠️ IMPORTANT : Gardez le CMD ouvert, il sera nécessaire pour les étapes suivantes!
# Choisissez un dossier où vous voulez cloner le projet

# Clonez le dépôt
git clone https://github.com/CedGaumond/Gestion-Bunny.git

# Naviguez vers le dossier du projet
cd Gestion-Bunny
cd BunnyCO/BunnyCO
```
## Installation des Logiciels Requis 💻

### 2. Visual Studio Code

Téléchargez Visual Studio Code depuis https://code.visualstudio.com/
Suivez l'assistant d'installation
Lancez VS Code après l'installation

### 3. Extension .NET MAUI ⚙️

Lancez Visual Studio Code
Appuyez sur Ctrl + Shift + X pour ouvrir les Extensions
Recherchez ".NET MAUI"
Cliquez sur "Installer"

### 4. Installation du SDK .NET 🔧

Visitez https://dotnet.microsoft.com/en-us/download/dotnet/9.0
Téléchargez le SDK
Exécutez l'installateur

## Configuration du Projet 📝
### 5. Restauration des Dépendances
```powershell
# Dans le même CMD que précédemment
# Assurez-vous d'être dans le dossier BunnyCO
dotnet workload restore
```

### 6. Ajout des packets NuGet
```powershell
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package QuestPDF
```
## Configuration de la Base de Données PostgreSQL 🗄️

### 6. Configuration de l'Environnement
```powershell
# Naviguez vers le dossier de la base de données
cd ~/SchoolWork/BD-2

# Création du dossier pour la base de données
read -p "Entrez le nom du nouveau dossier : " nom_dossier
mkdir "$nom_dossier"
cd "$nom_dossier"

# Configuration de la connexion PostgreSQL
export PGPASSWORD='UlGBqeXlkG'
psql -U postgres -e -L "log${nom_dossier}.log"
\c "$nom_dossier"
unset PGPASSWORD
```

### 7. Compiler et Exécuter ▶️

Ouvrez App.xaml.cs dans Visual Studio Code

Cherchez le bouton d'exécution (▶️) en haut à droite

Cliquez pour compiler et exécuter votre application
