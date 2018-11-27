using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float jumpimpulse;
    private float h;
    public float Speed;
    public float PlayerHealth;

    private float minYwalkable;
    public float AirFactor = 0.5f;

    private float numerosaltos = 0;
    public LayerMask groundLayer;
    private Rigidbody2D rb2d;
    public Text pausita;
    public Image ok;
    public float grounddetectionradius = 0.5f;
    private SpriteRenderer spr;
    private Animator ator;

    private float velocidadx;
    private bool jump;
    private bool jumppressed = false;
    private bool grounded;
    private RaycastHit2D[] results = new RaycastHit2D[14];
    [Range(0, 90)] public float maxSlope;


    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();
        ator = GetComponent<Animator>();

        minYwalkable = Mathf.Cos(Mathf.Deg2Rad * maxSlope);

    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal");

        jump = (Input.GetAxis("Jump") > 0);

       
        if (!jump)

            velocidadx = h * Speed;

        ator.SetFloat("Velocidad", Mathf.Abs(velocidadx));
        ator.SetBool("Grounded", grounded);
        jumppressed = false;



        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pausita.enabled = false;

            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pausita.enabled = true;
            }
        }


        /* //Rotamos el gameobject del personaje(no el sprite) para mantener la posicion del punto de disparo         
         if (h < 0)
         {
             transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));

         }
         if (h > 0)
         {
             transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));

         }*/


        if (!grounded)
            h *= AirFactor;
        if (h > 0)
            spr.flipX = false;
        if (h < 0)
            spr.flipX = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Suelo")
        {
            
            numerosaltos = 0;
        }
        if (collision.gameObject.tag == "End Game")
        {

            Destroy(this.gameObject);
            

        }
    }

    private void FixedUpdate()
    {

        //rb2d.velocity = new Vector2(velocidadx, rb2d.velocity.y);

        int nresults = rb2d.Cast(Vector2.down, results, grounddetectionradius);
        grounded = (nresults > 0);

        if (nresults > 0)//comprobar con la primera colision que reciba la direccion de esta
        {
            Vector2 normal = results[0].normal;
            //si la pendiente es demasiado grade se considera pared
            if (normal.y < minYwalkable)
            {

            }
            else
            {
                rb2d.velocity = new Vector2(normal.y * velocidadx, -normal.x * velocidadx);
            }
        }
        else
        {
            rb2d.velocity = new Vector2(velocidadx, rb2d.velocity.y);
        }

        if (jump && !jumppressed && grounded)
        {

            numerosaltos = 1;
            rb2d.AddForce(new Vector2(0, jumpimpulse), ForceMode2D.Impulse);
            jumppressed = true;

        }




    }

}

