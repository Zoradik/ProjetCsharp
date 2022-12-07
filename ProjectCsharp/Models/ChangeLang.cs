using System;
using System.Collections.Generic;
using System.Text;

namespace test2
{
    class ChangeLang
    {
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
            "Ouvrir les logs"
        };

        public void changeLangFunction(Object[] content, string[] translation)
        {
            if (content.Length == translation.Length)
            {
                for (int i = 0; i < content.Length; i++)
                {
                    content = translation;
                }
            }
        }


    }
}
