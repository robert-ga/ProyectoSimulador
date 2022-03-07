using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class calibrar : MonoBehaviour
{
    [SerializeField] Camera cam;
    private float timeRemaining=1;
    public Text timeText;
    int muni;
    DateTime fecha = DateTime.Today;
    string hora = DateTime.Now.ToShortTimeString();
    IDbConnection dbcon;
    private GameObject cab;
    public GameObject target;

    void Start()
    {
        
    }
    
    void Update()
    {
        timer();
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    
    public float timer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

        }
        else
        {
            timeRemaining = 0;

            if (timeRemaining == 0)
            {
                timeRemaining = 1;
                menu();
                //print( timeRemaining+ " " + canti + " " + precision());
            }
            //print("1:30    " + kills() +"   "+ p+"%");
            //SceneManager.LoadScene("MenuEntrenador");
        }
        //print("holaaaa" + xx);
        
        DisplayTime(timeRemaining);
        return timeRemaining;
        //return p.ToString();
    }
    public void menu()
    {
        SceneManager.LoadScene("MenuEntrenador");
    }
}
