using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public void GameOver() {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    }
}
