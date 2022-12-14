using System;
using System.Collections.Generic;
using System.Text;

namespace Final
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

    }
}
