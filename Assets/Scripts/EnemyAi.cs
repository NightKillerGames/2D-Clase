using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public int health = 100;
    private Animator ator2;
    private PlayerController pc;
    private bool muerto = false;

    void Start()
    {
        ator2 = GetComponent<Animator>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        
    }
    private void Update()
    {
        ator2.SetBool("Muerto", muerto);
        if (health <= 0)
            {
                muerto = true;
                Invoke("Destruir", 1);
            }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pc.TakeDmg();
        }
    }
    private void Destruir()
    {
        Destroy(gameObject);
    }
}

