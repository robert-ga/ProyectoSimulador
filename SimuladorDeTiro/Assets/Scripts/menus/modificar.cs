using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class modificar : MonoBehaviour
{
    IDbConnection dbcon;
    public InputField nombre;
    //dalateuser d;
    public Text reid;
    public InputField contra;
    public Text idf;
    public GameObject panelre;
    // Start is called before the first frame update
    void Start()
    {
        modi();
        idf.enabled = false;
        panelre.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void bmo()
    {
        reid.text = recuperar.idi;
        update_function(reid.text, nombre.text, contra.text);
        panelre.SetActive(true);
    }
    public void modi()
    {
        reid.text = recuperar.idi;
        F_function(reid.text);
    }
    private void F_function(string Search_by_id)
    {
        
        string Name_readers_Search, Address_readers_Search;
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        string sqlQuery = "SELECT usuario,contrasena " + "FROM usuarios where id_usuario =" + Search_by_id;// table name
        cmnd.CommandText = sqlQuery;
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {

            Name_readers_Search = reader.GetString(0);
            Address_readers_Search = reader.GetString(1);
            nombre.text = Name_readers_Search;
            contra.text = Address_readers_Search;

        }
        desconexiondb();
    }
    private void update_function(string update_id, string update_name, string update_address)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = string.Format("UPDATE usuarios set usuario = @name ,contrasena = @address where id_usuario = @id ");
        SqliteParameter P_update_name = new SqliteParameter("@name", update_name);
        SqliteParameter P_update_address = new SqliteParameter("@address", update_address);
        SqliteParameter P_update_id = new SqliteParameter("@id", update_id);
        cmnd.Parameters.Add(P_update_name);
        cmnd.Parameters.Add(P_update_address);
        cmnd.Parameters.Add(P_update_id);

        cmnd.ExecuteNonQuery();

        desconexiondb();
        F_function(reid.text);

        // SceneManager.LoadScene("home");
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
    public void atras()
    {
        SceneManager.LoadScene("Usuarios");
    }
}
