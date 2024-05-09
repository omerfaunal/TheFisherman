using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance
    void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void GameOver(bool win)
    {
        // Pause the game
        Time.timeScale = 0f;

        if(win) {
           
        } else {

        }

        //gameOverScreen.SetActive(true);

        // MuteAllSounds();
    }

    void MuteAllSounds()
    {
        // AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        // foreach (AudioSource source in audioSources)
        // {
        //     source.volume = 0f;
        // }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

