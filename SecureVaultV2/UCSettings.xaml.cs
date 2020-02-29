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
using System;
using System.Threading;
using AutoUpdaterDotNET;

namespace SecureVaultV2
{
    /// <summary>
    /// Interaction logic for UCSettings.xaml
    /// </summary>
    public partial class UCSettings : UserControl
    {
        public UCSettings()
        {
            InitializeComponent();
        }
        private void AutoUpdater_ApplicationExitEvent()
        {
            //string Text = @"Closing application...";
            Thread.Sleep(5000);
            System.Windows.Application.Current.Shutdown();
        }


        private void but_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutoUpdater.DownloadPath = Environment.CurrentDirectory;
                AutoUpdater.Start("https://www.dropbox.com/s/7dl3swl17l2wt8o/update.xml?dl=1");
                AutoUpdater.ReportErrors = true;
                AutoUpdater.Mandatory = true;
                AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
            }
            catch (Exception tr)
            {
                MessageBox.Show("Error in update, send this screenshot to developer:-" + tr.ToString());
            }

            GC.Collect();
        }
    }
}
