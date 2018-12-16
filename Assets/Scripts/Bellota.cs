using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellota : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;
    public Rigidbody2D rb;

    private float liveTime = 3f;
    private float timer = 0;

    private AbstractEnemy Enemigo;
    private BossFightController Boss;
    private AudioManager audio;

    private void Start()
    {
        rb.velocity = transform.right * speed;
        audio= GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= liveTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemigo"))
        {
            Enemigo = hitInfo.GetComponent<AbstractEnemy>();
            Enemigo.TakeDamage(damage);
            audio.InpactoBellota();
            Destroy(gameObject);
        }
        else if (hitInfo.CompareTag("Boss"))
        {
            Boss = hitInfo.GetComponent<BossFightController>();
            Boss.TakeDmg(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

