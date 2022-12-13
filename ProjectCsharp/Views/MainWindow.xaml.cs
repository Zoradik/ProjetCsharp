using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace ProjetV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChangeLang lang = new ChangeLang();

        public MainWindow()
        {
            //InitializeComponent();
        }


        //tab1 add save work
        private void tab1ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

            if (tab1TextBoxName.Text != "" && tab1TextBoxSourcePath.Text != "" && tab1TextBoxTargetPath.Text != "" && tab1SelectType.Text != "")
            {
                ProjetV3.EasySave addWork = new ProjetV3.EasySave();

                int fCount = Directory.GetFiles(tab1TextBoxSourcePath.Text, "*", SearchOption.AllDirectories).Length;
                long size = addWork.GetFileSizeSumFromDirectory(tab1TextBoxSourcePath.Text);

                try
                {
                    addWork.addWork(size, fCount, tab1TextBoxName.Text, tab1TextBoxSourcePath.Text, tab1TextBoxTargetPath.Text, tab1SelectType.Text);
                    tab1TextBoxName.Text = "";
                    tab1TextBoxSourcePath.Text = "";
                    tab1TextBoxTargetPath.Text = "";
                    tab1SelectType.Text = "";
                    tab1SaveWork_Loaded(sender, e);

                }
                catch
                {


                    MessageBox.Show(lang.printError);


                    tab1TextBoxName.Text = "";
                    tab1TextBoxSourcePath.Text = "";
                    tab1TextBoxTargetPath.Text = "";
                    tab1SelectType.Text = "";
                }
            }
            else
            {



                MessageBox.Show(lang.printFillOut);

            }

        }

        public int pgBarValue = 0;
        //tab2 run save work
        private void tab2ButtonStartSequentialRun_Click(object sender, RoutedEventArgs e)
        {
            pgBarValue = 0;
            ProjetV3.EasySave exeseqWork = new ProjetV3.EasySave();
            try
            {
                exeseqWork.ExecuteAllWork();
                tab2ProgessBar.Value = exeseqWork.change_pgBarValue();

                MessageBox.Show(lang.printExecutedworks);
                tab2DataGrid_Loaded(sender, e);
                tab1SaveWork_Loaded(sender, e);

            }
            catch
            {
                MessageBox.Show(lang.printError);
            }
        }

        private void tab2ButtonStartSingleRun_Click(object sender, RoutedEventArgs e)
        {
            pgBarValue = 0;
            if (tab2TextBoxNumber.Text != "")
            {
                ProjetV3.EasySave exeWork = new ProjetV3.EasySave();

                exeWork.ExecuteWork(tab2TextBoxNumber.Text);
                pgBarValue += 100;
                tab2ProgessBar.Value = pgBarValue;

                tab1SaveWork_Loaded(sender, e);
                MessageBox.Show(tab2TextBoxNumber.Text + lang.printHasBeenExecuted);
            }
            else
            {
                MessageBox.Show(lang.printFillOut);

            }
        }

        private void tab2ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            EasySave easySave = new EasySave();
            easySave.pauseThread();
            MessageBox.Show(lang.printPaused);
        }

        private void tab2ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            EasySave easySave = new EasySave();
            easySave.stopThread();
            pgBarValue = 0;
            MessageBox.Show(lang.printStop);
        }

        private void tab2ButtonResume_Click(object sender, RoutedEventArgs e)
        {
            EasySave easySave = new EasySave();
            easySave.resumeThread();
            MessageBox.Show(lang.printResume);
        }

        //tab3 Settings 

        private void tab3ButtonFrench_Click(object sender, RoutedEventArgs e)
        {

            Button clickedButton = (Button)sender;
            //clickedButton.Content = "...button clicked...";
            clickedButton.IsEnabled = false;
            tab3ButtonEnglish.IsEnabled = true;

            tab1SaveWork.Header = "Ajouter un travail de sauvegarde";
            tab2RunWork.Header = "Executer un travail de sauvegarde";
            tab3Settings.Header = "Paramètres";
            //tab1TextBlockDescription.Text = "Fait ca vite !";
            //tab2TextBlockDescription.Text = "Fait ca vite !";
            //tab3TextBlockDescription.Text = "Fait ca vite !";
            tab1LabelName.Content = "Nom du TS :";
            tab1LabelSourcePath.Content = "Chemin source :";
            tab1LabelTargetPath.Content = "Chemin cible :";
            tab1LabelType.Content = "Type de TS :";
            tab1SelectTypeFull.Content = "Complet";
            tab1SelectTypeDifferential.Content = "Différentiel";
            tab1LabelCrypted.Content = "Chiffrement ?";
            tab1CheckCrypted.Content = "Chiffrés";
            tab1ButtonAdd.Content = "Ajouter";
            tab2ButtonStartSequentialRun.Content = "Lancement séquentiel";
            tab2TextBoxNumber.Text = "Nombre";
            tab2ButtonStartSingleRun.Content = "Lancement unique";
            tab2LabelProgessBar.Content = "Progression";
            tab2ButtonPause.Content = "Pause";
            tab2ButtonStop.Content = "Stop";
            tab2ButtonResume.Content = "Reprendre";
            tab3ButtonFrench.Content = "Français";
            tab3ButtonEnglish.Content = "Anglais";
            tab3ButtonUserGuide.Content = "Ouvrir le guide utilisateur";
            tab3ButtonOpenConfig.Content = "Ouvrir la configuration";
            tab3ButtonOpenLogs.Content = "Ouvrir les logs";
            tab3ButtonConfigureBuisnessSoftware.Content = "Configurer le Logiciel Métier";
            tab3ButtonConfigureTypeFilesToEncrypt.Content = "Configurer le type de fichier à chiffrer";

            MessageBox.Show(lang.changeMessageBoxLang("french"));
        }
        private void tab3ButtonEnglish_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            //clickedButton.Content = "...button clicked...";
            clickedButton.IsEnabled = false;
            tab3ButtonFrench.IsEnabled = true;

            tab1SaveWork.Header = "Add Save Work";
            tab2RunWork.Header = "Run Save Work";
            tab3Settings.Header = "Settings";
            //tab1TextBlockDescription.Text = "Make it quick !";
            tab2TextBlockDescription.Text = "Make it quick !";
            tab3TextBlockDescription.Text = "Make it quick !";
            tab1LabelName.Content = "Save work name :";
            tab1LabelSourcePath.Content = "Source path :";
            tab1LabelTargetPath.Content = "Target path :";
            tab1LabelType.Content = "Save work type :";
            tab1SelectTypeFull.Content = "Full";
            tab1SelectTypeDifferential.Content = "Differential";
            tab1LabelCrypted.Content = "Files encrypted :";
            tab1CheckCrypted.Content = "Crypted";
            tab1ButtonAdd.Content = "Add";
            tab2ButtonStartSequentialRun.Content = "Start Sequential Run";
            tab2TextBoxNumber.Text = "Number";
            tab2ButtonStartSingleRun.Content = "Start Single Run";
            tab2LabelProgessBar.Content = "Progress";
            tab2ButtonPause.Content = "Pause";
            tab2ButtonStop.Content = "Stop";
            tab2ButtonResume.Content = "Resume";
            tab3ButtonFrench.Content = "French";
            tab3ButtonEnglish.Content = "English";
            tab3ButtonUserGuide.Content = "Open User Guide";
            tab3ButtonOpenConfig.Content = "Open Config";
            tab3ButtonOpenLogs.Content = "Open Logs";
            tab3ButtonConfigureBuisnessSoftware.Content = "Configure Buisness Software";
            tab3ButtonConfigureTypeFilesToEncrypt.Content = "Configure Type of Files to Encrypt";

            MessageBox.Show(lang.changeMessageBoxLang("english"));


        }
        private void tab3ButtonUserGuide_Click(object sender, RoutedEventArgs e)
        {
            OpenProcess.OpenProcessFunction("notepad.exe", @"..\..\..\Files\UserGuide.txt");
        }

        private void tab3ButtonOpenConfig_Click(object sender, RoutedEventArgs e)
        {
            OpenProcess.OpenProcessFunction("notepad.exe", @"..\..\..\Files\work.json");

        }

        private void tab3ButtonOpenLogs_Click(object sender, RoutedEventArgs e)
        {
            OpenProcess.OpenProcessFunction("notepad.exe", @"..\..\..\Files\log.json");

        }



        //button close
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TargetPATH_Clicked(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog openDlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                tab1TextBoxTargetPath.Text = openDlg.SelectedPath;
                // TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
            else
            {
                MessageBox.Show(lang.printEmptyTargetDir);
            }

        }
        private void SourcePath_Clicked(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog openDlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                tab1TextBoxSourcePath.Text = openDlg.SelectedPath;
                // TextBlock1.Text = System.IO.File.ReadAllText(openFileDlg.FileName);
            }
            else
            {

                MessageBox.Show(lang.printEmptySourceDir);
            }

        }

        private void tab1SaveWork_Loaded(object sender, RoutedEventArgs e)
        {
            ProjetV3.EasySave DisplayWorks = new ProjetV3.EasySave();
            tab1DataGrid.ItemsSource = DisplayWorks.displayWorks();
        }



        private void tab1DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void tab2DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            ProjetV3.EasySave DisplayWorks = new ProjetV3.EasySave();
            tab2DataGrid.ItemsSource = DisplayWorks.displayWorks();
        }

        private void tab2DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void tab3ButtonConfigureBuisnessSoftware_Click(object sender, RoutedEventArgs e)
        {
            //here you just have to change log.json with the file you want tu put buisness software
            // OpenProcess.OpenProcessFunction("notepad.exe", @"..\..\..\Files\log.json");
            // Create OpenFileDialog
            Ookii.Dialogs.Wpf.VistaFolderBrowserDialog openDlg = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();

            // Launch OpenFileDialog by calling ShowDialog method

            Nullable<bool> result = openDlg.ShowDialog();

            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            // Load content of file in a TextBlock


        }

        private void tab3ButtonConfigureTypeFilesToEncrypt_Click(object sender, RoutedEventArgs e)
        {
            OpenProcess.OpenProcessFunction("notepad.exe", @"..\..\..\Files\extensions.json");

            //here you just have to change log.json with the file you want tu put the extensions for the files to encrypt


        }
    }
}
