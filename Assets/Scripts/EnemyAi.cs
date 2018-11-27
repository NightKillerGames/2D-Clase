using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour
{
    public int health = 100;
    private Animator ator2;


    void Start()
    {
        ator2 = GetComponent<Animator>();
    }



     public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

