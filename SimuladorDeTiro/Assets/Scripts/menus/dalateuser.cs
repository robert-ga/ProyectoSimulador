using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;

public class dalateuser : MonoBehaviour
{
    IDbConnection dbcon;
    public Text id;
   // public GameObject panelre;
    // Start is called before the first frame update
    void Start()
    {

        //panelre.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void eli()

    {
       // panelre.SetActive(true);
        delateususario(id.text);
        delatepuntuacion(id.text);
        delateexamen(id.text);
    }
    private void delateususario(string delid)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "DELETE FROM usuarios WHERE id_usuario LIKE \'" + delid + "\'";
        cmnd.ExecuteNonQuery();
        id.text = delid;
        print(id.text);
        desconexiondb();
    }
    private void delatepuntuacion(string delid)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "DELETE FROM puntuacion WHERE id_usuario LIKE \'" + delid + "\'";
        cmnd.ExecuteNonQuery();
        id.text = delid;
        print(id.text);
        desconexiondb();
    }
    private void delateexamen(string delid)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "DELETE FROM examen WHERE id_usuario LIKE \'" + delid + "\'";
        cmnd.ExecuteNonQuery();
        id.text = delid;
        print(id.text);
        desconexiondb();
    }
    private void update_function(string update_id, string update_name, string update_address)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = string.Format("UPDATE usuarios set name = @name ,address = @address where ID = @id ");
        SqliteParameter P_update_name = new SqliteParameter("@name", update_name);
        SqliteParameter P_update_address = new SqliteParameter("@address", update_address);
        SqliteParameter P_update_id = new SqliteParameter("@id", update_id);
        cmnd.Parameters.Add(P_update_name);
        cmnd.Parameters.Add(P_update_address);
        cmnd.Parameters.Add(P_update_id);

        cmnd.ExecuteNonQuery();

        desconexiondb();
        Search_function(id.text);

        // SceneManager.LoadScene("home");
    }

    public void Search_function(string Search_by_id)
    {
        string Name_readers_Search, Address_readers_Search;
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        string sqlQuery = "SELECT usuario,contrasena " + "FROM usuarios where id_usuario =" + Search_by_id;// table name
        cmnd.CommandText = sqlQuery;
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            //  string id = reader.GetString(0);
            Name_readers_Search = reader.GetString(0);
            Address_readers_Search = reader.GetString(1);
            recuperar.idi = Search_by_id;
            id.text += Name_readers_Search + " - " + Address_readers_Search + "\n";

            Debug.Log(" name =" + Name_readers_Search + "Address=" + Address_readers_Search);

        }
        desconexiondb();

        /*string Name_readers_Search, Address_readers_Search;
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        string sqlQuery = "SELECT usuario,contrasena " + "FROM usuarios where id_usuario =" + Search_by_id;// table name
        cmnd.CommandText = sqlQuery;
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {

            Name_readers_Search = reader.GetString(0);
            Address_readers_Search = reader.GetString(1);
            t_name.text = Name_readers_Search;
            t_Address.text = Address_readers_Search;

        }
        desconexiondb();*/
    }
    public void modificar()
    {
        SceneManager.LoadScene("Modificar");
        Search_function(id.text);
    }
    public void llamr()
    {
        Search_function(id.text);
    }
    public void conexiondb()
    {
        string conn = "URI=file:" + Application.dataPath + "/BD/" + "simulador.db"; //Path to database.
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
        print("se conecto");
    }
    public void desconexiondb()
    {
        dbcon.Close();
    }
}
