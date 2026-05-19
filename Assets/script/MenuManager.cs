using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    private string themeToLoad;

    public Image fondFantasy;
    public Image fondGangster;

    public Color couleurSelec = Color.green;
    public Color couleurNormal = Color.white;

    void Start() {
        SetFantasy();
    }

    public void SetFantasy() {
        themeToLoad = "F-Level1";
        ChangerCouleurBoutons(true);
    }

    public void SetGangster() {
        themeToLoad = "G-Level1";
        ChangerCouleurBoutons(false);
    }
    private void ChangerCouleurBoutons(bool estFantasySelectionne)
    {
        if (fondFantasy != null)
        {
            fondFantasy.color = estFantasySelectionne ? couleurSelec : couleurNormal;
        }

        if (fondGangster != null)
        {
            fondGangster.color = estFantasySelectionne ? couleurNormal : couleurSelec;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(themeToLoad);
    }
}
