using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ServerBelPodryad.Services
{
    public static class Database
    {
        private static readonly MySqlConnection connection = new MySqlConnection($"" +
            $"server=localhost; " +
            $"username=admin; " +
            $"password=12345; " +
            $"database=building");
        public static bool OpenConnectionOrReturnNow()
        {
            try 
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Ошибка подключения к базе данных: " + ex.Message);
            }
        }

        public static void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }

        public static MySqlConnection GetConnection()
        {
            return connection;
        }
    }
}
