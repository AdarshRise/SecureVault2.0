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
        private static String RevertPass = null;

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

        public static void resetCurrentPassword()
        {
            CurrentPassword = RevertPassword();
        }
        private static string RevertPassword()
        {
            return RevertPass;
        }

        public static void SetRevertPassword(string str)
        {
            RevertPass = str;
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

        public static void PutShareMessage(string data)
        {
            ShareMessage = data;
            HandyControl.Controls.MessageBox.Success(ShareMessage, " Share Message In memory");
        }

        public static string GetShareMessage()
        {
            return ShareMessage;
        }

        public static string GetEnShareMessage()
        {
            return EnShareMessage;
        }




        public static string SaveFileDialogSelf()
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

        public static string SaveFileDialogShare()
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = "c:\\";
            //dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            //   dlg.Filter = "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png|All Files (*.*)|*.*";
            dlg.Filter = "All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            bool? result = dlg.ShowDialog();
            if (result == true)
            {
                string selectedFileName = dlg.FileName;
                //FileNameLabel.Content = selectedFileName; // to display it 
                //MessageBox.Show(selectedFileName);
                System.IO.File.AppendAllText(selectedFileName, GetEnShareMessage());
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
                HandyControl.Controls.MessageBox.Success(line, "get back data");

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

                HandyControl.Controls.MessageBox.Success(testing, "Not Null before passing data to decrypt");





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
                        HandyControl.Controls.MessageBox.Success(GetNumCreator().ToString(), "PassKey");
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
                HandyControl.Controls.MessageBox.Success(EnSelfMessage, "Encrypted Share Message");
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


        public static void PutEnShareMessage()
        {

            StringBuilder EnKeyTester = new StringBuilder();

            foreach (byte var in GetNumCreator())
            {
                //like += var;
                EnKeyTester.Append(var);
                EnKeyTester.Append(" ");

            }
          

            HandyControl.Controls.MessageBox.Success(EnKeyTester.ToString(), "This is the key used");

            
                HandyControl.Controls.MessageBox.Success(GetShareMessage(), "This is the Share Message used");


            try
            {
                byte[] encrypted;
                using (AesManaged aes = new AesManaged())
                {
                    if (GetShareMessage() != null)
                    {
                        encrypted = Encrypt(GetShareMessage(), GetNumCreator(), aes.IV); // aes.key max is 255 it seems
                        HandyControl.Controls.MessageBox.Success(GetNumCreator().ToString(), "PassKey ");
                    }
                    else
                    {
                        encrypted = null;
                    }
                }
                // EnShareMessage = encrypted.ToString();
                if (encrypted != null)
                    HandyControl.Controls.MessageBox.Success("The Encrypted byte array is not null", "Not Null after Encryptd");
                else
                    HandyControl.Controls.MessageBox.Error("The Encrypted byte array is  null", " Null after Encryptd");
                StringBuilder EnShareMessageBuild = new StringBuilder();

                foreach (byte var in encrypted)
                {
                    //like += var;
                    EnShareMessageBuild.Append(var);
                    EnShareMessageBuild.Append(" ");

                }
                EnShareMessage = EnShareMessageBuild.ToString();
                EnShareMessageBuild.Clear();
                GC.Collect();
            }

            catch (Exception exp)
            {
                //Console.WriteLine(exp.Message);
                EnShareMessage = null;
            }

            if (EnShareMessage != null)
            {

            }
        }





        // Left is Decrypt Part


        



        public static bool GetDeSelfMessage()
        {
            byte[] encrypted=ReadFileDialog();
            if (encrypted != null)
            {


                string Data;

                string testing = "";
                foreach (byte x in encrypted) // getting null bug
                {
                    testing = testing + x + " ";


                }
                if (encrypted == null)
                {
                    return false;
                }

                HandyControl.Controls.MessageBox.Success(testing, "Not Null before descrupt");

                HandyControl.Controls.MessageBox.Success(encrypted.Length.ToString(), "It's Length");
                HandyControl.Controls.MessageBox.Success(sizeof(byte).ToString(), "It's Size");

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

                  //  HandyControl.Controls.MessageBox.Error(Data, "after descrupt");
                    DeSelfMessage = Data.Substring(16);
                    return true;

                }

                catch (Exception exp)
                {
                    //Console.WriteLine(exp.Message);
                    HandyControl.Controls.MessageBox.Error(exp.ToString(), "Something Went Wrong While Decrypt");
                    DeSelfMessage = null;
                    return false;
                }
            }
            return false;
        }



        public static bool GetDeShareMessage()
        {
            byte[] encrypted = ReadFileDialog();
            if (encrypted != null)
            {


                string Data;

                string testing = "";
                foreach (byte x in encrypted) // getting null bug
                {
                    testing = testing + x + " ";


                }
                if (encrypted == null)
                {
                    return false;
                }

                HandyControl.Controls.MessageBox.Success(testing, "Not Null before descrupt");

                HandyControl.Controls.MessageBox.Success(encrypted.Length.ToString(), "It's Length");
                HandyControl.Controls.MessageBox.Success(sizeof(byte).ToString(), "It's Size");

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

                   // HandyControl.Controls.MessageBox.Error(Data, "after descrupt");
                    DeShareMessage = Data.Substring(16);
                    return true;

                }

                catch (Exception exp)
                {
                    //Console.WriteLine(exp.Message);
                    HandyControl.Controls.MessageBox.Error(exp.ToString(), "Something Went Wrong While Decrypt");
                    DeShareMessage = null;
                    return false;
                }
            }
            return false;
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
