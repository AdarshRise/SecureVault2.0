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
using System.Windows.Media.Animation;
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


                using (System.Data.SQLite.SQLiteConnection sqlcon = new System.Data.SQLite.SQLiteConnection(Tools.getDbcon()))
                {


                    sqlcon.Open();

                    using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon))
                    {



                        try
                        {
                            com.ExecuteNonQuery();


                            using (System.Data.SQLite.SQLiteDataReader dr = com.ExecuteReader())
                            {


                                dr.Read();

                                //MessageBox.Show(dr["Password"].ToString());

                                if (LoginPasstxt.Password.ToString() == dr["Pass"].ToString())
                                {
                                    HandyControl.Controls.MessageBox.Success
                                    ("Welcome, You have logged In ", "Happy to see you again");
                                    // MessageBox.Show(getInfo.getLog().ToString());
                                    Tools.setLog(true,true);

                                    Vault vault = new Vault();
                                    var WelcomeWindow = Window.GetWindow(this);        // Getting Parent Window control                           
                                    WelcomeWindow.Hide();                                   
                                    //vault.ShowDialog();

                                    vault.Opacity = 0;
                                    vault.ShowDialog();



                                    DoubleAnimation da = new DoubleAnimation();
                                    da.From = 1;
                                    da.To = 0;
                                    da.Duration = new Duration(TimeSpan.FromSeconds(2));
                                    da.AutoReverse = true;
                                    da.RepeatBehavior = RepeatBehavior.Forever;
                                    // da.RepeatBehavior=new RepeatBehavior(3);
                                    vault.BeginAnimation(OpacityProperty, da);
                                    //MessageBox.Show(getInfo.getLog().ToString());




                                    Tools.setLog(false,true);                // changing  the login side
                                    WelcomeWindow.Show();

                               
                                }
                                else
                                {
                                    HandyControl.Controls.MessageBox.Error
                                    (" Error in login, Is your Id and Password Correct? ", " Login Error");
                                }

                                dr.Close();
                            }

                            com.Dispose();
                        }
                        catch (Exception error)
                        {
                            HandyControl.Controls.MessageBox.Error
                            (" Error in login, Is your Id and Password Correct? ", " Login Error");
                            HandyControl.Controls.MessageBox.Show(error.ToString());
                        }

                    }
                    sqlcon.Close();

                }
            }
        }
    }
}
