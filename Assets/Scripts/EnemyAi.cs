using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public int health = 100;
    private Animator ator2;
    private PlayerController pc;

    void Start()
    {
        ator2 = GetComponent<Animator>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pc.TakeDmg();
        }
    }
}

