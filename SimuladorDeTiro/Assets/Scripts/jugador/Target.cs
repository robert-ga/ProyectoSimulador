using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    //public GameObject blanco;
    //public GameObject puntero;
    //public Text dis;
    //private float distacia;
    private void Update()
    {
        //distacia = Vector3.Distance(blanco.transform.position, puntero.transform.position);
        //int x= (int)Math.Round(distacia);
        //print(x);
    }
    public void Hit()
    {
        transform.position = TargetBouns.Instance.GetRamdomPosition();
    }
}
