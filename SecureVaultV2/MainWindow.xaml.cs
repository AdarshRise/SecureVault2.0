using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
using HandyControl.Tools.Extension;
using HandyControl.Expression;
using HandyControl.Media.Animation;
using HandyControl.Properties.Langs;
using HandyControl.Interactivity;
//using MessageBox = HandyControl.Controls.MessageBox;
using MessageBoxResult = System.Windows.MessageBoxResult;

namespace SecureVaultV2
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_But_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            

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
 
        }
    }
}
