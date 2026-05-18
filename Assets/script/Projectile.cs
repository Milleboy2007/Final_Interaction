using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Réglages")]
    public float vitesse = 12f;
    public float tempsDeVie = 3f;

    private Rigidbody2D rb;
    private int degats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * vitesse;
        Destroy(gameObject, tempsDeVie);
    }

    public void ConfigurerDegats(int montantDegats)
    {
        degats = montantDegats;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Ennemi touché ! Dégâts infligés : " + degats);
            // collision.GetComponent<EnemyHealth>().PrendreDegats(degats);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}