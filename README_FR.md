# EasySave

EASY SAVE est un logiciel qui aide l'utilisateur à sauvegarder ses données.

- <a href="#prérequis">Prérequis</a> 
- <a href="#guide-utilisateur">Guide Utilisateur</a>
  - <a href="#langues">Langues</a>
  - <a href="#ajouter-un-travail-de-sauvegarde">Ajouter un travail de sauvegarde</a>
  - <a href="#exécuter-un-travail-de-sauvegarde">Exécuter un travail de sauvegarde</a>
  - <a href="#quitter-easysave">Quitter EasySave</a>
- <a href="#licence">Licence</a> 



 
## Prérequis

Cette application aura besoin des conditions suivantes pour fonctionner correctement :
 - Windows 10 ou plus récent
 - 4go de RAM ou plus
 - "ConsoleTables" sur VisualStudio 
 - "Newtonsoft.Json" sur VisualStudio 


## Guide Utilisateur

Pour lancer l'application il faut accéder et cliquer sur lancer sur Projet.csproj !'

### V1 Application Console

L'utilisateur sera guidé à travers l'expérience EasySave. Suivez les instructions affichées sur la console et répondez avec des chiffres sur votre clavier

### Langues

Vous pouvez choisir la langue de l'application console à chaque fois que l'application s'ouvre. Entrez ``1`` pour l'anglais et ``2`` pour le français.

![language](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/language.png)

#### Ajouter un travail de sauvegarde

Pour ajouter un travail de sauvegarde, entrez ``1`` :
Ensuite le programme vous demandera plusieurs informations :
   1. un nom pour identifier votre sauvegarde
   2. un répertoire source, où le programme copiera les fichiers
   3. un répertoire cible, où le programme dupliquera les fichiers
   4. un type de sauvegarde, complète ou différentielle

![add](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/add.png)

P.S. : 
Sauvegarde complète : consiste à copier tous les fichiers et dossiers d'un système, en stockant toutes les données.
Sauvegarde différentielle : seuls les fichiers modifiés depuis la dernière sauvegarde complète sont sauvegardés.

Une fois le type spécifié : le travail de sauvegarde commence. L'application vous montre la progression du travail de sauvegarde.

![progess](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/progress.png)

#### Exécuter un travail de sauvegarde

Pour exécuter un travail de sauvegarde, entrez ``2`` :
Ensuite le programme vous demandera l'index de la sauvegarde que vous voulez faire.
Ensuite, vous choisirez le type de sauvegarde que vous souhaitez effectuer. 
Entrez ``1`` pour une sauvegarde, ou ``2`` pour sauvegarder tous les travaux.

![execute](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/execute.png)

Vous pourrez voir la progression de la sauvegarde.

![progess](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/progress.png)


#### Quitter EasySave

Entrez ``3`` fermera EasySave.

## Licence

Ce projet est sous licence par la société ``ProSoft``

