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
        // a method that will allow to create a backupwork
        public void addWork(long filesize, int countfile, string theName, string theRepS, string theRepC, string theType)
        {
            var jsonDataWork = File.ReadAllText(Work.filePath); //Read the JSON file
            var workList = JsonConvert.DeserializeObject<List<Work>>(jsonDataWork) ?? new List<Work>(); //convert a string into an object for JSON

            var jsonDataWork2 = File.ReadAllText(Etat.filePath); //Read the JSON file
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
                if (workList.Count < 5) // this condition limits the number of backupwork that can be created (5 max)
                {
                    workList.Add(new Work() //parameter that the JSON file will contains
                    {
                        name = theName,
                        repS = theRepS,
                        repC = theRepC,
                        type = theType,
                    });


                    string strResultJsonWork = JsonConvert.SerializeObject(workList, Formatting.Indented);  //convert an object into a string for JSON
                    File.WriteAllText(Work.filePath, strResultJsonWork); // write in the JSON file

                    var jsonDataState = File.ReadAllText(Etat.filePath); //Read the JSON file
                    var stateList = JsonConvert.DeserializeObject<List<Etat>>(jsonDataState) ?? new List<Etat>(); //convert a string into an object for JSON


                    stateList.Add(new Etat() //parameter that the JSON file will contains
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

                    string strResultJsonState = JsonConvert.SerializeObject(stateList, Formatting.Indented); //convert an object into a string for JSON
                    File.WriteAllText(Etat.filePath, strResultJsonState); // write in the JSON file

                    // Switch the language of the outpoot according to the choice of the user when he started the program
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
                {   // Switch the language of the outpoot according to the choice of the user when he started the program
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
            {   // Switch the language of the outpoot according to the choice of the user when he started the program
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
        
