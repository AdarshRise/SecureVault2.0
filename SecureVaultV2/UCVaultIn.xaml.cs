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

namespace SecureVaultV2
{
    /// <summary>
    /// Interaction logic for UCVaultIn.xaml
    /// </summary>
    public partial class UCVaultIn : UserControl
    {
        public UCVaultIn()
        {
            InitializeComponent();
        }

        private void RegisterPass_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(Email_box.Text))
            {
                HandyControl.Controls.MessageBox.Show("Error in E-Mail Field, Please Check it again.", "Register Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            if (string.IsNullOrWhiteSpace(Email_box.Text))
            {
                HandyControl.Controls.MessageBox.Show("Error in Password Field, Please Check it again.", "Register Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }


            using (System.Data.SQLite.SQLiteConnection sqlcon = new System.Data.SQLite.SQLiteConnection(Tools.getDbcon()))
            {



                if (!string.IsNullOrWhiteSpace(Pass_box.Text) && !string.IsNullOrWhiteSpace(Email_box.Text))
                {
                    sqlcon.Open();
                    //MessageBox.Show(sqlcon.ToString());
                    string query = "insert into Record values(null,'" + Email_box.Text + "','" + Pass_box.Text + "');";
                    //System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);

                    using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon))
                    {
                        com.ExecuteNonQuery();
                    }
                    HandyControl.Controls.MessageBox.Show("Congrats , You have successefully Loaded the Data ", "Data Loaded  ", MessageBoxButton.OK, MessageBoxImage.Information);
                    sqlcon.Close();
                }
            }

        }
    }
}
