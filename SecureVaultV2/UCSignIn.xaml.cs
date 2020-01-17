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
    /// Interaction logic for UCSignIn.xaml
    /// </summary>
    public partial class UCSignIn : UserControl
    {
        public UCSignIn()
        {
            InitializeComponent();
        }

        private void SignIn_Login_but_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(VaultIdNametxt.Text))
            {
                HandyControl.Controls.MessageBox.Error
                   ("Error in Login Field, Please Check it again.", "Sign In Error");
                
            }

            else if (string.IsNullOrWhiteSpace(LoginPasstxt.Password.ToString()))
            {
                HandyControl.Controls.MessageBox.Error("Error in Password Field, Please Check it again.", "Sign In Error");
            }

            else
            {
                //String query = " Record(Id int,EmailID varchar(40) primary key,Password varchar(40) not null );";
                string query = "select Pass from Register where nameId='" + VaultIdNametxt.Text + "';";

                System.Data.SQLite.SQLiteConnection sqlcon = new System.Data.SQLite.SQLiteConnection(Tools.getDbcon());
                sqlcon.Open();
                System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);

                try
                {
                    com.ExecuteNonQuery();
                    System.Data.SQLite.SQLiteDataReader dr = com.ExecuteReader();
                    dr.Read();

                    //MessageBox.Show(dr["Password"].ToString());

                    if (LoginPasstxt.Password.ToString() == dr["Pass"].ToString())
                    {
                        HandyControl.Controls.MessageBox.Success
                        ("Welcome, You have logged In ", "Happy to see you again");
                        // MessageBox.Show(getInfo.getLog().ToString());
                        Tools.setLog(true);
                        //MessageBox.Show(getInfo.getLog().ToString());
                    }
                    else
                    {
                        HandyControl.Controls.MessageBox.Error
                        (" Error in login, Is your Id and Password Correct? ", " Login Error");
                    }

                }
                catch (Exception error)
                {
                    HandyControl.Controls.MessageBox.Error
                    (" Error in login, Is your Id and Password Correct? ", " Login Error");
                    HandyControl.Controls.MessageBox.Show(error.ToString());
                }

                sqlcon.Close();


            }
        }
    }
}
