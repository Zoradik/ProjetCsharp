using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjetV3
{
    //interface de sauvegarde qui contient la définiton de la méthode de sauvegarde (abstract factory interface)
    interface IBackup
    {
        void Sauvegarde(string sourcePATH, string destPATH, bool copyDirs, int getStateIndex, long fileCount, int getIndex, string getName);
    }
}