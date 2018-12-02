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
        for (float i = 1; i >= 0; i -= (Time.deltaTime*0.5f))
        {
            fadein.color = new Color(1, 1, 1, i);
            yield return null;
        }
        Destroy(gameObject);
    }
}
