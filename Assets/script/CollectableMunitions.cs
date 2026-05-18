using UnityEngine;

public class CollectableMunitions : MonoBehaviour
{
    public int quantiteDonnee = 5;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerAttack scriptAttaque = collision.GetComponent<PlayerAttack>();

            if (scriptAttaque != null)
            {
                scriptAttaque.AddMunition(quantiteDonnee);
                Destroy(gameObject);
            }
        }
    }
}