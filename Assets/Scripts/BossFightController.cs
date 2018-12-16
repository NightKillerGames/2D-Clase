using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightController : MonoBehaviour {

    public int Health = 100;
    public float attackTime = 1f;
    public Transform[] Spawns;
    public GameObject piedra;

    private int contadorOleada=0;
    private Transform[] oleada1 = new Transform[2]; //esquinas
    private Transform[] oleada2 = new Transform[2]; //arriba
    private Transform[] oleada3 = new Transform[3]; //tres abajo
    private Transform[] oleada4 = new Transform[3]; //tres en medio
    private Transform[][] oleadas;
    private float timer;
    private Animator anim;
    private bool cast = false;
    private AudioSource _audio;
    private bool isDead = false;
    private bool start = false;

	void Start () {
        anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        oleada1 = new Transform[] {Spawns[0],Spawns[4]};
        oleada2 = new Transform[] {Spawns[1],Spawns[3]};
        oleada3 = new Transform[] {Spawns[0],Spawns[2],Spawns[4]};
        oleada4 = new Transform[] {Spawns[1],Spawns[2],Spawns[3]};
        oleadas = new Transform[][] {oleada1, oleada2, oleada3, oleada4};
    }
	public void EmpezarCombate()
    {
        start = true;
        _audio.Play();
    }
	void Update () {
        if (!start)
        {
            return;
        }
        CheckIsDead();
        if (isDead)
        {
            Death();
        }
        timer += Time.deltaTime;
        if(timer >= attackTime)
        {
            anim.SetTrigger("Castear");
            _audio.Play();
            attack();
            timer = 0;
        }
	}
    
    void attack() {
        for (int i = 0; i < oleadas[contadorOleada].Length; i++)
        {
            GameObject _piedra =  Instantiate(piedra,oleadas[contadorOleada][i].position,oleadas[contadorOleada][i].rotation);
            Debug.Log(_piedra.transform.position);
        }
        if (contadorOleada >= oleadas.Length - 1)
        {
            contadorOleada = 0;
        }
        else
        {
            contadorOleada++;
        }
    }
    public void TakeDmg(int dmg)
    {
        Health -= dmg;
    }
    void CheckIsDead()
    {
        if (Health <= 0)
        {
            isDead = true;
        }
    }
    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
