using UnityEngine;

public class CollectableVie : MonoBehaviour
{
    public int healtAmount = 25;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth scriptHealth = collision.GetComponent<PlayerHealth>();

            if (scriptHealth != null)
            {
                if (scriptHealth.GetStateHealth())
                {
                    scriptHealth.AddHealth(healtAmount);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Vie déjà au maximum, pack laissé au sol.");
                }
            }
        }
    }
}