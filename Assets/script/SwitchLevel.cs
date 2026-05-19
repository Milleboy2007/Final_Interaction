using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
    public string nomDeLaSceneSuivante;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Chargement de la scène : " + nomDeLaSceneSuivante);
            SceneManager.LoadScene(nomDeLaSceneSuivante);
        }
    }
}
