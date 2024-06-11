using UnityEngine;
using System.IO;
using System.Data;
using Mono.Data.Sqlite;
using Unity.VisualScripting;

public class InventorySave
{
    public int cellNumber;
    public int itemNumber;
}
public class SavedData
{
    public int episodeNumber;
}


public class DBM
{

    private static string connectionString = "URI=file:" + Application.persistentDataPath + "/db.bytes";
    private static SqliteConnection connection;
    private static SqliteCommand command;



     static void OpenConnection()
    {
        connection = new SqliteConnection(connectionString);
        connection.Open();
        command = connection.CreateCommand();
        command.CommandText = "CREATE TABLE IF NOT EXISTS SavedEpisode (id INTEGER PRIMARY KEY);";
        command.ExecuteNonQuery();
        command.CommandText = "CREATE TABLE IF NOT EXISTS Users (Name Text);";
        command.ExecuteNonQuery();

    }
     static void CloseConnection()
    {
        command.Dispose();
        connection.Close();
    }

    public static void SaveEpisode(int id)
    {
        OpenConnection();
        command.CommandText = $"INSERT OR REPLACE INTO SavedEpisode (id) VALUES ('{id}');";
        command.ExecuteNonQuery();
        CloseConnection();
    }
    public static string GetLastUser()
    {
        OpenConnection();
        command.CommandText = $"SELECT Name FROM Users ORDER BY rowid DESC LIMIT 1;";
        var ans = command.ExecuteScalar();
        CloseConnection();
        if (ans != null) return ans.ToString();
        else return null;
       
    }
    public static void SaveUser(string name)
    {
        OpenConnection();
        command.CommandText = $"INSERT OR REPLACE INTO Users (Name) VALUES ('{name}');";
        command.ExecuteNonQuery();
        CloseConnection();
    }

    public static int LoadEpisode()
    {
        OpenConnection();
        command.CommandText = $"SELECT MAX(id) FROM SavedEpisode;";
        string ans = command.ExecuteScalar().ToString();
        CloseConnection();
        ans.NullIfEmpty();
        if (ans != null ) return int.Parse(ans);
        else return 0;
        
    }


    public static  void DropSaves()
    {
        OpenConnection();
        command.CommandText = $"DROP  TABLE Users;";
        command.ExecuteNonQuery();
        command.CommandText = $"DROP  TABLE SavedEpisode;";
        command.ExecuteNonQuery();
        command.CommandText = $"DROP  TABLE Inventory;";
        command.ExecuteNonQuery();
        CloseConnection();
    }
}

