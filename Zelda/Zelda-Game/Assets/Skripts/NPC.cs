using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    //-------Variablen für Skript-----------                                                                             
    public bool Aktiv;               //Event Trigger
    public bool Verfolgen;           //Event Trigger



    //-------Variablen für Skript----------- 
    public Vector3 position;
    private Vector3 sehen;
    public Vector3 rückschlag;

    public float laufGeschwindichkeit = 1.5f;
    public float abstand;           
    public float speed;


    //-----Variablen von Componente---------
    private Rigidbody2D myRigidbody;       // 
    private Animator animator;             // 
    private Transform transform;           //
    private SpielerDaten spielerDaten;     //



    void Start()
    {

        animator = GetComponent<Animator>();                     
        myRigidbody = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        spielerDaten = GameObject.Find("Spieler").GetComponent<SpielerDaten>();

    

    }

    void Update()
    {
        

    }


    void OnTriggerEnter2D(Collider2D Objekt)
    {
        if (Objekt.gameObject.tag == "Player")
        {
            Debug.Log("Event An");
            Aktiv = true;

        }
       

    }

    void OnTriggerExit2D(Collider2D Objekt)
    {
        if (Objekt.gameObject.tag == "Player")
        {
            Debug.Log("Event Aus");
            Aktiv = false;
            spielerDaten.spielerStatus = SpielerDaten.Status.laufen;

        }

    }

    void SpielerFolgen()
    {
        position = transform.position;

        abstand = Vector3.Distance(spielerDaten.position, this.transform.position);

        sehen = spielerDaten.position - position;



        if ((abstand > 1.5 ) && (sehen != Vector3.zero))     
        {

            animator.SetBool("Bewegung", true);
            animator.SetFloat("x", sehen.x);                     
            animator.SetFloat("y", sehen.y);

            myRigidbody.MovePosition(position + (spielerDaten.position - position) * laufGeschwindichkeit * Time.fixedDeltaTime);

        }
        else
        {

             animator.SetBool("Bewegung", false);
            
        }

    }

    void Laufen ()
    {

        if (sehen != Vector3.zero)                   //Wird gedrückt?
        {

            myRigidbody.MovePosition(transform.position + sehen * laufGeschwindichkeit * Time.fixedDeltaTime);    // Position + Richtung*Geschwindichkeit*Zeit
                                                                                                                                  //Aktion
            animator.SetFloat("x", sehen.x);                     //Animation
            animator.SetFloat("y", sehen.y);                     //Animation
            animator.SetBool("Bewegung", true);                     //Animation

        }

        else                                            //Wird nicht gedrückt?
        {

            animator.SetBool("Bewegung", false);                    //Animation

        }

    }

  



}

