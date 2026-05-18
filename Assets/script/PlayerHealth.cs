using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLife = 100;
    private int actualLife;

    private Animator animator;

    void Start()
    {
        actualLife = maxLife;
        animator = GetComponent<Animator>();
    }

    public void PrendreDegats(int degats)
    {
        actualLife -= degats;

        Debug.Log("Aïe ! Le joueur perd " + degats + " PV. Reste : " + actualLife);

        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (actualLife <= 0)
        {
            Mourir();
        }
    }

    void Mourir()
    {
        Debug.Log("GAME OVER : Le joueur est mort !");
    }
}