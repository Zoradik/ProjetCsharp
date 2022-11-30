# EasySave

EASY SAVE is a logiciel that help user saving their data.

- <a href="#prerequisites">Prerequisites</a> 
- <a href="#user-guide">User Guide</a>
  - <a href="#language">Language</a>
  - <a href="#execute-a-backup-work">Execute a backup work</a>
- <a href="#licence">Licence</a> 



 
## Prerequisites

This application will need the folowing requirement to run smoothly :
 - Windows 10 or more recent
 - 4go of RAM or more
 - "ConsoleTables" package 
 - "Newtonsoft.Json" package 


## User Guide

### V1  Console App

The user will be guided through the EasySave experience. Follow the instruction displayed on the console and answer with numbers on your keyboard

### Language

You can choose the Language of the console app everytime the app opens. Enter ``1`` for english and ``2`` for french.

![language](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/language.png)

#### Add a backup work

To create a backup work, enter ``1``:
then the program will ask you severals informations :
  1. a name to identify your Save
  2. a source directory, where the program will copy the files from
  3. a target directory, where the program will paste the file in
  4. a type of save, complete ou differencial

![add](https://github.com/Zoradik/ProjetCsharp/blob/develop/ProjectCsharp/Images/add.png)

P.S. : 
Full backup: consists of copying all the files and folders of a system, storing all the data.
Differential backup: only files modified since the last full backup are backed up.

Once the type specified : the backup work begin. The app show you the progression of the work.

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

