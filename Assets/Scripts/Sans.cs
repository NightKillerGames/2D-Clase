using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sans : MonoBehaviour {

    public AudioSource audioSans;
    public AudioSource audioGeneral;
    private bool isFirstTime=true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isFirstTime)
            {
                audioGeneral.Stop();
                audioSans.Play();
                isFirstTime = false;
            }
           
        }
    }
}
