using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using ConsoleTables;
using System.Linq;

namespace Projet
{
    class EasySave
    {
        // Methode pour créer une backup pour le work
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            var jsonDataWork = File.ReadAllText(Work.filePath); //Lire le fichier JSON
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convertion un string en un objet pour JSON

            var jsonDataWork2 = File.ReadAllText(Etat.filePath); //Lire le fichier JSON
            var stateList2 = JsonConvert.DeserializeObject<List<Etat>>(jsonDataWork2) ?? new List<Etat>();

            bool nameExist = false;
            for (int i = 0; i < stateList2.Count; i++)
            {
                if (stateList2[i].Name == theName)
                {
                    nameExist = true;
                    break;
                }
                else
                {
                    nameExist = false;
                }
            }

            if (!nameExist)
            {
                if (workList.Count < 5) // La condition paramètre les limites des works qu'on peut créer 
                {
                    workList.Add(new Work() //paramètres que le fichier JSON contiendra
                    {
                        name = theName,
                        repS = theRepS,
                        repC = theRepC,
                        type = theType,
                    });


                    string strResultJsonWork = JsonConvert.SerializeObject(workList, Formatting.Indented);  //convertion un string en un objet pour JSON
                    File.WriteAllText(Work.filePath, strResultJsonWork); // Ecriture du fichier JSON

                    var jsonDataState = File.ReadAllText(Etat.filePath); //Lire le fichier JSON
                    var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState) ?? new List<Etat>(); //convertion un string en un objet pour JSON


                    stateList.Add(new Etat() //Paramètre que le JSON aura 
                    {
                        Name = theName,
                        SourceFilePath = theRepC,
                        TargetFilePath = theRepS,
                        Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        State = "INACTIVE",
                        TotalFilesToCopy = countfile.ToString(),
                        TotalFilesSize = filesize.ToString(),
                        NbFilesLeftToDo = "0",
                        Progression = "0%"
                    });

                    string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented); //convertion un string en un objet pour JSON
                    File.WriteAllText(Etat.filePath, strResultJsonState); // Ecriture dans le fichier JSON

                    // Choix du langage en fonction du choix principal de la langue
                    if (Language.language == "FR")
                    {

                        Console.WriteLine("Travail ajouté avec succès !\n");

                    }
                    else if (Language.language == "EN")
                    {

                        Console.WriteLine("Backup work created successfully !\n");

                    }
                }
                else
                {   // Choix du langage en fonction du choix principal de la langue
                    if (Language.language == "FR")
                    {

                        Console.WriteLine("Nombre maximal de travaux atteint !\n");

                    }
                    else if (Language.language == "EN")
                    {

                        Console.WriteLine("Maximum number of jobs reached !\n");

                    }
                }
            }
            else
            {   // Choix du langage en fonction du choix principal de la langue
                if (Language.language == "FR")
                {

                    Console.WriteLine("Un travail avec le meme nom existe déjà !\n");

                }
                else if (Language.language == "EN")
                {

                    Console.WriteLine("A job with the same name already exists!\n");
                }
            }
        }
        public void displayWorks() // une méthode qui permettra d'afficher tous nos travaux de sauvegarde
        {
            var jsonData = File.ReadAllText(Work.filePath); //Lire le fichier JSON
            var stateList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>(); //convertion un string en un objet pour JSON

            var dt = new ConsoleTable("index", "Name", "SourceFilePath", "TargetFilePath", "Type");
            foreach (var (state, i) in stateList.Select((el, i) => (el, i)))
            {
                dt.AddRow(i + 1, state.name, state.repS, state.repC, state.type);
            }

            dt.Write();
        }
        public void ExecuteWork(string inputUtilisateur) // une méthode qui permettra d'executer tous nos travaux de sauvegarde
        {
            var jsonData = File.ReadAllText(Work.filePath); //Lire le fichier JSON
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>(); //convertion string en un objet pour JSON

            if (workList.Count >= Convert.ToInt32(inputUtilisateur)) //cette condition permet à l'utilisateur de choisir la ligne exacte afin d'exécuter le travail de sauvegarde choisi.
            {
                int index = Convert.ToInt32(inputUtilisateur) - 1;
                string sourceDir = workList.ElementAt(index).repS;
                string backupDir = workList.ElementAt(index).repC;
                string name = workList.ElementAt(index).name;
                long filesNum = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories).Length;

                //cette condition est utilisée pour exécuter le type de sauvegarde choisi lors de la création
                if (workList.ElementAt(Convert.ToInt32(inputUtilisateur) - 1).type == "Differential")
                {
                    var jsonDataState2 = File.ReadAllText(Etat.filePath);
                    var stateList2 = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState2) ?? new List<Etat>();

                    int indexState = 0;
                    for (int i = 0; i < stateList2.Count; i++)
                    {
                        if (stateList2[i].Name == workList[index].name)
                        {
                            indexState = i;
                            break;
                        }
                    }

                    stateList2[indexState].State = "Active";

                    string strResultJsonState2 = JsonConvert.SerializeObject(stateList2, Formatting.Indented);
                    File.WriteAllText(Etat.filePath, strResultJsonState2);
                    // differential backup
                    DifferentialBackup SD = new DifferentialBackup();
                    SD.Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, index, name);

                }
                else
                {
                    var jsonDataState2 = File.ReadAllText(Etat.filePath);
                    var stateList2 = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState2) ?? new List<Etat>();

                    int indexState = 0;
                    for (int i = 0; i < stateList2.Count; i++)
                    {
                        if (stateList2[i].Name == workList[index].name)
                        {
                            indexState = i;
                            break;
                        }
                    }

                    stateList2[indexState].State = "Active";

                    string strResultJsonState2 = JsonConvert.SerializeObject(stateList2, Formatting.Indented);
                    File.WriteAllText(Etat.filePath, strResultJsonState2);
                    // backup complète
                    FullBackup SD = new FullBackup();
                    SD.Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, index, name);

                }

            }
            else
            {   // Changement du langage en fonction de la langue choisie 
                if (Language.language == "FR")
                {

                    Console.WriteLine("No backup job with entry " + inputUtilisateur + " found !\n");

                }
                else if (Language.language == "EN")
                {

                    Console.WriteLine("No backup job with entry " + inputUtilisateur + " found !\n");

                }
            }
        }
        public void ExecuteAllWork()
        {
            var jsonData = File.ReadAllText(Work.filePath);
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>();

            for (int i = 0; i < workList.Count; i++)
            {
                ExecuteWork("1");
            }
        }
        public long GetFileSizeSumFromDirectory(string searchDirectory) //une méthode permettant de calculer la taille d'un répertoire
        {
            var files = Directory.EnumerateFiles(searchDirectory);

            // obtenir la taille de tous les fichiers dans le répertoire courant
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();

            var directories = Directory.EnumerateDirectories(searchDirectory);

            // obtenir la taille de tous les fichiers dans tous les sous-répertoires
            var subDirSize = (from directory in directories select GetFileSizeSumFromDirectory(directory)).Sum();

            return currentSize + subDirSize;
        }
    }
}
