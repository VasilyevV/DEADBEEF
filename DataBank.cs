using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace DEADBEEF
{
    internal class DataBank
    {
        internal void Set(string FileName, string md5Hash)
        {
            var connection = new SqliteConnection("Data Source=Databank.db");
            connection.Open();

            var command = new SqliteCommand();
            command.Connection = connection;
            command.CommandText = @"INSERT INTO Hashs (Hash, FileName) VALUES (@Hash, @FileName)";
            command.Parameters.Add(new SqliteParameter("@Hash", md5Hash));
            command.Parameters.Add(new SqliteParameter("@FileName", FileName));

            command.ExecuteNonQuery();

            Console.WriteLine("Hash добавлен");
        }

        internal bool Get (string md5Hash)
        {
            var HashList = new List<string>();
            int count = 0;
            
            string sqlExpression = "SELECT * FROM Hashs";
            var connection = new SqliteConnection("Data Source=Databank.db");
            connection.Open();
            var command = new SqliteCommand(sqlExpression, connection);
            SqliteDataReader reader = command.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read())
                {
                    var hash = reader["Hash"].ToString();
                    HashList.Add(hash);
                }
                
                foreach(var str in HashList)
                    if(str == md5Hash)
                        count++;

                if(count>0)        
                    return true;
                return false;
            }
            else
                return false;
        }
    }
}
