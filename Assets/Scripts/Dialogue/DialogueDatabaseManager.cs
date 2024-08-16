using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine.UIElements;
using System.IO;

public class DialogueDatabaseManager : MonoBehaviour
{
    public IDbConnection OpenDialogueDb()
    {
        return OpenDb("Dialogue", new string[] {"english TEXT"});
    }
    public IDbConnection OpenActorDb(){
        return OpenDb("Actor", new string[] {"actor TEXT"});
    }
    private IDbConnection OpenDb(string databaseName, string[] columns){
        string database = "URI=file:" + Path.Combine(Application.streamingAssetsPath, databaseName + ".sqlite");
        IDbConnection connection = new SqliteConnection(database);
        connection.Open();
        IDbCommand createTable = connection.CreateCommand();

        string columnText = "";
        foreach(string column in columns){
            columnText += column + ", ";
        }
        columnText = columnText[..^2];
        createTable.CommandText = $"CREATE TABLE IF NOT EXISTS {databaseName} (id INTEGER PRIMARY KEY, {columnText})";
        createTable.ExecuteReader();

        return connection;
    }
}
