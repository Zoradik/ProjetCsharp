using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using System.Threading;

namespace test2
{
    public class FullBackup : IBackup
    {


        public string Name;
        private bool condition_pause = true;
        public bool Condition_pause { get => condition_pause; set => condition_pause = value; }
        public bool SetStop { get => stop; set => stop = value; }
        private bool stop = false;



        // La méthode qui sera utilisée pour la sauvegarde complète 
        public void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, int getStateIndex, long fileCount, int getIndex, string getName)
        {
            Name = getName;
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
            List<FileInfo> files2 = new List<FileInfo>(files);
            var jsonn = File.ReadAllText(Settings.filePath);
            var Listt = JsonConvert.DeserializeObject<List<Settings>>(jsonn) ?? new List<Settings>();
            string[] extensionss = new string[] { Listt[0].extensionsAccepted };
            extensionss = extensionss[0].Split(',', ' ');
            //for (int j = 0; j < files2.Count; j++)
            // {
            //  if (extensionss.Contains(files2[j].Extension))
            // {
            //    FileInfo tompon = files2[j];
            //   files2.RemoveAt(j);
            //  files2.Insert(0, tompon);
            // }
            //MessageBox.Show(files[i].Extension);

            //}
            //files = files2.ToArray();
            var i = 0;

            var json = File.ReadAllText(Settings.filePath);
            var List = JsonConvert.DeserializeObject<List<Settings>>(json) ?? new List<Settings>();
            string[] extensions = new string[] { List[0].extensionsAccepted };
            extensions = extensions[0].Split(',', ' ');
            TimeSpan TimeToCrypt = TimeSpan.Zero;

            foreach (var file in files)
            {
                while (!Condition_pause) { }
                if (stop) break;

                if (extensions.Contains(file.Extension))
                {
                    var p = new Process();
                    p.StartInfo.FileName = @"..\..\..\CryptoSoft\CryptoSoft\CryptoSoft.exe";
                    p.StartInfo.Arguments = $"{file.FullName} {file.FullName.Replace(sourcePATH, destPATH)}";
                    Stopwatch timer = new Stopwatch();
                    timer.Start();
                    p.Start();
                    timer.Stop();
                    TimeToCrypt += stopWatch.Elapsed;

                    lock (Locker.Read_State)
                    {
                        var jsonState = File.ReadAllText(Etat.filePath); //Lis le fichier JSON
                        var stateListCrypt = JsonConvert.DeserializeObject<List<Etat>>(jsonState) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON

                        stateListCrypt[getStateIndex].TimeToCrypt = TimeToCrypt.ToString();

                        string ResultJsonState = JsonConvert.SerializeObject(stateListCrypt, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON



                        //mettre un lock

                        File.WriteAllText(Etat.filePath, ResultJsonState);

                    }


                }
                else
                {

                    file.CopyTo(file.FullName.Replace(sourcePATH, destPATH), true); //Copie des fichiers existants dans un nouveau
                }
                i++;
                lock (Locker.Read_State)
                {
                    ///////////////////////////////////////


                    var filesLeftToDo = Directory.GetFiles(sourcePATH, "*", SearchOption.AllDirectories).Length - i;
                    string progress = Convert.ToString((100 - (filesLeftToDo * 100) / fileCount)) + "%";
                    List<Etat> stateList;



                    var jsonData = File.ReadAllText(Etat.filePath); //Lis le fichier JSON
                    stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonData) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON


                    stateList[getStateIndex].NbFilesLeftToDo = filesLeftToDo.ToString();
                    stateList[getStateIndex].Progression = progress;

                    string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON





                    //mettre un lock

                    File.WriteAllText(Etat.filePath, strResultJsonState);

                }





                List<Etat> stateList2;
                string strResultJsonState2 = "";
                lock (Locker.Read_State)
                {
                    var jsonDataState2 = File.ReadAllText(Etat.filePath); //Lis le fichier JSON
                    stateList2 = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState2) ?? new List<Etat>(); //convertir une chaîne en un objet pour JSON





                    stateList2[getStateIndex].Time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");


                    strResultJsonState2 = JsonConvert.SerializeObject(stateList2, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON





                    //mettre un lock


                    File.WriteAllText(Etat.filePath, strResultJsonState2);

                }






                lock (Locker.Read_State)
                {
                    var jsonDataState3 = File.ReadAllText(Work.filePath); //Lis le fichier JSON
                    var stateList3 = JsonConvert.DeserializeObject<List<Work>>(jsonDataState3) ?? new List<Work>(); //convertir une chaîne en un objet pour JSON

                    try
                    {
                        stateList3.Remove(stateList3[getIndex]);
                    }
                    catch
                    {
                        //MessageBox.Show(getIndex.ToString());
                    }


                    string strResultJsonState3 = JsonConvert.SerializeObject(stateList3, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON





                    //mettre un lock   



                    File.WriteAllText(Work.filePath, strResultJsonState3);

                }






                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                Console.WriteLine("RunTime " + elapsedTime);

                List<Log> stateList4;
                lock (Locker.Read_State)
                {
                    var jsonDataState4 = File.ReadAllText(Log.filePath); //Lis le fichier JSON
                    stateList4 = JsonConvert.DeserializeObject<List<Log>>(jsonDataState4) ?? new List<Log>(); //convertir une chaîne en un objet pour JSON

                    stateList4.Add(new Log()
                    {
                        Name = stateList2[getStateIndex].Name,
                        FileSource = stateList2[getStateIndex].SourceFilePath,
                        FileTarget = stateList2[getStateIndex].TargetFilePath,
                        FileSize = stateList2[getStateIndex].TotalFilesSize,
                        FileTransferTime = elapsedTime,
                        time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        TimeToCrypt = TimeToCrypt.ToString(),

                    });

                    string strResultJsonState4 = JsonConvert.SerializeObject(stateList4, Formatting.Indented);  //convertir un objet en une chaîne de caractères pour JSON






                    //mettre un lock   

                    File.WriteAllText(Log.filePath, strResultJsonState4);


                }
            }





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