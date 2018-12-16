using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public float gemas;
    public int playerHealth = 3;
    public bool gameOver = false;
    public Sprite[] corazones = new Sprite[4];
    public Image salud;
    public Text countText;
    public GameObject gameOverText;
    public GameObject victoryText;
    public GameObject pauseText;

    private bool canvasIsEnable = false;
    private GameObject canvas;
    private PlayerController pc;
    private AudioManager _audio;
    private InventoryController ic;

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
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        _audio = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        ic = GetComponent<InventoryController>();
    }

    void Update()
    {
        if (gameOver)
        {
            GameOver(true);
        }
        if (playerHealth > 3)
        {
            playerHealth = 3;
        }
        //Actualizacion de la vida
        switch (playerHealth)
        {
            case 0:
                salud.sprite = corazones[0];
                break;
            case 1:
                salud.sprite = corazones[1];
                break;
            case 2:
                salud.sprite = corazones[2];
                break;
            case 3:
                salud.sprite = corazones[3];
                break;
        }
        

        //Pausa
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseText.SetActive(false);
            }
            else if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseText.SetActive(true);
            }
        }
    }

    public void GemaCogida()
    {
        gemas ++;
        _audio.SonidoGema();
        countText.text = gemas.ToString()+"X ";
    }
    public IEnumerator GoToMenu()
    {
        gameOver = false;
        ic.VaciarInventario();
        yield return new WaitForSeconds(3);
        playerHealth = 3;
        gemas = 0;
        countText.text = "OX ";
        SceneManager.LoadScene("Menu");
        yield return new WaitForSeconds(0.001f);
        ActivarCanvas(false);
    }
    public void ActivarCanvas(bool enable)
    {
        canvas.SetActive(enable);
    }
    public void GameOver(bool isOver)
    {
        gameOver = isOver;
        gameOverText.SetActive(isOver);
        if (isOver)
        {
            _audio.Death();
            StartCoroutine(GoToMenu());
        }
    }
    public void Victoria()
    {
        victoryText.SetActive(true);
        StartCoroutine(GoToMenu());
    }
}
