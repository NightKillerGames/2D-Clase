using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public Vector2 InitialPosition;
    public Vector2 FinalPosition;

    private float _currentPatrolTime = 0;
    private bool _direction = true; //derecha
    public float Health = 100;
    public float Speed = 5;

    private Animator ator2;
    private PlayerController pc;
    private bool muerto = false;

    void Start()
    {
        ator2 = GetComponent<Animator>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    protected virtual void Move()
    {
        gameObject.transform.position = Vector2.Lerp(InitialPosition, FinalPosition, _currentPatrolTime);
    }

    public void Update()
    {
        ator2.SetBool("Muerto", muerto);
        Move();
        CheckIsDead();
        Patrol();
    }

    protected virtual void Patrol()
    {
        if (_direction)
        {
            _currentPatrolTime += Time.deltaTime * Speed;
        }
        else
        {
            _currentPatrolTime -= Time.deltaTime * Speed;
        }
        if (_currentPatrolTime >= 1)
        {
            _currentPatrolTime = 1;
            _direction = !_direction;
        }
        else if (_currentPatrolTime <= 0)
        {
            _currentPatrolTime = 0;
            _direction = !_direction;
        }
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;

    }
    private void Die()
    {
        Destroy(gameObject);
    }

    private void CheckIsDead()
    {
        if (Health <= 0)
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
