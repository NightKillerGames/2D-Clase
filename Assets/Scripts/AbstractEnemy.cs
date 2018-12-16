using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    public Vector2 InitialPosition;
    public Vector2 FinalPosition;

    protected float _currentPatrolTime = 0;
    protected bool _direction = true; //derecha

    public float Health = 100;
    public float Speed = 5;
    public GameObject muerte;
    private AudioManager audio;

    protected Animator ator2;
    protected PlayerController pc;
    protected bool muerto = false;

    void Start()
    {
        ator2 = GetComponent<Animator>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    public virtual void Move()
    {
        gameObject.transform.position = Vector2.Lerp(InitialPosition, FinalPosition, _currentPatrolTime);
    }

    public virtual void Update()
    {
        CheckIsDead();
        ator2.SetBool("Muerto", muerto);
        if (muerto)
        {
            audio.MuerteEnemy();
            return;
        }
        Move();
        Patrol();
    }

    public virtual void Patrol()
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
            transform.rotation = Quaternion.Euler(new Vector3(0, -360, 0));
            _currentPatrolTime = 1;
            _direction = !_direction;
        }
        else if (_currentPatrolTime <= 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            _currentPatrolTime = 0;
            _direction = !_direction;
        }
    }
    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    protected void CheckIsDead()
    {
        if (Health <= 0 && !muerto)
        {
            muerto = true;
            GameObject explosion = Instantiate(muerte, transform.position, transform.rotation);
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