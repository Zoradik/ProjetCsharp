using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ProjetV3
{
    /// <summary>
    /// Logique d’interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex _mutex = null;
        protected override void OnStartup(StartupEventArgs e)
        {
            const string AppName = "EasySave";
            bool OpenApp;

            _mutex = new Mutex(true, AppName, out OpenApp);

            if (!OpenApp)
            {
                //L'application est déjà en cours d'exécution ! Quitter l'application
                MessageBox.Show("App is already running my dude !");
                Application.Current.Shutdown();
            }
            base.OnStartup(e);
        }
    }

}
