using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools;
using HandyControl.Tools.Extension;
using HandyControl.Expression;
using HandyControl.Media.Animation;
using HandyControl.Properties.Langs;
using HandyControl.Interactivity;
using MessageBox = HandyControl.Controls.MessageBox;

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
            MessageBox.Ask("This is great");
            MessageBox.Show(new MessageBoxInfo
            {
                MessageBoxText = "Ask",
                Caption = "Title",
                Button = MessageBoxButton.YesNo, 
                IconBrushKey = ResourceToken.AccentBrush,
                IconKey = ResourceToken.AskGeometry,
                Style = ResourceHelper.GetResource<Style>("MessageBoxCustom")
            });
        }
    }
}
