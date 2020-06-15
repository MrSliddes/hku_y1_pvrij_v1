using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(Animatie());
    }

    public IEnumerator Animatie()
    {
        yield return new WaitForSeconds(14);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield break;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT GAME");
        Application.Quit();
    }
}
