using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ConexionDB : MonoBehaviour
{
    IDbConnection dbcon;
    public InputField usuarioregistro;
    public InputField contrasenaregistro;
    string nombre;
    public GameObject panelre;
    public Text posi, nega;
    public void Start()
    {
        panelre.SetActive(false);
        posi.enabled = false;
        nega.enabled = false;
    }
    public string readdb()
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT usuario,contrasena FROM usuarios";
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            
            string usuario = reader.GetString(0);
            string contrasena = reader.GetString(1);
            if (usuarioregistro.text==usuario)
            {
                nombre = usuarioregistro.text;
                print("se encontro");
            }
        }
        print(nombre);
        return nombre;
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
    public void Search_function(string nombre)
    {
        string Name_readers_Search, Address_readers_Search;
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        string sqlQuery = "SELECT usuario" + "FROM usuarios where usuario =" + nombre;// table name
        cmnd.CommandText = sqlQuery;
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            Name_readers_Search = reader.GetString(0);
            //Address_readers_Search = reader.GetString(1);
            
            //Debug.Log(" name =" + Name_readers_Search + "Address=" + Address_readers_Search);

        }
        
        desconexiondb();
    }

    public void registrarusuario()
    {
        readdb();
        if (readdb() != usuarioregistro.text)
        {
            conexiondb();
         
            IDbCommand cmnd = dbcon.CreateCommand();
            cmnd.CommandText = "INSERT INTO usuarios (usuario,contrasena) VALUES(\'" + usuarioregistro.text + "\',\'" + contrasenaregistro.text + "\')";
            cmnd.ExecuteNonQuery();
            desconexiondb();
            panelre.SetActive(true);
            posi.enabled = true;
            nega.enabled = false;
            print("se registro");
        }
        else
        {
            panelre.SetActive(true);
            posi.enabled = false;
            nega.enabled = true;
            print("ya registrado");
        }
    }
    
    public void regresar()
    {
        SceneManager.LoadScene("Registro");
    }
}
