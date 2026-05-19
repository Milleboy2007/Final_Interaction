using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public GameObject[] quiteBTNs;

    void Start()
    {
        #if UNITY_WEBGL
        foreach (GameObject btn in quiteBTNs)
        {
            if (btn != null) btn.SetActive(false);
        }
        #endif
    }

    public void GameOver() {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ReprendreGame()
    {
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public void Quite()
    {
        Debug.Log("Game Quite");
        Application.Quit();
    }

    public void Rejouer() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
