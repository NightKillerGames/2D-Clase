using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Image fadein;
    private GameManager gm;

    private void Start()
    {
        StartCoroutine("FadeImage");
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gm.GameOver(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gm.ActivarCanvas(true);
    }

    public void QuitGame()
    {
        Debug.Log("APLICACION CERRADA");
        Application.Quit();
    }
    IEnumerator FadeImage()
    {
        fadein.CrossFadeAlpha(0, 3, true);
        yield return new WaitForSeconds(3);
        Destroy(fadein.gameObject);
    }
}
