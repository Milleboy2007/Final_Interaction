using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    private Animator animator;

    public Image healthSlider;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    private static int nbVie = 3;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();

        if (healthSlider != null) {
            healthSlider.fillAmount = (float)currentHealth / maxHealth;
        }

        UpdateHearts();
    }

    public void PrendreDegats(int degats)
    {
        if (currentHealth == 0) return;

        currentHealth -= degats;

        if (currentHealth < 0) currentHealth = 0;

        Debug.Log("Aïe ! Le joueur perd " + degats + " PV. Reste : " + currentHealth);

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        healthSlider.fillAmount = (float)currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            nbVie--;
            UpdateHearts();

            if (nbVie <= 0)
            {
                Mourir();
            }
            else
            {
                animator.SetTrigger("Die");
                Invoke("Reload", 1.5f);
            }
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < nbVie)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    void Reload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Mourir()
    {
        Debug.Log("GAME OVER : Le joueur est mort !");
        nbVie = 3;
        FindAnyObjectByType<GameManager>().GameOver();
    }
}