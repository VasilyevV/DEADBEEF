using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;
using DEADBEEF;

class Master
{
    static void Main()
    {
        //создание БД
        string sqlExpression = @"CREATE TABLE Hashs 
                                (_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
                                 Hash TEXT NOT NULL, 
                                 FileName TEXT NOT NULL)";
        var connection = new SqliteConnection("Data Source=Databank.db");
        connection.Open();
        var command = new SqliteCommand(sqlExpression, connection);
        command.ExecuteNonQuery();

        string path = @"D:\Temp";//путь к главной папке
        string[] FileList = Directory.GetFiles(path);//список файлов в папке

        var Data = new DataBank();


        foreach (string str in FileList)
        {
            string thisFileMD5 = GetHash.Hash(str);
            string shortFileName = str.Substring(str.LastIndexOf('\\') + 1);
            
            Data.Set(shortFileName, thisFileMD5);
            Console.WriteLine("md5: " + thisFileMD5);
        }



        Console.ReadLine();
    }
}