using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Projet
{
    //une interface de sauvegarde qui contient la définition de la méthode de sauvegarde (interface de fabrique abstraite)
    interface IBackup
    {
        void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, int getStateIndex, long fileCount, int getIndex, string getName);
    }
}
