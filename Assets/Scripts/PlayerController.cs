using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    /// <summary>
    /// hay que acordarse de no dejar numeros hardcodeados 
    /// </summary>
    public float jumpimpulse;
    public float speed;
    public float grounddetectionradius = 0.5f;
    public float invincibilityTime = 0.5f;
    public Vector2 empujeDMG;
    public float radioPunch = 0.2f;
    public LayerMask lm;
    public int punchDamage = 5;
    public float shootDelayMaxTime = 0.1f;
    public float punchDelayMaxTime = 0.5f;

    public Transform firePoint;
    public GameObject BellotaPrefab;
    public Transform puñetazo;

    private bool canMove = true;
    private bool dmg = false;
    private bool empuje = false;
    private float invincibilityTimeCounter;
    private float h;
    private bool subida = false;
    private float velocidadx;
    private bool jump;
    private bool grounded;
    private bool jumpReleased;
    private float shootDelay;
    private float punchDelay;
    private bool puerta = false;
    private bool invencible = false;
    private float cantMoveTime = 0.2f;
    private int maxMovement = 9;

    private RaycastHit2D[] results = new RaycastHit2D[14];
    private Rigidbody2D rb2d;
    private Animator ator;
    private GameManager gm;
    private AudioManager _audio;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ator = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        _audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void Update()
    {
        ator.SetBool("Dmg", dmg);
       
        if (dmg)
        {
            invincibilityTimeCounter += Time.deltaTime;
            invencible = true;
            if (invincibilityTimeCounter >= invincibilityTime)
            {
                invencible = false;
                dmg = false;
                invincibilityTimeCounter = 0;
            }
        }
        h = Input.GetAxis("Horizontal");

        jump = (Input.GetAxis("Jump") > 0);
        
        velocidadx = h * speed;


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
        if (Input.GetButtonDown("Fire2") && shootDelay >= shootDelayMaxTime)
        {
            _audio.bellota();
            Shoot();
            shootDelay = 0;
        }
        punchDelay += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && punchDelay >= punchDelayMaxTime)
        {
            _audio.punch();
            ator.SetTrigger("puño");
            AtaqueCorto();
            punchDelay = 0;
        }
        //Rotamos el gameobject del personaje(no el sprite) para mantener la posicion del punto de disparo         
        if (h < 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
        if (h > 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 360, 0));

        if (puerta && Input.GetKeyDown(KeyCode.E)) {
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
            _audio.jump();
        }
        if (empuje)
        {
            rb2d.AddForce(empujeDMG, ForceMode2D.Impulse);
            empuje = false;
            StartCoroutine("StopPlayerMovement");
        }
        if (rb2d.velocity.y> maxMovement)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, maxMovement);
        }
    }
    IEnumerator StopPlayerMovement()
    {
        canMove = false;
        yield return new WaitForSeconds(cantMoveTime);
        canMove = true;
    }

    void Shoot()
    {
        Instantiate(BellotaPrefab, firePoint.position, firePoint.rotation);
    }

    public void TakeDmg()
    {
        //_audio.Dmgoof();
        if (invencible)
        {
            return;
        }
        gm.playerHealth -= 1;
        empuje = true;
        dmg = true;
        if (gm.playerHealth == 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            empuje = false;
            gm.gameOver = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "End Game")
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            dmg = true;
            gm.gameOver = true;
        }
        if (collision.gameObject.CompareTag("Puerta"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "Gemas")
        {
            gm.GemaCogida();
            Destroy(collision.gameObject);
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(5)){
            if (collision.CompareTag("FinalDoor"))
            {
                gm.Victoria();
            }
        }
        if (collision.gameObject.CompareTag("Hazard"))
        {
            TakeDmg();
        }
        if (collision.gameObject.CompareTag("BossDoor"))
        {
            GameObject.FindGameObjectWithTag("Boss").GetComponent<BossFightController>().EmpezarCombate();
        }
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard") || collision.gameObject.CompareTag("Boss"))
        {
            TakeDmg();
        }
    }
    private void AtaqueCorto()
    {
        Collider2D atacado = Physics2D.OverlapCircle(puñetazo.position, radioPunch, lm, -Mathf.Infinity, Mathf.Infinity);
        if (atacado != null)
        {
            if (atacado.CompareTag("Enemigo"))
            {
                atacado.GetComponent<BasicEnemy>().TakeDamage(punchDamage);
            }else if (atacado.CompareTag("Boss"))
            {
                atacado.GetComponent<BossFightController>().TakeDmg(punchDamage);
            }
        }
        else
        {
          
        }
    }
}