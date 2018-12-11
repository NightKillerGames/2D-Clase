using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public float gemas;
    public Sprite[] corazones = new Sprite[4];
    public Image salud;
    public Text countText;
    public GameObject gameOverText;
    public GameObject pauseText;
    private GameObject canvas;
    private PlayerController pc;
    public int playerHealth = 3;
    public bool gameOver = false;
    private bool canvasIsEnable = false;

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
	}

    void Update()
    {
        if (gameOver)
        {
            GameOver(true);
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
        countText.text = gemas.ToString()+"X ";
    }
    public IEnumerator GoToMenu()
    {
        gameOver = false;
        yield return new WaitForSeconds(1);
        playerHealth = 3;
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
        Debug.Log("Muerto");
        gameOver = isOver;
        gameOverText.SetActive(isOver);
        if(isOver)
            StartCoroutine(GoToMenu());
    }
}
