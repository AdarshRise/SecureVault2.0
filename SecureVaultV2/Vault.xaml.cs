using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace SecureVaultV2
{
    /// <summary>
    /// Interaction logic for Vault.xaml
    /// </summary>
    public partial class Vault : Window
    {
        static UserControl usc1 = new UCTWB();
        static UserControl UCVaultIn = new UCVaultIn();
        static UserControl UCVaultOut = new UCVaultOut();
        static UserControl UCSelfMessage = new UCSelfMessage();
        static UserControl UCShareMessage = new UCShareMessage();

        public Vault()
        {
            InitializeComponent();
            //VaultDisplayWindow.Children.Clear();
            ShowMe();
           // Thread.Sleep(2000);
            VaultDisplayWindow.Children.Add(usc1);
            this.Closed += new EventHandler(MainWindow_Closed);

        }
        public void ShowMe()
        {
            (FindResource("showMe") as Storyboard).Begin(this);
        }

        void MainWindow_Closed(object sender, EventArgs e)
        {
            VaultDisplayWindow.Children.Clear();
           // VaultDisplayWindow.Children.Remove(usc1);
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void SideMenuItem_VaultIn(object sender, MouseButtonEventArgs e)
        {
            //HandyControl.Controls.MessageBox.Warning("Error in Vault Id Field, Please Check it again.", "Registration Error");
            VaultDisplayWindow.Children.Clear();
            VaultDisplayWindow.Children.Add(UCVaultIn);
        }

        private void SideMenuItem_VaultOut(object sender, MouseButtonEventArgs e)
        {
            VaultDisplayWindow.Children.Clear();
            VaultDisplayWindow.Children.Add(UCVaultOut);
        }

        private void SideMenuItem_SelfMessage(object sender, MouseButtonEventArgs e)
        {
            VaultDisplayWindow.Children.Clear();
            VaultDisplayWindow.Children.Add(UCSelfMessage);
        }

        private void SideMenuItem_ShareMessage(object sender, MouseButtonEventArgs e)
        {
            VaultDisplayWindow.Children.Clear();
            VaultDisplayWindow.Children.Add(UCShareMessage);
        }
    }
}
