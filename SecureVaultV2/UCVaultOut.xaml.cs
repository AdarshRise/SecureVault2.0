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
    /// Interaction logic for UCVaultOut.xaml
    /// </summary>
    public partial class UCVaultOut : UserControl
    {
        public UCVaultOut()
        {
            InitializeComponent();
        }

        private void ReadPass_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(Email_box.Text))
            {
                HandyControl.Controls.MessageBox.Show("Error in E-Mail Field, Please Check it again.", "Read Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                // MessageBox.Show("Thanks, This Part is UnderDevelopment, Please Wait for The next Update", "Work In Progress", MessageBoxButton.OK, MessageBoxImage.Information);

                if (!string.IsNullOrWhiteSpace(Email_box.Text))
                {
                    using (System.Data.SQLite.SQLiteConnection sqlcon = new System.Data.SQLite.SQLiteConnection(Tools.getDbcon()))
                    {

                        sqlcon.Open();
                        //MessageBox.Show(sqlcon.ToString());
                        string query = "Select password from Record where EmailID='" + Email_box.Text + "';";
                       // System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);

                        using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon))
                        {
                            com.ExecuteNonQuery();
                            using (System.Data.SQLite.SQLiteDataReader dr = com.ExecuteReader())
                            {
                               // System.Data.SQLite.SQLiteDataReader dr = com.ExecuteReader();
                                dr.Read();

                                Pass_box.Text = dr["Password"].ToString();
                            }
                        }
                        //MessageBox.Show("Congrats , You have successefully Loaded the Data ", "Data Loaded  ", MessageBoxButton.OK, MessageBoxImage.Information);
                        sqlcon.Close();
                    }
                }

            }



        }
    }
}
