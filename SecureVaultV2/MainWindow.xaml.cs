using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
using HandyControl.Tools.Extension;
using HandyControl.Expression;
using HandyControl.Media.Animation;
using HandyControl.Properties.Langs;
using HandyControl.Interactivity;
using System;

//using MessageBox = HandyControl.Controls.MessageBox;
using MessageBoxResult = System.Windows.MessageBoxResult;
using System.Windows.Media.Animation;

namespace SecureVaultV2
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowMe();
            
        }
        public void ShowMe()
        {
            (FindResource("showMe") as Storyboard).Begin(this);
        }


        private void Close_But_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var val = MessageBox.Ask("Shutting down in 3 2 1....", "End");

            //System.Windows.MessageBoxResult.OK;

           // MessageBox.Show(val.ToString(), "The Val");
           // MessageBox.Show(val.GetType().ToString(), "The Val");
            if (val == MessageBoxResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
            }
          
            
            GC.Collect();
            
            
            
            /*
            MessageBox.Show("This is great","Show");
            var val = MessageBox.Ask("This is great", "Ask");

            //System.Windows.MessageBoxResult.OK;

            MessageBox.Show(val.ToString(), "The Val");
            MessageBox.Show(val.GetType().ToString(), "The Val");
            if(val==MessageBoxResult.OK)
            {
                MessageBox.Show("It Was OK");
            }
            if (val==MessageBoxResult.Cancel)
            {
                MessageBox.Show("It Was Cancel");
            }
            MessageBox.Error("This is great", "Error");
            MessageBox.Info("This is great", "Info");
            MessageBox.Fatal("This is great", "Fatal");
            MessageBox.Warning("This is great", "Warning");
            MessageBox.Success("This is great", "Success");
            */
            
        }

        private void Min_But_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // System.Windows.Application.Current.;
            WindowState = System.Windows.WindowState.Minimized;
        }

        private void SwitchTest_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Vault vault = new Vault();
            this.Hide();
            vault.ShowDialog();
            this.Show();
          
            GC.Collect();
        }
    }
}
