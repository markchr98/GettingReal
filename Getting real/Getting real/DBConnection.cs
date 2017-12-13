using MySql.Data;
using MySql.Data.MySqlClient;
using System;

namespace Getting_real
{
    public class DBConnection
    {
        //Need values 
        private static string Server = "10.140.65.233";
        private string port = "3306";
        private static string Username = "root";
        private static string Password = "your_password";
        private static string Database = "mcm_database";
        
        private DBConnection()
        {
        }
        private static string connstring = string.Format("Server=" + Server + "; database=" + Database + "; UID=" + Username + "; password=" + Password);
        private MySqlConnection connection = new MySqlConnection(connstring);
        public MySqlConnection Connection
        {
            get { return connection; }
        }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }
       
        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}