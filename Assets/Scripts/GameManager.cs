using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public Sprite[] corazones = new Sprite[4];
    public Image salud;
    private PlayerController pc;

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
	
	void Update () {
        switch (pc.playerHealth)
        {
            case 0:
                salud.sprite = corazones[0];
                Debug.Log("case 0");
                break;
            case 1:
                salud.sprite = corazones[1];
                Debug.Log("case 1");
                break;
            case 2:
                salud.sprite = corazones[2];
                Debug.Log("case 2");
                break;
            case 3:
                salud.sprite = corazones[3];
                Debug.Log("case 3");
                break;
        }
	}

}
