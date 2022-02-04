using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.SceneManagement;
public class usuariosregistrados : MonoBehaviour
{
    IDbConnection dbcon;
    public GameObject cell;
    public Transform ce;
    public InputField nombre;
    public Button todo;

    //public GameObject panelre;
    //public Text texvi;
    void Start()
    {
        //readdb();

        //delateususario(18);
        //llenardatos();
        //texvi.enabled = false;
        //refrescar();
        //panelre.SetActive(false);

    }
    void Update()
    {

        //refrescar();
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
    public void readdb()
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT usuario,contrasena FROM usuarios";
        //cmnd.ExecuteNonQuery();
        IDataReader reader = cmnd.ExecuteReader();
        while (reader.Read())
        {
            string usuario = reader.GetString(0);
            string contrasena = reader.GetString(1);
            Debug.Log("usuario= " + usuario + "  contraseña =" + contrasena);
        }
        //desconexiondb();
    }
    public class Datosusuarios
    {
        public int id;
        public string name;
        public string contra;
        public Datosusuarios(int id, string name, string contra)
        {
            this.id=int.Parse(id.ToString());
            this.name = name;
            this.contra = contra;
        }
    }
    public List<Datosusuarios> getLeaderBoard()
    {
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM usuarios ORDER BY id_usuario DESC";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datosusuarios> datosusuarios = new List<Datosusuarios>();
        while (reader.Read())
        {
            datosusuarios.Add(new Datosusuarios(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString()));
            print(datosusuarios);
        }
        return datosusuarios;
    }
    public void llenardatos()
    {
        List<Datosusuarios> datosusuarios = getLeaderBoard();
        
        for(int i = 0; i < datosusuarios.Count; i++)
        {
            /*GameObject obj = Instantiate(cell, Vector3.zero,Quaternion.identity);
            obj.transform.SetParent(parentContent.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.GetChild(0).GetComponent<Text>().text = i.name;
            obj = Instantiate(cell, Vector3.zero,Quaternion.identity);
            obj.transform.SetParent(parentContent.transform);
            obj.transform.localScale = Vector3.one;
            obj.transform.GetChild(0).GetComponent<Text>().text =i.contra;*/

            GameObject da = Instantiate(cell);
            Datosusuarios datos = datosusuarios[i];
            da.GetComponent<llenardatosusuarios>().setdatos(datos.id,datos.name, datos.contra);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            //print(da);

        }
    }

    public void delateususario(string id)
    {
        conexiondb();
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText =string.Format("DELETE FROM usuarios WHERE id_usuario = \"{0}\"", id);
        //print("selimuni");
        cmnd.ExecuteNonQuery();
        desconexiondb();
        
    }
    public List<Datosusuarios> buscar(string Search_by_name)
    {
        conexiondb();
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        //string query = "SELECT * FROM usuarios";
        string query = "SELECT * FROM usuarios where usuario LIKE\'" + Search_by_name + "\'";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        List<Datosusuarios> datosusuarios = new List<Datosusuarios>();
        while (reader.Read())
        {
            datosusuarios.Add(new Datosusuarios(int.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString()));
            print(datosusuarios);
        }

        return datosusuarios;
    }
    public void llenardatosbuscados()
    {

        List<Datosusuarios> datosusuarios = buscar(nombre.text);
        //refrescar();
        for (int i = 0; i < datosusuarios.Count; i++)
        {
            GameObject da = Instantiate(cell);
            Datosusuarios datos = datosusuarios[i];
            da.GetComponent<llenardatosusuarios>().setdatos(datos.id, datos.name, datos.contra);
            da.transform.SetParent(ce);
            da.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

        }

    }


    public void mostrartodo()
    {
        llenardatos();
    }
    void TaskOnClick()
    {
        //Output this to console when Button1 or Button3 is clicked
        SceneManager.LoadScene("Usuarios");
    }
    public void usuario()
    {
        //refrescar();
        llenardatosbuscados();
    }
    public void atras()
    {
        SceneManager.LoadScene("MenuAdministrador");
    }
    public void reg()
    {
        SceneManager.LoadScene("Registro");
    }
    public void refrescar()
    {
        SceneManager.LoadScene("Usuarios");
    }
    public void salir()
    {

    }
}
