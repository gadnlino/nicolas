using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Aula3108.Models
{
    public static class DbConnectionString
    {
        private static string connectionStringBase = 
            @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=[DB_FILE_LOCATION];Integrated Security=True;Connect Timeout=30";

        private static string dbFileName =  "\\App_Data\\Database1.mdf";

        public static string GetDbConnectionString()
        {            
            string curdir = System.AppDomain.CurrentDomain.BaseDirectory;

            if (curdir.EndsWith("\\"))
            {
                curdir = curdir.Substring(0, curdir.Length - 1);
            }

            string dbFileFullPath = $"{curdir}{dbFileName}";

            return connectionStringBase.Replace("[DB_FILE_LOCATION]", dbFileFullPath);
        }
    }
}