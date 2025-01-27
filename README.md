# Guide d'Installation et Configuration de BunnyCO

## Table des Matières

1. [Prérequis](#prérequis)
2. [Installation des Logiciels](#installation-des-logiciels)
3. [Configuration du Projet](#configuration-du-projet)
4. [Résolution des Problèmes](#résolution-des-problèmes)

## Prérequis

Avant de commencer, assurez-vous d'avoir :
* Un ordinateur sous Windows
* Une connexion Internet stable
* Les droits administrateur sur votre machine

## Installation des Logiciels

### 1. Git
```bash
# Vérifiez si Git est installé
git --version

# Si la commande n'est pas reconnue, téléchargez et installez Git :
# Visitez https://git-scm.com/download/win
2. Visual Studio Code
bashCopy# Téléchargez et installez Visual Studio Code
# Visitez https://code.visualstudio.com/
3. Extension .NET MAUI
bashCopy# Dans Visual Studio Code :
# 1. Appuyez sur Ctrl + Shift + X
# 2. Recherchez ".NET MAUI"
# 3. Cliquez sur Installer
4. SDK .NET
bashCopy# Téléchargez et installez le SDK .NET 9.0
# Visitez https://dotnet.microsoft.com/en-us/download/dotnet/9.0
Configuration du Projet
1. Ouvrir l'Invite de Commandes
bashCopy# Appuyez sur Windows + R
# Tapez 'cmd'
# Appuyez sur Enter
2. Cloner le Projet
bashCopy# Naviguez vers le dossier où vous voulez cloner le projet
cd C:\Users\VotreNom\Documents

# Clonez le dépôt
git clone https://github.com/CedGaumond/Gestion-Bunny.git

# Accédez au dossier du projet
cd Gestion-Bunny
cd BunnyCO
3. Restaurer les Dépendances
bashCopy# Dans le même CMD, exécutez :
dotnet workload restore

# Gardez le CMD ouvert pour la suite
4. Ouvrir dans Visual Studio Code
bashCopy# Dans le même CMD, exécutez :
code .
5. Compiler et Exécuter

Dans Visual Studio Code :
bashCopy# Naviguez vers App.xaml.cs dans l'explorateur de fichiers

Cherchez le bouton d'exécution (flèche) en haut à droite
Cliquez pour compiler et exécuter
