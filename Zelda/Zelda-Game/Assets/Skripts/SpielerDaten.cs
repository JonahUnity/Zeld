using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpielerDaten : MonoBehaviour
{
    public enum Status { 
        laufen, 
        reden, 
        schlag, 
        rennen, 
        trinken, 
        getroffen };

    public Status spielerStatus = Status.laufen;                        //Spieler Status

    


    public float laufGeschwindichkeit = 5;                              // Laufgeschwindichkeit von Spieler
    public int leben = 10;

    public Vector3 position;                                            // Position vom Spieler

    public Vector3 richtung;

   








    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

       

    }

   



}


