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
    public AudioClip salto;
    public AudioClip MuertePlayer;



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
        audioPlayer.clip = MuerteEnemigo;
        audioPlayer.Play();
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
        audioPlayer.clip = salto;
        audioPlayer.Play();
    }
    public void Death()
    {
        audioEfectos.clip = MuertePlayer;
        audioEfectos.Play();
    }
}
