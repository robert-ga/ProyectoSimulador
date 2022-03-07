using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class menuentrenador : MonoBehaviour
{
    public Text nombrere;
    int ti,d,t,m;
    // Start is called before the first frame update
    private void Start()
    {
        nombrere.text = recuperar.name;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        //Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void entrenar()
    {
        recuperar.tipo = 1;
        d = int.Parse(recuperar.distancia);
        t = int.Parse(recuperar.tiempo);
        m = recuperar.municion;
        SceneManager.LoadScene("Entrenamiento");
    }
    public void examen()
    {
        recuperar.tipo = 0;
        SceneManager.LoadScene("Examen");
    }
    public void estadistica()
    {
        SceneManager.LoadScene("Estadisticas");
    }
    public void opciones()
    {
        SceneManager.LoadScene("Opciones");
    }
    public void salir()
    {
        SceneManager.LoadScene("Login");
    }
    
}
