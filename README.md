Guide d'Installation et de Configuration - Projet Gestion-Bunny
Prérequis
1. Installation de Git
Vérifiez d'abord si Git est déjà installé sur votre système :
bashCopygit --version
Si Git n'est pas installé, téléchargez-le depuis le site officiel : https://git-scm.com/download/win
2. Installation des Outils de Développement
Installez les outils suivants dans l'ordre indiqué :

Visual Studio Code

Rendez-vous sur https://code.visualstudio.com/
Téléchargez et exécutez l'installateur


Extension .NET MAUI pour Visual Studio Code

Ouvrez Visual Studio Code
Utilisez le raccourci Ctrl + Shift + X
Recherchez ".NET MAUI"
Procédez à l'installation de l'extension


SDK .NET 9.0

Téléchargez le SDK depuis https://dotnet.microsoft.com/en-us/download/dotnet/9.0
Suivez les instructions d'installation



Configuration du Projet
1. Préparation de l'Environnement
Ouvrez l'invite de commandes :
bashCopy# Utilisez Windows + R
# Saisissez 'cmd'
# Appuyez sur Entrée
2. Installation du Projet
bashCopy# Naviguez vers votre répertoire de travail
cd C:\Users\VotreNom\Documents

# Clonez le dépôt
git clone https://github.com/CedGaumond/Gestion-Bunny.git

# Accédez au projet
cd Gestion-Bunny
cd BunnyCO

# Restaurez les dépendances
dotnet workload restore
3. Lancement du Projet

Ouvrez le projet dans Visual Studio Code :

bashCopycode .

Pour exécuter l'application :

Localisez le fichier App.xaml.cs dans l'explorateur de fichiers
Repérez le bouton d'exécution (symbolisé par une flèche) dans la barre d'outils supérieure
Cliquez pour lancer la compilation et l'exécution
