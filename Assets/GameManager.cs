using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance
    public GameObject gameOverPanel;
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
        gameOverPanel.SetActive(true);
        if(win)
        {
            gameOverPanel.GetComponentInChildren<TextMeshProUGUI>().text = "A Real Survivor!";
        } else {
            gameOverPanel.GetComponent<Image>().color = Color.gray;
            gameOverPanel.GetComponentInChildren<TextMeshProUGUI>().text = "AAAHHHH \n You are dead!";
        }
        

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
        Debug.Log("Quiting game");
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}

