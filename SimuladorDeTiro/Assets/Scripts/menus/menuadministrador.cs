using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class menuadministrador : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void registo()
    {
        SceneManager.LoadScene("Registro");
    }
    public void usuarios()
    {
        SceneManager.LoadScene("Usuarios");
    }
    public void estadisitcas()
    {
        SceneManager.LoadScene("EstadisticasUsuarios");
    }
    public void salir()
    {
        SceneManager.LoadScene("Login");
    }
}
