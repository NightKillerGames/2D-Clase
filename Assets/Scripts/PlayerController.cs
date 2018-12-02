using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public float jumpimpulse;
    public float speed;
    public float airFactor = 0.5f;
    public float grounddetectionradius = 0.5f;

    [HideInInspector]public int playerHealth = 3;
    private bool playerIsDead = false;
    private float h;
    private bool subida = false;
    private float velocidadx;
    private bool jump;
    private bool grounded;
    private RaycastHit2D[] results = new RaycastHit2D[14];
    private Rigidbody2D rb2d;
    private Animator ator;

    public float shootDelayMaxTime = 0.1f;
    private float shootDelay;

    public Transform firePoint;
    public GameObject BellotaPrefab;
    private bool puerta = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
    }
    public void GameOver()
    {
        Debug.Log("Muerto");
        
    }
    private void Update()
    {
        if (playerIsDead)
        {
            GameOver();
            
            return;
        }
        h = Input.GetAxis("Horizontal");

        jump = (Input.GetAxis("Jump") > 0);

       
        if (!jump)
        {
            velocidadx = h * speed;
        }

        if (rb2d.velocity.y > 0)
        {
            subida = true;
        }
        if (rb2d.velocity.y < 0)
        {
            subida = false;
        }

        ator.SetFloat("Velocidad", Mathf.Abs(velocidadx));
        ator.SetBool("Grounded", grounded);
        ator.SetBool("Subida", subida);

        shootDelay += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && shootDelay>=shootDelayMaxTime)
        {
            Shoot();
            shootDelay = 0;
        }
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                //pausita.enabled = false;

            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
               // pausita.enabled = true;
            }
        }

        //Rotamos el gameobject del personaje(no el sprite) para mantener la posicion del punto de disparo         
        if (h < 0)
             transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        if (h > 0)
             transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));

        /*if (!grounded) velocidadx *= airFactor; */ //He arreglado lo del air factor pero creo que no tiene sentido en este juego, lo dejo por si lo usamos como un debuff o aglko

        if (puerta && Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene("Level01");
        }
        Debug.Log(h.ToString("n2") + " " + velocidadx.ToString("n2") + " " + rb2d.velocity.x.ToString("n2"));
    }
  


    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(velocidadx, rb2d.velocity.y);
        int nresults = rb2d.Cast(Vector2.down, results, grounddetectionradius);
        grounded = (nresults > 0);
        //Debug.Log(nresults +" " + grounded + " "+ rb2d.velocity.y);
        if (jump && grounded)
        {
            rb2d.AddForce(new Vector2(0, jumpimpulse), ForceMode2D.Impulse);
        }
    }

    void Shoot()
    {
        Instantiate(BellotaPrefab, firePoint.position, firePoint.rotation);
    }
    public void TakeDmg()
    {
        playerHealth-=1;
        if(playerHealth == 0)
        {
            playerIsDead = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puerta"))
        {
            puerta = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puerta"))
        {
            puerta = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "End Game") 
        {
            Destroy(this.gameObject);
        }
    }
}