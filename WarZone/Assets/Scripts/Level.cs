using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] float endDelay = 1f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<ScoreTracker>().ResetScore();
    }

    public void LoadGameOver()
    {
        StartCoroutine(GameOverDelay()); 
    }

    IEnumerator GameOverDelay()
    {
        yield return new WaitForSeconds(endDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
