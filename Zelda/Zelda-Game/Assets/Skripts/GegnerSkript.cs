using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GegnerSkript : MonoBehaviour
{

    //-------Variablen für Skript-----------                                                                             
    public bool aktiv;               //Aktiver Gegner
   
   
    public enum Status { laufen, reden, schlag, rennen, warten};            //Gegner Status 
    public Status gegnerStatus = Status.warten;                             



    //-------Variablen zum Einstellen----------- 
    // public GameObject Gegner;

    public Vector3 position;                            // Position Gegner
    public Vector3 abstandVector;                       // Abstand zu Spieler in Vector
    public Vector3 abstandRichtung;                     // Richtung zum Spieler 

    public float abstandTotal;                          // Abstand Spieler zu Gegner als länge 
                              
    
    public float laufGeschwindichkeit = 1.5f;
    public float triggerAbstand = 5f;
    public float schadenAbstand = 1f;
    public float schaden = 1f;
    public float lebenGegner = 5f;

   

    //-----Variablen von Componente---------
    private Rigidbody2D myRigidbody;       // 
    private Animator animator;             // 
    private Transform transform;           //
    private SpielerDaten spielerDaten;     //
    private PlayerSkript playerSkript;

    

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        spielerDaten = GameObject.Find("Spieler").GetComponent<SpielerDaten>();
        playerSkript = GameObject.Find("Spieler").GetComponent<PlayerSkript>();

       

        gegnerStatus = Status.laufen;

      //  Debug.Log(GameObjekt.name);

    }



    void Update()

    {

        Abstand();
        Laufen();
        Getroffen();
        Tot();
             


    }



    void Abstand()
    {
        position = transform.position;
        abstandTotal = Vector3.Distance(spielerDaten.position, this.transform.position);

        abstandVector = spielerDaten.position - position;

        abstandRichtung = Vector3.Normalize(abstandVector);

       
        if (abstandTotal > triggerAbstand)                                                                              // Keiner da
        {
            gegnerStatus = Status.warten;
        }

        if ((abstandTotal < triggerAbstand) && (abstandTotal > schadenAbstand) && (gegnerStatus != Status.schlag))      
        {
            gegnerStatus = Status.laufen;
        }

        if ((abstandTotal <= schadenAbstand) && (gegnerStatus != Status.schlag))
        {
            gegnerStatus = Status.schlag;
            StartCoroutine("Schlag");
        }
       



      

    }

    void Laufen()
    {
        if (gegnerStatus == Status.laufen )
        {
            animator.SetBool("Bewegung", true);
            animator.SetFloat("x", abstandRichtung.x);
            animator.SetFloat("y", abstandRichtung.y);

            myRigidbody.MovePosition(position + abstandRichtung * laufGeschwindichkeit * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("Bewegung", false);
        }
    }

    void Getroffen()
    {
        if((spielerDaten.spielerStatus == SpielerDaten.Status.schlag) && (abstandTotal <= 1.2f))
        {
            transform.position = (position + (abstandRichtung * (-3)));

            lebenGegner--;
        }
    }

    void Tot ()
    {
        if(lebenGegner == 0)
        {
            Destroy(gameObject, 5);
        }
    }


    IEnumerator Schlag ()
    {

           
        animator.SetFloat("x", abstandRichtung.x);
        animator.SetFloat("y", abstandRichtung.y);

            
        animator.SetBool("Schlag", true);
        yield return null;

        
        StartCoroutine("Treffer");

        animator.SetBool("Schlag", false);
        yield return new WaitForSeconds(0.4f);
        

        gegnerStatus = Status.laufen;


        

    }

    IEnumerator Treffer()
    {

        GameObject.Find("Spieler").transform.position = (position + (abstandRichtung * 3));
        yield return new WaitForSeconds(0);
        spielerDaten.leben--;


    }










}

