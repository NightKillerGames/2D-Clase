using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellota : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 10;
    // Use this for initialization
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemigo"))
        {
            EnemyAi Enemigo = hitInfo.GetComponent<EnemyAi>();



            Enemigo.TakeDamage(damage);


            Debug.Log(hitInfo);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        

    }
}

