using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSkript : MonoBehaviour
{
   
    //-------Variablen für Skript------------                                                                             
    private Vector3 richtung;        // Richtung
    private Vector3 position;
    private Vector3 richtungRuckstoss;
    private Vector3 Test;


    //-----Variablen für Componente-----------

    private Rigidbody2D myRigidbody;       // 
    private Animator animator;             // 
    private Transform transform;
    private SpielerDaten spielerDaten;     //
    private GegnerSkript gegnerSkript;

    

   


    void Start()  // Wird 1mal ausgeführt
    {
        animator = GetComponent<Animator>();                      // Varibalen von Animatior
        myRigidbody = GetComponent<Rigidbody2D>();                // Bewegung
        transform = GetComponent<Transform>();
        spielerDaten = GetComponent<SpielerDaten>();
        gegnerSkript = GetComponent<GegnerSkript>();

        GameObject Spieler;


    }

    void Update()   // Wird konstand ausgeführt                                           
    {

                                                     
        richtung.x = Input.GetAxisRaw("x-Achse");      // Eingabe in Variablen speichern             
        richtung.y = Input.GetAxisRaw("y-Achse");


        spielerDaten.position = transform.position;     // Position für SpielerDaten


        if (Input.GetButtonDown("Space") && spielerDaten.spielerStatus != SpielerDaten.Status.schlag)
        {
            spielerDaten.spielerStatus = SpielerDaten.Status.schlag;
            StartCoroutine("Schlag");

        }
        if(spielerDaten.spielerStatus == SpielerDaten.Status.laufen)
        {
            Laufen();
        }



       

        
    
    }

    void Laufen()
    { 
        if(richtung != Vector3.zero)                   //Wird gedrückt?
        {

            myRigidbody.MovePosition(transform.position + richtung * spielerDaten.laufGeschwindichkeit * Time.fixedDeltaTime);    // Position + Richtung*Geschwindichkeit*Zeit
                                                                                                                                  //Aktion
            animator.SetFloat("x", richtung.x);                     //Animation
            animator.SetFloat("y", richtung.y);                     //Animation
            animator.SetBool("Bewegung", true);                     //Animation

        }
        
        else                                            //Wird nicht gedrückt?
        {

            animator.SetBool("Bewegung", false);                    //Animation

        }    
    }

  

    IEnumerator Schlag()
    {
    
            animator.SetBool("Schlag", true);
            

            yield return null;

            animator.SetBool("Schlag", false);

            yield return new WaitForSeconds(0.2f);

            spielerDaten.spielerStatus = SpielerDaten.Status.laufen;

    }

    

    

    
    




}


 // Debug.Log();