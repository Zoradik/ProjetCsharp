using System;
using System.Collections.Generic;
using System.Text;

namespace test2
{
    class ChangeLang
    {
        public string
            printNoSaveWorkFound = "No backup job found with entry",
            printImpossibleToRunBuissnessSoftwareRunning = "Impossible to run the save work, a buisness software is running. ",
            printSaveWorkAlreadyExist = "A save work with the same name already exist",
            printSaveWorkAdded = "Save work successfully added",
            printError = "An error has occured",
            printFillOut = "please complete all fields",
            printExecutedworks = "all save works has been executed",
            printHasBeenExecuted = "has been successfully executed",
            printEmptyTargetDir = "The specified destination directory is empty",
            printEmptySourceDir = "The specified source directory is empty",
            printPaused = "You paused the save work(s)",
            printStop = "You Stopped the save work(s)",
            printResume = "You resumed the save work(s)";



        public string changeMessageBoxLang(string lang)
        {
            if (lang == "french")
            {
                printNoSaveWorkFound = "Pas de travail de sauvegarde trouvé avecc cette entrée ";
                printImpossibleToRunBuissnessSoftwareRunning = "Impossible de lancer car un logiciel métier est en cours d'éxecution";
                printSaveWorkAlreadyExist = "Un travail avec le même nom existe déjà !";
                printSaveWorkAdded = "Travail ajouté avec succès !";

                printError = "Une erreur est survenue";
                printFillOut = "Veuillez remplir tous les champs";
                printExecutedworks = "Tous les travaux sont executées ";
                printHasBeenExecuted = "à été executé avec succes";
                printEmptyTargetDir = "Le répertoire de destination précisé est vide";
                printEmptySourceDir = "Le répertoire source précisé est vide";
                printPaused = "Vous avez mis en pause le ou les travaux de sauvegarde";
                printStop = "Vous avez arrêté le ou les travaux de sauvegarde";
                printResume = "Vous avez repris le ou les travaux de sauvegarde";

                return "Language changé vers français avec succès";
            }
            if (lang == "english")
            {
                printNoSaveWorkFound = "No backup job found with entry";
                printImpossibleToRunBuissnessSoftwareRunning = "Impossible to run the save work, a buisness software is running. ";
                printSaveWorkAlreadyExist = "A save work with the same name already exist";
                printSaveWorkAdded = "Save work successfully added";

                printError = "An error has occured";
                printFillOut = "please complete all fields";
                printExecutedworks = "all save works has been executed";
                printHasBeenExecuted = "has been successfully executed";
                printEmptyTargetDir = "The specified destination directory is empty";
                printEmptySourceDir = "The specified source directory is empty";
                printPaused = "You paused the save work(s)";
                printStop = "You Stopped the save work(s)";
                printResume = "You resumed the save work(s)";

                return "Language successfully changed to english";
            }
            else
            {
                return "error";
            }

        }







        /*
        * try to pass the views components in params but that dont work
        * 

       public string[] englishTranslation = {
           "Add Save Work",
           "Run Save Work",
           "Settings",
           "The best backup management software you can have !",
           "The best backup management software you can have !",
           "The best backup management software you can have !",
           "Save work name :",
           "Source path :",
           "Target path :",
           "Save work type :",
           "Full",
           "Differential",
           "Files encrypted :",
           "Crypted",
           "Add",
           "Start Sequential Run",
           "Number",
           "Start Single Run",
           "Progress",
           "Pause",
           "Stop",
           "French",
           "English",
           "Open User Guide",
           "Open Config",
           "Open Logs",
           "Configure Buisness Software",
           "Configure Type of Files to Encrypt",
           "An error has occured",
           "please complete all fields",
           "all save works has been executed",
           "has been successfully executed",
           "The specified destination directory is empty",
           "The specified source directory is empty"
       };
       public string[] frenchTranslation = {
           "Ajouter un travail de sauvegarde",
           "Executer un travail de sauvegarde",
           "Paramètres",
           "Le meilleur logiciel de gestion de sauvegarde que vous puissiez avoir !",
           "Le meilleur logiciel de gestion de sauvegarde que vous puissiez avoir !",
           "Le meilleur logiciel de gestion de sauvegarde que vous puissiez avoir !",
           "Nom du TS :",
           "Chemin source :",
           "Chemin cible :",
           "Type de TS :",
           "Complet",
           "Différentiel",
           "Fichiers chiffrés :",
           "Chiffrés",
           "Ajouter",
           "Lancement séquentiel",
           "Nombre",
           "Lancement unique",
           "Progression",
           "Pause",
           "Stop",
           "Français",
           "Anglais",
           "Ouvrir le guide utilisateur",
           "Ouvrir la configuration",
           "Ouvrir les logs",
           "Configurer le Logiciel Métier",
           "Configurer le type de fichier à chiffrer",
           "Une erreur est survenue",
           "Veuillez remplir tous les champs",
           "Tous les travaux sont executées ",
           "à été executé avec succes",
           "Le répertoire de destination précisé est vide",
           "Le répertoire source précisé est vide"
       };


       public void changeLangFunction(Object[] content, string[] translation)
       {
           if (content.Length == translation.Length)
           {
               for(int i=0;i< content.Length;i++)
               {
                   content = translation;
               }
           }
       }


      */
    }
}
