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

     
    }
}
