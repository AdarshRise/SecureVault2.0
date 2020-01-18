using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureVaultV2
{
    class Tools
    {

        private static String databasePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WinFile.db");
        private static String dbcon = @"data Source=" + databasePath + ";Version=3;";
        private static bool login;

        public static bool getLog()
        {
            return login;
        }
        public static void setLog(bool value)
        {
            if (value)
                login = true;
            else
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

    }




}
