# Comment Configurer et Exécuter le Projet 🚀

## Première Étape : Cloner le Projet 📂

### 1. Cloner le Dépôt Git
# Ouvrez l'Invite de Commandes (CMD)
# ⚠️ IMPORTANT : Gardez le CMD ouvert, il sera nécessaire pour les étapes suivantes!

# Choisissez un dossier où vous voulez cloner le projet
# Clonez le dépôt
git clone https://github.com/CedGaumond/Gestion-Bunny.git

# Naviguez vers le dossier du projet
cd Gestion-Bunny
cd BunnyCO

### 2. Installation de Visual Studio Code 💻
# Téléchargez Visual Studio Code
Start-Process "https://code.visualstudio.com/"
# Suivez l'assistant d'installation
# Lancez VS Code après l'installation

### 3. Installation de l'Extension .NET MAUI ⚙️
# Lancez Visual Studio Code
code
# Appuyez sur Ctrl + Shift + X pour ouvrir les Extensions
# Recherchez ".NET MAUI"
# Cliquez sur "Installer"

### 4. Installation du SDK .NET 🔧
# Téléchargez le SDK .NET
Start-Process "https://dotnet.microsoft.com/en-us/download/dotnet/9.0"
# Exécutez l'installateur
# Vérifiez l'installation
dotnet --version

### 5. Restauration des Dépendances 📝
# Dans le même CMD que précédemment
# Assurez-vous d'être dans le dossier BunnyCO
dotnet workload restore

### 6. Compiler et Exécuter ▶️
# Ouvrez le projet dans VS Code
code .
# Ouvrez App.xaml.cs
# Cherchez le bouton d'exécution (▶️) en haut à droite
# Cliquez pour compiler et exécuter votre application

# Note: Assurez-vous de suivre ces étapes dans l'ordre pour une installation réussie.
