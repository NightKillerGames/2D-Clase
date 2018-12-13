using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    private static AudioManager instance = null;
    private static PlayerController pc;
    public AudioClip gema;
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
}
