﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour { 
/// <summary>
/// hay que acordarse de no dejar numeros hardcodeados que este señor nos baja 2 puntos seguro
/// </summary>
    public float jumpimpulse;
    public float speed;
    public float grounddetectionradius = 0.5f;
    public float invincibilityTime = 0.5f;
    public Vector2 empujeDMG;
    [HideInInspector] public int playerHealth = 3;
    [HideInInspector] public bool gameOver = false;

    private bool canMove = true;
    private bool dmg = false;
    private bool empuje = false;
    private float invincibilityTimeCounter;
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
 
    private void Update()
    {
        ator.SetBool("Dmg", dmg);
        if (gameOver)
        {
            GameOver();
            return;
        }
        if (dmg)
        {
            invincibilityTimeCounter += Time.deltaTime;
            if(invincibilityTimeCounter >= invincibilityTime)
            {
                dmg = false;
                invincibilityTimeCounter = 0;
            }
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
        if (Input.GetButtonDown("Fire1") && shootDelay >= shootDelayMaxTime)
        {
            Shoot();
            shootDelay = 0;
        }

        //Rotamos el gameobject del personaje(no el sprite) para mantener la posicion del punto de disparo         
        if (h < 0)
             transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        if (h > 0)
             transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));

        if (puerta && Input.GetKeyDown(KeyCode.E)){
            SceneManager.LoadScene("Level01");
        }
        //Debug.Log(h.ToString("n2") + " " + velocidadx.ToString("n2") + " " + rb2d.velocity.x.ToString("n2"));
    }

    private void FixedUpdate()
    {
        if (canMove) {
            rb2d.velocity = new Vector2(velocidadx, rb2d.velocity.y);
        }
        int nresults = rb2d.Cast(Vector2.down, results, grounddetectionradius);
        grounded = (nresults > 0);
        //Debug.Log(nresults +" " + grounded + " "+ rb2d.velocity.y);
        if (jump && grounded && canMove)
        {
            rb2d.AddForce(new Vector2(0, jumpimpulse), ForceMode2D.Impulse);
        }
        if (empuje)
        {
            rb2d.AddForce(empujeDMG, ForceMode2D.Impulse);
            empuje = false;
            StartCoroutine("StopPlayerMovement");
        }
    }
    IEnumerator StopPlayerMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(0.1f);
        canMove = true;
    }
    IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

    void Shoot()
    {
        Instantiate(BellotaPrefab, firePoint.position, firePoint.rotation);
    }
  
    public void GameOver()
    {
        Debug.Log("Muerto");
        StartCoroutine("GoToMenu");
        GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void TakeDmg()
    {
        playerHealth-=1;
        empuje = true;
        dmg = true;
        if(playerHealth == 0)
        {
            empuje = false;
            gameOver = true;
        }
     }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Puerta"))
        {
            SceneManager.LoadScene("Level01");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "End Game") 
        {
            ator.SetBool("Dmg", dmg);
            gameOver =true;
        }
    }
}