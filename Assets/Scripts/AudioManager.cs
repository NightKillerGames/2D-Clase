using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance = null;
    private static PlayerController pc;

    public AudioClip gema;
    public AudioClip MuerteEnemigo;
    public AudioClip puño;
    public AudioClip lanzarbellota;
    public AudioClip inpactobellota;
    public AudioClip salto;
    public AudioClip MuertePlayer;
    public AudioClip DmgPlayer;

    public AudioSource audiomain;
    public AudioSource audioEfectos;
    public AudioSource audioPlayer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    public void SonidoGema()
    {
        audioEfectos.clip = gema;
        audioEfectos.Play();
    }
    public void MuerteEnemy()
    {
        audiomain.clip = MuerteEnemigo;
        audiomain.Play();
    }
    public void punch ()
    {
        audioPlayer.clip = puño;
        audioPlayer.Play();
    }
    public void bellota()
    {
        audioPlayer.clip = lanzarbellota;
        audioPlayer.Play();
    }
    public void jump()
    {
        audiomain.clip = salto;
        audiomain.Play();
    }
    public void Death()
    {
        audiomain.clip = MuertePlayer;
        audiomain.Play();
    }
    public void Dmgoof()
    {
        audioPlayer.clip = DmgPlayer;
        audioPlayer.Play();
    }
    public void InpactoBellota()
    {
        audioEfectos.clip = inpactobellota;
        audioEfectos.Play();
    }
}
