using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;

namespace Projet
{
    class EasySave
    {
        // Method qui peut ajouter une backup au work
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            var jsonDataWork = File.ReadAllText(Work.filePath); //Lis le fichier json
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convertir une chaîne en un objet pour JSON

            var jsonDataWork2 = File.ReadAllText(Etat.filePath); //Lis le fichier json
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
                if (workList.Count < 10000) // Limitations quasi illimités
                {
                    workList.Add(new Work() //Paramètres json avec des contraintes
                    {
                        name = theName,
                        repS = theRepS,
                        repC = theRepC,
                        type = theType,
                    });


                    string strResultJsonWork = JsonConvert.SerializeObject(workList, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON
                    File.WriteAllText(Work.filePath, strResultJsonWork); // Ecriture dans le fichier JSON

                    var jsonDataState = File.ReadAllText(Etat.filePath); //Lis le fichier json
                    var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON


                    stateList.Add(new Etat() //Paramètres json avec des contraintes
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

                    string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented); //convertir un objet en une chaîne de caractères pour JSON
                    File.WriteAllText(Etat.filePath, strResultJsonState); // Ecriture dans le fichier JSON

                    // Changer la langue de l'outpoot selon le choix de l'utilisateur lors du démarrage du programme


                    MessageBox.Show("Travail ajouté avec succès !\n");

                    
                   

                      

                    
                }
                else
                {   // Changer la langue de l'outpoot selon le choix de l'utilisateur lors du démarrage du programme



                    MessageBox.Show("Nombre maximal de travaux atteint !\n");

                    
                   
                   
                }
            }
            else
            {   // Changer la langue de l'outpoot selon le choix de l'utilisateur lors du démarrage du programme


                MessageBox.Show("Un travail avec le meme nom existe déjà !\n");

                
              
            }
        }
        public List<Work> displayWorks() // une méthode qui permettra d'afficher tous nos travaux de sauvegarde
        {
            var jsonData = File.ReadAllText(Work.filePath); //Lis le fichier json
            var stateList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>();

            return stateList;
        }
        public void ExecuteWork(string inputUtilisateur) // une méthode qui permettra d'exécuter un travail de sauvegarde créé
        {
            var jsonData = File.ReadAllText(Work.filePath); //Lis le fichier json
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>(); //convertir une chaîne en un objet pour JSON

            if (workList.Count >= Convert.ToInt32(inputUtilisateur)) //cette condition permet à l'utilisateur de choisir la ligne exacte afin d'exécuter le travail de sauvegarde choisi.
            {
                int index = Convert.ToInt32(inputUtilisateur) - 1;
                string sourceDir = workList.ElementAt(index).repS;
                string backupDir = workList.ElementAt(index).repC;
                string name = workList.ElementAt(index).name;
                long filesNum = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories).Length;

                //cette condition est utilisée pour exécuter le type de sauvegarde choisi lors de la création. 
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
                    // backup différentielle
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
                    // complete backup
                    FullBackup SD = new FullBackup();
                    SD.Sauvegarde(sourceDir, backupDir, true, indexState, filesNum, index, name);

                }

            }
            else
            {   // Changer la langue de l'outpoot selon le choix de l'utilisateur lors du démarrage du programme


                MessageBox.Show("No backup job with entry " + inputUtilisateur + " found !\n");

              
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
        public long GetFileSizeSumFromDirectory(string searchDirectory) //une méthode qui permet de calculer la taille d'un répertoire (sous-répertoire inclus)
        {
            var files = Directory.EnumerateFiles(searchDirectory);

            // obtenir la taille de tous les fichiers du répertoire courant
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();

            var directories = Directory.EnumerateDirectories(searchDirectory);

            // obtenir la taille de tous les fichiers dans tous les sous-répertoires
            var subDirSize = (from directory in directories select GetFileSizeSumFromDirectory(directory)).Sum();

            return currentSize + subDirSize;
        }
    }
}
