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
 using System.Data.SQLite;
using System.IO;

/*
using HandyControl.Tools;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Expression;
using HandyControl.Interactivity;
using HandyControl.Media;
using HandyControl.Properties;
*/

namespace SecureVaultV2
{
    /// <summary>
    /// Interaction logic for UCSignUp.xaml
    /// </summary>
    public partial class UCSignUp : UserControl
    {
        public UCSignUp()
        {
            InitializeComponent();
        }

        private void but_Register_SignUp_Click(object sender, RoutedEventArgs e)
        {
            //HandyControl.Controls.Growl.Success("Hello");
             HandyControl.Controls.Growl.Info("Processing！", "Process");


            //SQLiteConnection.CreateFile("MyDatabase.sqlite");
            //SELECT name FROM sqlite_master WHERE type = 'table' AND name = '{table_name}';



            //  HandyControl.Controls.Growl.Warning(new HandyControl.Data.GrowlInfo { Message = "Check Update" });

            //HandyControl.Controls.Growl.SuccessGlobal(Tools.getDbPath()); // outside window
       

            if(!File.Exists(Tools.getDbPath()))
            {
                SQLiteConnection.CreateFile(Tools.getDbPath());
            }

           
            System.Data.SQLite.SQLiteConnection sqlcon = new System.Data.SQLite.SQLiteConnection(Tools.getDbcon());


            if (string.IsNullOrWhiteSpace(Idtxt.Text))
            {
                HandyControl.Controls.MessageBox.Warning("Error in Vault Id Field, Please Check it again.", "Registration Error");
            }
            else if (string.IsNullOrWhiteSpace(nametxt.Text))
            {
                HandyControl.Controls.MessageBox.Warning("Error in Name Field, Please Check it again.", "Registration Error");
            }
            else if (string.IsNullOrWhiteSpace(Passtxt.Password))
            {
                HandyControl.Controls.MessageBox.Warning("Error in Password Field, Please Check it again.", "Registration Error");
            }
            else if (string.IsNullOrWhiteSpace(RePasstxt.Password))
            {
                HandyControl.Controls.MessageBox.Warning("Error in Re-Pass Field, Please Check it again.", "Registration Error");

            }
            else if (!string.IsNullOrWhiteSpace(nametxt.Text) && !string.IsNullOrWhiteSpace(Idtxt.Text) && !string.IsNullOrWhiteSpace(Passtxt.Password) && !string.IsNullOrWhiteSpace(RePasstxt.Password) && RePasstxt.Password == Passtxt.Password)
            {
                sqlcon.Open();
                //MessageBox.Show(sqlcon.ToString());
                
                string query = "SELECT name FROM sqlite_master WHERE type='table' AND name='Register';";
                SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);
                SQLiteCommand com2;
                query = com.ExecuteReader().Read().ToString();
               // HandyControl.Controls.Growl.SuccessGlobal(query); // outside window
                if(query==false.ToString())
                {
                    //HandyControl.Controls.Growl.SuccessGlobal("It Works");
                           query= "create table Register (nameId varchar(40) primary key,name varchar(40),Pass varchar(40));";                                              //HandyControl.Controls.Growl.SuccessGlobal(com.ExecuteReader().ToString()); // outside window
                           com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);
                           com.ExecuteNonQuery();
                  
                   

                }
                
                com.Dispose();
                sqlcon.Close();
                sqlcon.Open();


                query = "select  nameId from Register where nameId='" + Idtxt.Text + "';";

                com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);

                try
                {
                    com.ExecuteNonQuery();
                    System.Data.SQLite.SQLiteDataReader dr1 = com.ExecuteReader();
                    dr1.Read();

                    //MessageBox.Show(dr["Password"].ToString());

                    if (Idtxt.Text == dr1["nameId"].ToString())
                    {
                       // MessageBox.Show("Welcome, You have logged In ", "Happy to see you again", MessageBoxButton.OK, MessageBoxImage.Information);
                        HandyControl.Controls.MessageBox.Info("Choose a new ID", "ID Taken");
                        // MessageBox.Show(getInfo.getLog().ToString());
                       // getInfo.setLog();
                        //MessageBox.Show(getInfo.getLog().ToString());
                    }
                    dr1.Close();
                 
                }
                catch (Exception error)
                {
                    //MessageBox.Show(" Error in login, Is your Id and Password Correct? ", " Login Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    // MessageBox.Show(error.ToString());

                    // HandyControl.Controls.MessageBox.Fatal("Line before Error", " In Catch ");
                    //HandyControl.Controls.MessageBox.Fatal(error.ToString() , " In Catch " );
                    com.Dispose();
                    
                    sqlcon.Close();
                    sqlcon.Open();

                 

                    
               query = "insert into Register values('" + nametxt.Text.ToString() + "','" + Idtxt.Text.ToString() + "','" + RePasstxt.Password.ToString() + "');";
               com2 = new System.Data.SQLite.SQLiteCommand(query, sqlcon);

                    try
                    {
                        com2.ExecuteNonQuery();
                        HandyControl.Controls.MessageBox.Success("This Can't be changed later", "Always Remember ");
                        HandyControl.Controls.MessageBox.Success("Welcome , You have been successefully registered ", "Registration Completed");
                    }

                    catch (Exception error2)
                    {
                        HandyControl.Controls.MessageBox.Info("Choose a new ID", "ID Taken");
                        HandyControl.Controls.MessageBox.Info(error2.ToString());
                    }

                    com2.Dispose();
                    sqlcon.Close();

                }


              

                /*




                //query = " select nameId from Register;";
                query = "select count(*) from Register ;";
                com = new System.Data.SQLite.SQLiteCommand(query, sqlcon);
                com.ExecuteNonQuery();
                System.Data.SQLite.SQLiteDataReader dr = com.ExecuteReader();
              

                HandyControl.Controls.Growl.SuccessGlobal("It Works "+ dr.Read().ToString()); // Returns True when count>0



                */



                sqlcon.Close();

            }
            else
            {
                HandyControl.Controls.MessageBox.Error("Pass Code are not same, Please Check it again.", "Registration Error");
            }
            // MessageBox.Show(RePasstxt.Password);// this works


        }
    }
}
