﻿using System;
using System.IO;

namespace Projet
{
    class Program
    {
        static void Main(string[] args)
        {
            // une boucle utilisée pour les langues
            while (true)
            {
                Console.WriteLine("________________________________________________________________________________________________________________________");
                Console.WriteLine("__________________________________________OOOOOOO_______O_______OOOOO___OO_____OO_______________________________________");
                Console.WriteLine("__________________________________________OOO__________O_O______OO_______OO___OO________________________________________");
                Console.WriteLine("__________________________________________OOOOO______OOOOOOO____OOOOO_____OOOO__________________________________________");
                Console.WriteLine("__________________________________________OOO_______OO_____OO_____OOO______OO___________________________________________");
                Console.WriteLine("__________________________________________OOOOOOO__OOO_____OOO__OOOOO______OO___________________________________________");
                Console.WriteLine("________________________________________________________________________________________________________________________");
                Console.WriteLine("|                                                                                                                      |");
                Console.WriteLine("________________________________________________________________________________________________________________________");
                Console.WriteLine("________________________________________OOOOO_________O_______OO______OO___OOOOOOO______________________________________");
                Console.WriteLine("________________________________________OO___________O_O_______OO____OO____OOO__________________________________________");
                Console.WriteLine("________________________________________OOOOO______OOOOOOO______OO__OO_____OOOOO________________________________________");
                Console.WriteLine("__________________________________________OOO_____OO_____OO______OOOO______OOO__________________________________________");
                Console.WriteLine("________________________________________OOOOO____OOO_____OOO______OO_______OOOOOOO______________________________________");
                Console.WriteLine("________________________________________________________________________________________________________________________");

                Console.WriteLine("Choose a language");
                Console.Write("1. English \t");
                Console.Write("2. Français\n");

                int inputLg = Convert.ToInt32(Console.ReadLine());

                if (inputLg == 1)
                {
                    Language.language = "EN";
                    break;
                }
                else if (inputLg == 2)
                {
                    Language.language = "FR";
                    break;
                }
                else
                {
                    Console.WriteLine("Bad entry, you can select <1> or <2>\n");
                    continue;
                }
            }

            // Boucle globale du prgramme
            while (true)
            {
                
                if (Language.language == "FR")
                {
                    Console.WriteLine("Choisissez le travail que vous souhaitez executer : ");
                    Console.Write("1. Ajoutez un travail de sauvegarde \t");
                    Console.Write("2. Executez un travail de sauvegarde\n");
                    Console.WriteLine("3. Quittez l'application\n");

                    string input = Console.ReadLine();
                    if (input == "1") //Ajouter un travail dans work
                    {
                        Console.Write("Entrez le nom d'un travail de sauvegarde :");
                        string inputName = Console.ReadLine();
                        Console.WriteLine("");
                        Console.Write("Entrer le chemin répertoire source : (Exemple de format: C:\\Source)");
                        string inputSourcePath = Console.ReadLine();
                        Console.WriteLine("");
                        Console.Write("Entrer le chemin répertoire cible : (Exemple de format: C:\\Destination)");
                        string inputDestinationPath = Console.ReadLine();
                        Console.WriteLine("");
                        Console.WriteLine("Choisissez un type de sauvegarde :\n");
                        Console.Write("1. Sauvegarde complète \t");
                        Console.WriteLine("2. Sauvegarde Différentielle\n");

                        input = Console.ReadLine();

                        if (input == "1")
                        {
                            try
                            {
                                var verifDest = Directory.GetFiles(inputDestinationPath, "*", SearchOption.AllDirectories);
                                int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                                EasySave backup = new EasySave();
                                long size = backup.GetFileSizeSumFromDirectory(inputSourcePath);
                                string inputType = "Full";
                                backup.addWork(size, fCount, inputName, inputSourcePath, inputDestinationPath, inputType);//Ajout dans le travail
                            }
                            catch
                            {
                                Console.Write("Le répertoire source ou de destination précisé est erroné"); //Si le répertoire n'est pas répertorié
                            }
                        }
                        else if (input == "2")
                        {
                            try
                            {
                                var verifDest = Directory.GetFiles(inputDestinationPath, "*", SearchOption.AllDirectories).Length;
                                int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                                EasySave backup = new EasySave();
                                long size = backup.GetFileSizeSumFromDirectory(inputSourcePath);
                                string inputType = "Differential";
                                backup.addWork(size, fCount, inputName, inputSourcePath, inputDestinationPath, inputType);
                            }
                            catch
                            {
                                Console.WriteLine("Le répertoire source ou de destination précisé est erroné\n");
                            }
                        }
                        else
                        {

                            Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3>\n"); //Protection d'erreur

                        }

                    }
                    else if (input == "2") //Tableau qui présentera l'ensemble des travaux créer 
                    {
                        Console.WriteLine("Voici les différents travaux de sauvegardes :");

                        EasySave backup = new EasySave();
                        backup.displayWorks();
                        Console.WriteLine("");

                        Console.Write("1. Execution d'un des travaux de sauvegarde \t");
                        Console.WriteLine("2. Execution séquentielle de l'ensemble des travaux \n");

                        input = Console.ReadLine();

                        if (input == "1") //choix demandé
                        {
                            backup.displayWorks();
                            Console.WriteLine("Veuillez sélectionner l'index correspondant au travail de sauvegarde souhaité \n");

                            input = Console.ReadLine();

                            switch (input) //choix de job a executé au nombre maximum de 5 
                            {
                                case "1":
                                    Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 1\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "2":
                                    Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 2\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "3":
                                    Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 3\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "4":
                                    Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 4\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "5":
                                    Console.WriteLine("Vous avez choisis le travail de sauvegarde numéro 5\n");
                                    backup.ExecuteWork(input);
                                    break;
                                default:
                                    Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3> ou <4> ou <5>");
                                    break;
                            }
                        }
                        else if (input == "2") //execution de tous les jobs crée
                        {
                            backup.ExecuteAllWork();
                        }
                        else
                        {
                            Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2>\n");
                        }
                    }
                    else if (input == "3") //Cas où on quitte l'application
                    {
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.WriteLine("Mauvaise entrée vous pouvez sélectionner <1> ou <2> ou <3>\n");
                    }
                }
                else if (Language.language == "EN") //Langage anglais
                {
                    Console.WriteLine("Choose the job you wish to use : ");
                    Console.Write("1. Add a backup work \t");
                    Console.Write("2. Execute a backup work\n");
                    Console.WriteLine("3. Leave application\n");

                    string input = Console.ReadLine();
                    if (input == "1") //On ajoute une sauvegarde (comme pour le francais)
                    {
                        Console.Write("Enter the name of a backup work :");
                        string inputName = Console.ReadLine();
                        Console.WriteLine("");
                        Console.Write("Enter the source directory path: (path example: D:\\Source)");
                        string inputSourcePath = Console.ReadLine();
                        Console.WriteLine("");
                        Console.Write("Enter the target directory path : (path example: D:\\Destination)");
                        string inputDestinationPath = Console.ReadLine();
                        Console.WriteLine("");
                        Console.WriteLine("Choose the backup type :\n");
                        Console.Write("1. Full Backup \t");
                        Console.WriteLine("2. Differential backup\n");

                        input = Console.ReadLine();

                        if (input == "1")
                        {
                            try
                            {
                                var verifDest = Directory.GetFiles(inputDestinationPath, "*", SearchOption.AllDirectories);
                                int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                                EasySave backup = new EasySave();
                                long size = backup.GetFileSizeSumFromDirectory(inputSourcePath);
                                string inputType = "Full";
                                backup.addWork(size, fCount, inputName, inputSourcePath, inputDestinationPath, inputType);
                            }
                            catch
                            {
                                Console.Write("The specified source or destination directory is incorrect");
                            }
                        }
                        else if (input == "2")
                        {
                            try
                            {
                                var verifDest = Directory.GetFiles(inputDestinationPath, "*", SearchOption.AllDirectories).Length;
                                int fCount = Directory.GetFiles(inputSourcePath, "*", SearchOption.AllDirectories).Length;
                                EasySave backup = new EasySave();
                                long size = backup.GetFileSizeSumFromDirectory(inputSourcePath);
                                string inputType = "Differential";
                                backup.addWork(size, fCount, inputName, inputSourcePath, inputDestinationPath, inputType);
                            }
                            catch
                            {
                                Console.WriteLine("The specified source or destination directory is incorrect\n");
                            }
                        }
                        else
                        {

                            Console.WriteLine("Bad entry, you can select <1> or <2> or <3>\n");

                        }

                    }
                    else if (input == "2")
                    {
                        Console.WriteLine("Here are the different backup works :");

                        EasySave backup = new EasySave();
                        backup.displayWorks();
                        Console.WriteLine("");

                        Console.Write("1. Execution one of the backup works \t");
                        Console.WriteLine("2. Sequential execution of all the backup works \n");

                        input = Console.ReadLine();

                        if (input == "1")
                        {
                            backup.displayWorks();
                            Console.WriteLine("Please select the index corresponding to the desired backup work \n");

                            input = Console.ReadLine();

                            switch (input)
                            {
                                case "1":
                                    Console.WriteLine("You have chosen the first Backup work\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "2":
                                    Console.WriteLine("You have chosen the second Backup work\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "3":
                                    Console.WriteLine("You have chosen the third Backup work\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "4":
                                    Console.WriteLine("You have chosen the fourth Backup work\n");
                                    backup.ExecuteWork(input);
                                    break;
                                case "5":
                                    Console.WriteLine("You have chosen the fifth Backup work\n");
                                    backup.ExecuteWork(input);
                                    break;
                                default:
                                    Console.WriteLine("Bad entry, you can select <1> or <2> or <3> or <4> or <5>");
                                    break;
                            }
                        }
                        else if (input == "2") //execution
                        {
                            backup.ExecuteAllWork();
                        }
                        else
                        {
                            Console.WriteLine("Bad entry, you can select <1> or <2>\n");
                        }
                    }
                    else if (input == "3")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Bad entry, you can select <1> or <2> or <3>\n");
                    }
                }
            }
        }
    }
}
