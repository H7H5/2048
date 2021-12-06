using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
public class UnitySQLite : MonoBehaviour
{
    public string Connection;
    public IDbConnection dbconn;
    public IDbCommand dbcmd;
    private IDataReader reader;
    private string DatabaseName = "mydb.s3db";
    private Save_string save_String = new Save_string();
    
    private void Awake()
    {
#if UNITY_EDITOR
        string filepath = Application.dataPath + "/Plugins/" + DatabaseName;
        Connection = "URI=file:" + filepath;
        dbconn = new SqliteConnection(Connection);
        dbconn.Open();
#elif UNITY_ANDROID
        ///*
        //Application database Path android
        string filepath = Application.persistentDataPath + "/" + DatabaseName;
        if (!File.Exists(filepath))
        {
            // If not found on android will create Tables and database
            Debug.LogWarning("File \"" + filepath + "\" does not exist. Attempting to create from \"" +
                             Application.dataPath + "!/assets/mydb");
            // UNITY_ANDROID
            WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/mydb.s3db");
            while (!loadDB.isDone) { }
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDB.bytes);
        }
        Connection = "URI=file:" + filepath;
        Debug.Log("Stablishing connection to: " + Connection);
        dbconn = new SqliteConnection(Connection);
        dbconn.Open();
        string query; = "CREATE TABLE save_string (super_id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, id integer, value varchar(200), name varchar(100))";
        try
        {
            dbcmd = dbconn.CreateCommand(); // create empty command
            dbcmd.CommandText = query; // fill the command
            reader = dbcmd.ExecuteReader(); // execute command which returns a reader
            StartInsertString(); 
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        //*/
#endif
    }

    public void StartInsertString()
    {
        save_String.StartInsertString(this);
    }

    public void UpdateString(string value, int id)
    {
        save_String.UpdateString(this, value, id);
    }

    public string ReadString(int id)
    {
        return save_String.ReadString(this, id);
    } 
}
