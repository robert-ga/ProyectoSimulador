using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class menulogin : MonoBehaviour
{
    // Start is called before the first frame update
    //private string usuario;
    //private string contraseña;
    public InputField usuario;
    public InputField contrasenaa;
    IDbConnection dbcon;
    private void Start()
    {
        //Cursor.visible = false;
    }
    //string nombre;
    public void ingresar()
    {
        
        conparacion(usuario.text);
        
        //recuperanombre();
    }
    public void conparacion(string name)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT id_usuario,usuario,contrasena FROM usuarios";
        //cmnd.ExecuteNonQuery();
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            
            if (usuario.text==reader.GetString(1) && contrasenaa.text == reader.GetString(2))
            {

                recuperar.name = reader.GetString(1);
                recuperar.idilogin = reader.GetInt32(0).ToString();
                SceneManager.LoadScene("MenuEntrenador");
    
                //Search_function(usuario.text);
                //print("jugador");

            }
            if(usuario.text == "admi" && contrasenaa.text == "admi123")
            {
                SceneManager.LoadScene("MenuAdministrador");
                //print("administrador");
            }
            else
            {
                //print("no esta registrado");
            }
        }
        //return usuario.text;
        //desconexiondb();
    }
    public void conexiondb()
    {
        string conn = "URI=file:" + Application.dataPath + "/BD/" + "simulador.db"; //Path to database.
        dbcon = new SqliteConnection(conn);
        dbcon.Open();
    }
    public void desconexiondb()
    {
        dbcon.Close();
    }
    /*public void Search_function(string nombree)
    {
        string Name_readers_Search, Address_readers_Search;
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        string sqlQuery = "SELECT id_usuario,usuario,contrasena " + "FROM usuarios where usuario=" + nombree;// table name
        cmnd.CommandText = sqlQuery;
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            string id = reader.GetString(0);
            Name_readers_Search = reader.GetString(1);

            Address_readers_Search = reader.GetString(2);
            //recuperar.idi = Search_by_id;
            //usuario.text += Name_readers_Search + " - " + Address_readers_Search + "\n";

            Debug.Log(" id =" + Name_reade + "Address=" + Address_readers_Search);

        }
        desconexiondb();

    }*/
    /*public void recuperanombre()
    {
        nombre = conparacion();
        print(nombre);
    }*/
}
