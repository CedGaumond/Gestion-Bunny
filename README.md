#################################################################
#                                                               #
#           Comment Configurer et Exécuter le Projet 🚀          #
#                                                               #
#################################################################

#------------------------#
# 1. Cloner le Projet 📂 #
#------------------------#

# Ouvrez l'Invite de Commandes (CMD)
# ⚠️ IMPORTANT : Gardez le CMD ouvert pour les étapes suivantes!

# Cloner le dépôt
git clone https://github.com/CedGaumond/Gestion-Bunny.git

# Navigation
cd Gestion-Bunny
cd BunnyCO

#----------------------------------------#
# 2. Installation de Visual Studio Code 💻 #
#----------------------------------------#

# Télécharger VS Code
Start-Process "https://code.visualstudio.com/"

# Lancer VS Code après installation
code

#----------------------------------------#
# 3. Installation Extension .NET MAUI ⚙️   #
#----------------------------------------#

# Ouvrir VS Code et installer l'extension
code --install-extension "ms-dotnettools.dotnet-maui"

#-----------------------------------#
# 4. Installation du SDK .NET 🔧     #
#-----------------------------------#

# Télécharger le SDK
Start-Process "https://dotnet.microsoft.com/en-us/download/dotnet/9.0"

# Vérifier l'installation
dotnet --version

#----------------------------------------#
# 5. Restauration des Dépendances 📝     #
#----------------------------------------#

# S'assurer d'être dans le bon dossier
cd BunnyCO
dotnet workload restore

#--------------------------------#
# 6. Compiler et Exécuter ▶️      #
#--------------------------------#

# Ouvrir le projet
code .

# Lancer la compilation et l'exécution
dotnet build
dotnet run

#################################################################
#                                                               #
# Note: Suivez ces étapes dans l'ordre pour réussir            #
#       l'installation du projet.                              #
#                                                               #
#################################################################
