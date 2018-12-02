using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image fadein;
    private void Start()
    {
        StartCoroutine("FadeImage");
    }
    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1);
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
