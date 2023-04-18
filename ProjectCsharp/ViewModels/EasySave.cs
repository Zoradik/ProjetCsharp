using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Diagnostics;
using System.Threading;

namespace ProjetV3
{
    class EasySave
    {
        //initialiser les threads ici
        Thread threadA, threadB;


        //objet pour le verrou de thread pour la section critique
        static readonly object _object = new object();

        ChangeLang lang = new ChangeLang();
        public int pgBarValue = 0;

        // La méthode qui permettera de créer d'un travail de sauvegarde 
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            var jsonDataWork = File.ReadAllText(Work.filePath); //Lis le fichier JSON
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convertir une chaîne en un objet pour JSON

            var jsonDataWork2 = File.ReadAllText(Etat.filePath); //Lis le fichier JSON
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

                workList.Add(new Work() //paramètres que le fichier JSON contiendra
                {
                    name = theName,
                    repS = theRepS,
                    repC = theRepC,
                    type = theType,
                });


                string strResultJsonWork = JsonConvert.SerializeObject(workList, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON
                File.WriteAllText(Work.filePath, strResultJsonWork); // écrire dans le fichier JSON

                var jsonDataState = File.ReadAllText(Etat.filePath); //Lis le fichier JSON
                var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON


                stateList.Add(new Etat() //paramètres que le fichier JSON contiendra
                {
                    Name = theName,
                    SourceFilePath = theRepS,
                    TargetFilePath = theRepC,
                    Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    State = "INACTIVE",
                    TotalFilesToCopy = countfile.ToString(),
                    TotalFilesSize = filesize.ToString(),
                    NbFilesLeftToDo = "0",
                    Progression = "0%"
                });

                string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented); //convertir un objet en une chaîne de caractères pour JSON
                File.WriteAllText(Etat.filePath, strResultJsonState); // écrire dans le fichier JSON

                // Changer la langue de sortie selon le choix de l’utilisateur quand il a commencé le programme


                MessageBox.Show(lang.printSaveWorkAdded);








            }
            else
            {   // Changer la langue de sortie selon le choix de l’utilisateur quand il a commencé le programme


                MessageBox.Show(lang.printSaveWorkAlreadyExist);



            }
        }
        public List<Work> displayWorks() // une méthode qui permettra d’afficher tout nos travaux de sauvegarde
        {
            var jsonData = File.ReadAllText(Work.filePath); //Lis le fichier JSON
            var stateList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>();

            return stateList;
        }
        public void ExecuteWork(string inputUtilisateur) // une méthode qui permettra d'exécuter un travail de sauvegarde créé 
        {
            //début de la section critique
            lock (_object)
            {
                if (Process.GetProcessesByName("Calculator").Length == 0)
                {
                    var jsonData = File.ReadAllText(Work.filePath); //Lis le fichier JSON
                    var workList = JsonConvert.DeserializeObject<List<Work>>(jsonData) ?? new List<Work>(); //convertir une chaîne en un objet pour JSON

                    if (workList.Count >= Convert.ToInt32(inputUtilisateur)) //condition qui permet à l’utilisateur de choisir la ligne exacte afin d’exécuter le travail de sauvegarde choisi
                    {
                        int index = Convert.ToInt32(inputUtilisateur) - 1;
                        string sourceDir = workList.ElementAt(index).repS;
                        string backupDir = workList.ElementAt(index).repC;
                        string name = workList.ElementAt(index).name;
                        long filesNum = Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories).Length;

                        //cette condition est utilisée pour exécuter le type de sauvegarde choisi dans la création 
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
                            // sauvegarde différentielle
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
                    {   // Changer la langue de sortie selon le choix de l’utilisateur quand il a commencé le programme

                        MessageBox.Show($"{lang.printNoSaveWorkFound} {inputUtilisateur} \n");
                    }
                }
                else
                {
                    //mettre en pause puis lancer quand le logiciel métier est fermé
                    MessageBox.Show(lang.printImpossibleToRunBuissnessSoftwareRunning);
                }
                //fin de la section critique 
            }
        }
        public void ExecuteAllWork()
        {
            //Obtenir les données JSON
            var workList = JsonConvert.DeserializeObject<List<Work>>(File.ReadAllText(Work.filePath)) ?? new List<Work>();
            int q = workList.Count;

            for (int i = 0; i < q; i += 1)
            {
                threadA = new Thread(() => ExecuteWork(Convert.ToString(i)));
                if (i + 1 < q)
                {
                    threadB = new Thread(() => ExecuteWork(Convert.ToString(i + 1)));
                    threadB.Start();
                }
                threadA.Start();
                pgBarValue += 100;
            }
        }

        public static void nothing() { }

        public void stopThread()
        {

        }
        public bool pausedThread;
        public void resumeThread()
        {
            pausedThread = false;
            //throw new ThreadInterruptedException();
        }
        public void pauseThread()
        {
            while (pausedThread == true)
            {
                Thread.Sleep(Timeout.Infinite);
            }
           
        }

        public int change_pgBarValue()
        {
            return pgBarValue;
        }
        public long GetFileSizeSumFromDirectory(string searchDirectory) //une méthode qui permet de calculer la taille d’un répertoire (sous-répertoires inclus)
        {
            var files = Directory.EnumerateFiles(searchDirectory);

            // obtenir la taille de tous les fichiers dans le répertoire 
            var currentSize = (from file in files let fileInfo = new FileInfo(file) select fileInfo.Length).Sum();

            var directories = Directory.EnumerateDirectories(searchDirectory);

            // Obtenir la taille de tous les fichiers dans tous les sous-répertoires
            var subDirSize = (from directory in directories select GetFileSizeSumFromDirectory(directory)).Sum();

            return currentSize + subDirSize;
        }

    }
}