using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace Projet
{

    class DifferentialBackup : IBackup
    {
        // a la méthode qui sera utilisée pour la sauvegarde différentielle
        public void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, int getStateIndex, long fileCount, int getIndex, string getName)
        {
            //Initialiser un timer qui détermine le temps de sauvegarde.
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            // Obtenir les sous-répertoires pour le répertoire spécifié.
            DirectoryInfo dir = new DirectoryInfo(sourcePATH);

            
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourcePATH);
            }
            //afin de créer les sous-répertoires sources dans le répertoire de destination.
            if (copyDirs)
            {
                CreateDirs(destPATH, dir.GetDirectories());
            }


            FileInfo[] files = copyDirs ? dir.GetFiles("*", SearchOption.AllDirectories) : dir.GetFiles();
            var i = 0;
            foreach (var file in files)
            {
                if (File.Exists(file.FullName.Replace(sourcePATH, destPATH)))
                {
                    using (var sourcef = File.OpenRead(file.FullName))
                    {
                        // Ouverture du PATH
                        using (var destinationf = File.OpenRead(file.FullName.Replace(sourcePATH, destPATH)))
                        {
                            var hash1 = BitConverter.ToString(MD5.Create().ComputeHash(sourcef));
                            var hash2 = BitConverter.ToString(MD5.Create().ComputeHash(destinationf));
                            if (hash1 == hash2)
                            {
                                continue;
                            };
                        }
                    }
                    var jsonDataNo = File.ReadAllText(Etat.filePath); //Lis le fichier json
                    var stateListNo = JsonConvert.DeserializeObject<List<Etat>>(jsonDataNo) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON
                }
                file.CopyTo(file.FullName.Replace(sourcePATH, destPATH), true); //Copie des fichiers existants dans un nouveau
                i++;
                var filesLeftToDo = Directory.GetFiles(sourcePATH, "*", SearchOption.AllDirectories).Length - i;
                string progress = Convert.ToString((100 - (filesLeftToDo * 100) / fileCount)) + "%";
                var jsonData = File.ReadAllText(Etat.filePath); //Lis le fichier json
                var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON

                stateList[getStateIndex].NbFilesLeftToDo = filesLeftToDo.ToString();
                stateList[getStateIndex].Progression = progress;

                string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON
                File.WriteAllText(Etat.filePath, strResultJsonState);
                // Changer la langue de l'outpoot selon le choix de l'utilisateur lors du démarrage du programme
               
            }
            var jsonDataState2 = File.ReadAllText(Etat.filePath); //Lis le fichier json
            var stateList2 = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState2) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON

            stateList2[getStateIndex].Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            stateList2[getStateIndex].Progression = "100%";
            stateList2[getStateIndex].State = "END";

            string strResultJsonState2 = JsonConvert.SerializeObject(stateList2, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON
            File.WriteAllText(Etat.filePath, strResultJsonState2);

            var jsonDataState3 = File.ReadAllText(Work.filePath); //Lis le fichier json
            var stateList3 = JsonConvert.DeserializeObject<List<Work>>(jsonDataState3) ?? new List<Work>(); //convertir une chaîne en un objet pour JSON

            stateList3.Remove(stateList3[getIndex]);

            string strResultJsonState3 = JsonConvert.SerializeObject(stateList3, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON
            File.WriteAllText(Work.filePath, strResultJsonState3);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

            var jsonDataState4 = File.ReadAllText(Log.filePath); //Lis le fichier json
            var stateList4 = JsonConvert.DeserializeObject<List<Log>>(jsonDataState4) ?? new List<Log>(); //convertir une chaîne en un objet pour JSON
           
            stateList4.Add(new Log()
            {
                Name = stateList2[getStateIndex].Name,
                FileSource = stateList2[getStateIndex].SourceFilePath,
                FileTarget = stateList2[getStateIndex].TargetFilePath,
                FileSize = stateList2[getStateIndex].TotalFilesSize,
                FileTransferTime = elapsedTime,
                time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            });

            string strResultJsonState4 = JsonConvert.SerializeObject(stateList4, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON
            File.WriteAllText(Log.filePath, strResultJsonState4);
        }
        private void CreateDirs(string path, DirectoryInfo[] dirs)
        {
            foreach (var dir in dirs)
            {

                if (!Directory.Exists(Path.Combine(path, dir.Name)))
                {
                    Directory.CreateDirectory(Path.Combine(path, dir.Name));
                }
                CreateDirs(Path.Combine(path, dir.Name), dir.GetDirectories());
            }
        }
    }
}
