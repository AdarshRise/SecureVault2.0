using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Win32;

namespace SecureVaultV2
{
    class Tools
    {

        private static String databasePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinFile.db");
        private static readonly String dbcon = @"data Source=" + databasePath + ";Version=3;";
        private static bool login;
        private static byte[] key = new byte[32];
        private static String SelfMessage = null;
        private static String ShareMessage = null;
        private static String EnSelfMessage = null;
        private static String DeSelfMessage = null;
        private static String EnShareMessage = null;
        private static String DeShareMessage = null;
        private static String CurrentPassword = null;

        public static bool getLog()
        {
            return login;
        }
        public static void setLog(bool value, bool inout)
        {
            if (value)
                login = true;
            else if (!inout)
            {
                HandyControl.Controls.Growl.ErrorGlobal("Login Problem, Restart The Software"); // outside window
            }

            if (!value)
                login = false;

        }

        public static string getDbcon()
        {
            return dbcon;
        }
        public static string getDbPath()
        {
            return databasePath;
        }

        public static void putCurrentPassword(string Pass)
        {
            CurrentPassword = Pass;
        }

        private static string getCurrentPassword()
        {
            return CurrentPassword;
        }


        // Text related Funtions


        static byte[] Encrypt(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;
            // Create a new AesManaged.    
            using (AesManaged aes = new AesManaged())
            {
                // Create encryptor    
                ICryptoTransform encryptor = aes.CreateEncryptor(Key, IV);
                // Create MemoryStream    
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create crypto stream using the CryptoStream class. This class is the key to encryption    
                    // and encrypts and decrypts data from any given stream. In this case, we will pass a memory stream    
                    // to encrypt    
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // Create StreamWriter and write data to a stream    
                        using (StreamWriter sw = new StreamWriter(cs))
                            sw.Write(plainText);
                        encrypted = ms.ToArray();
                    }
                }
            }
            // Return encrypted data    
            return encrypted;
        }


        //  Decrypt Message


        static string Decrypt(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null;
            // Create AesManaged    
            using (AesManaged aes = new AesManaged())
            { 
                // Create a decryptor    
                ICryptoTransform decryptor = aes.CreateDecryptor(Key, IV);
                // Create the streams used for decryption.    
                using (MemoryStream ms = new MemoryStream(cipherText))
                {
                    // Create crypto stream    
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        // Read crypto stream    
                        using (StreamReader reader = new StreamReader(cs))
                            plaintext = reader.ReadToEnd();
                    }
                }
            }
            return plaintext;
        }




        public static void NumCreator()
        {
            
            string str = getCurrentPassword();
            if (str != null)
            {


                short left = (short)(32 - str.Length);  // Review this part

                while (str.Length < 32)
                {
                    str += str;
                }
                char[] newstr = str.ToCharArray();
                // byte[] key = new byte[32];
                for (int i = 0; i < 32; i++)
                {
                    key[i] = (byte)(((int)newstr[i]) % 255);

                    // Console.Write(key[i] + " ");
                }
                // Console.WriteLine();

                // return key;
              //  HandyControl.Controls.MessageBox.Error(key[0].ToString(), "The Key Genearated5");
            }
            else
            {
                HandyControl.Controls.MessageBox.Error("Password Can't Be Empty", "PassKey Error");
            }

        }

        private static byte [] GetNumCreator()
        {
            return key;
        }



        public static void PutSelfMessage(string data)
        {
            SelfMessage = data;
        }

        private static string GetSelfMessage()
        {
            return SelfMessage;
        }


        public static string GetEnSelfMessage()
        {
            return EnSelfMessage;
        }

        public static string GetDeSelfMessageVal()
        {
            return DeSelfMessage;
        }

        public void FreeShareMessage()
        {
            EnSelfMessage = null;
            GC.Collect();
        }

        public void FreeSelfMessage()
        {
            SelfMessage = null;
            GC.Collect();
        }

        public void PutShareMessage(string data)
        {
            ShareMessage = data;
        }

        public string GetShareMessage()
        {
            return ShareMessage;
        }

        public string GetEnShareMessage()
        {
            return EnShareMessage;
        }




        public static string SaveFileDialog()
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = "c:\\";
            //dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
         //   dlg.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png|All Files (*.*)|*.*";
            dlg.Filter= "All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string selectedFileName = dlg.FileName;
                //FileNameLabel.Content = selectedFileName; // to display it 
                //MessageBox.Show(selectedFileName);
                System.IO.File.AppendAllText(selectedFileName, GetEnSelfMessage());
                return selectedFileName;
            }
            return "";

         
        }


        private static byte[] ReadFileDialog() /// Modular later
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            //var newDestination = Environment.CurrentDirectory;
            dialog.InitialDirectory = "c:\\";
            dialog.Filter = "All Files (*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                var fullPath = dialog.FileName;
                var fileOnlyName = Path.GetFileName(fullPath);

               // return fullPath;

                TextReader rdr = File.OpenText(fullPath);
                string line = rdr.ReadLine();
                rdr.Close();
                HandyControl.Controls.MessageBox.Error(line, "get back data Error");

                string num = "";
                List<byte> bas = new List<byte>();


                foreach (char s in line)
                {
                    if (!Char.IsWhiteSpace(s))
                    {
                        num = num + s;
                    }
                    else
                    {
                        //Console.Write(num + " ");
                        // newdata[sizefinder] = Byte.Parse(num);
                        //Console.ReadKey();
                        bas.Add(Byte.Parse(num));
                        num = "";
                    }
                }
                byte[] encdata = bas.ToArray();




                string testing = "";
                foreach (byte x in encdata) // getting null bug
                {
                    testing = testing + x + " ";
                }

                HandyControl.Controls.MessageBox.Error(testing, "Not Null before passing data to decrypt");





                return encdata;
                // File.Copy(fullPath, Path.Combine(newDestination, fileOnlyName));
            }
            else
            {
                return null;
            }
        }

            public static void PutEnSelfMessage()
        {
           
            try
            {
                byte[] encrypted;
                using (AesManaged aes = new AesManaged())
                {
                    if (GetSelfMessage() != null)
                    {
                        encrypted = Encrypt(GetSelfMessage(), GetNumCreator(), aes.IV); // aes.key max is 255 it seems
                        HandyControl.Controls.MessageBox.Error(GetNumCreator().ToString(), "PassKey Error");
                    } 
                    else
                    {
                        encrypted = null;
                    }
                }
               // EnSelfMessage = encrypted.ToString();
               
                StringBuilder EnSelfMessageBuild = new StringBuilder();

                foreach (byte var in encrypted)
                {
                    //like += var;
                    EnSelfMessageBuild.Append(var);
                    EnSelfMessageBuild.Append(" ");

                }
                EnSelfMessage = EnSelfMessageBuild.ToString();
                EnSelfMessageBuild.Clear();
                GC.Collect();
            }

            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
                EnSelfMessage = null;
            }

            if(EnSelfMessage!= null)
            {

            }

        }


        private void PutEnShareMessage()
        {
            byte[] encrypted;
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    if (GetShareMessage() != null)
                    {


                        encrypted = Encrypt(GetShareMessage(), GetNumCreator(), aes.IV); // aes.key max is 255 it seems
                    }
                    else
                    {
                        encrypted = null;
                    }
                }
                EnShareMessage = encrypted.ToString();
            }

            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
                EnShareMessage = null;
            }
        }





        // Left is Decrypt Part


        



        public static void GetDeSelfMessage()
        {
            byte[] encrypted=ReadFileDialog();
            string Data;

            string testing="";
            foreach (byte x in encrypted) // getting null bug
            {
                testing =  testing + x +" ";
                

            }


            HandyControl.Controls.MessageBox.Error(testing, "Not Null before descrupt");

            HandyControl.Controls.MessageBox.Error(encrypted.Length.ToString(), "It's Length");
            HandyControl.Controls.MessageBox.Error(sizeof(byte).ToString(), "It's Size");
            
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    
                    if (encrypted != null)
                    {


                        Data = Decrypt(encrypted, GetNumCreator(), aes.IV); // aes.key max is 255 it seems
                    }
                    else
                    {
                        Data = null;
                    }
                }

                HandyControl.Controls.MessageBox.Error(Data, "after descrupt");
                DeSelfMessage = Data;
                
            }

            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
                HandyControl.Controls.MessageBox.Error(exp.ToString(), "Something Went Wrong While Decrypt");
                DeSelfMessage = null;
            }
        }


            













        // Self Made Hash Function...

        /*

        static int hash(string str)
        {
            int result = 0;
            char[] newstr = str.ToCharArray();
            foreach (char x in newstr)
            {
                result = result + (int)x;
            }
            result = result % 25;
            return result;
        }
        */



        // This is for Future File Reading Feature.

        /*

                public static void Main(String[] args)
                {
                    Console.OutputEncoding = System.Text.Encoding.UTF8;
                    Console.WriteLine("Enter text that needs to be encrypted..");

                    FileInfo fileInfo = new FileInfo("exp.txt");
                    int filesize = (int)(fileInfo.Length) / 1048576;



                    Console.WriteLine(fileInfo.Length / 1048576);
                    fileInfo = null;
                    GC.Collect();
                    if (filesize < 100)
                    {
                        string[] lines = System.IO.File.ReadAllLines("exp.txt");

                        // Console.WriteLine("the size above");
                        StringBuilder newData = new StringBuilder();
                        foreach (string x in lines)
                        {
                            // Console.Write(x);
                            newData.Append("\n");
                            newData.Append(x);

                        }
                        //Console.Write(newData);
                        //string image = lines[0];
                        //string data = Console.ReadLine();
                        lines = null;
                        GC.Collect();
                        String newDataS = newData.ToString();
                        newData.Clear();

                        // newData = new StringBuilder();
                        newData = null;
                        GC.Collect();
                        EncryptAesManaged(ref newDataS);
                        newData = null;
                        GC.Collect();
                    }
                    else
                    {
                        Console.WriteLine(" Data exeeds file limit");
                    }
                    Console.ReadLine();
                }


                    */


            


        // Encrypt  Message







    }




}
