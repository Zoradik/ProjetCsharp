# EasySave

EASY SAVE est un logiciel qui aide l'utilisateur à sauvegarder ses données.

- <a href="#prérequis">Prérequis</a> 
- <a href="#mode-d'emploi">Mode d'emploi</a>
  - <a href="#langues">Langues</a>
  - <a href="#ajouter-un-travail-de-sauvegarde">Ajouter un travail de sauvegarde</a>
  - <a href="#execute-a-backup-work">Execute a backup work</a>
- <a href="#licence">Licence</a> 



 
## Prérequis

Cette application aura besoin des conditions suivantes pour fonctionner correctement :
 - Windows 10 ou plus récent
 - 4go de RAM ou plus
 - "ConsoleTables" sur VisualStudio 
 - "Newtonsoft.Json" sur VisualStudio 


## Mode d'emploi

### V1 Application Console

L'utilisateur sera guidé à travers l'expérience EasySave. Suivez les instructions affichées sur la console et répondez avec des chiffres sur votre clavier

### Langues

Vous pouvez choisir la langue de l'application console à chaque fois que l'application s'ouvre. Entrez ``1`` pour l'anglais et ``2`` pour le français.

![language](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/language.png)

#### Ajouter un travail de sauvegarde

To create a backup work, enter ``1``:
then the program will ask you severals informations :
  1. a name to identify your Save
  2. a source directory, where the program will copy the files from
  3. a target directory, where the program will paste the file in
  4. a type of save, complete ou differential

![add](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/add.png)

P.S. : 
Full backup: consists of copying all the files and folders of a system, storing all the data.
Differential backup: only files modified since the last full backup are backed up.

Once the type is specified : the backup work begin. The app show you the progression of the work.

![progess](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/progress.png)

#### Execute a backup work

To execute a backup work, enter ``2``:
Then the program will ask you the index among the table that you want to make.
Afterward, you will choose the type of save you want to make. Enter ``1`` for one save, or ``2`` to save all the work.

![execute](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/execute.png)

You will be able to see the progression of the save. 

![progess](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/progress.png)


#### Quit EasySave :

Enter ``3`` will close the EasySave.

## Licence

This project is under licence by the company ``ProSoft``

