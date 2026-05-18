using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Réglages")]
    public float vitesse = 12f;
    public float tempsDeVie = 3f;

    private Rigidbody2D rb;
    private int degats;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, tempsDeVie);
    }

    public void ConfigurerDegats(int montantDegats, float direction)
    {
        degats = montantDegats;

        rb.linearVelocity = new Vector2(direction * vitesse, 0);
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Debug.Log("Ennemi touché ! Dégâts infligés : " + degats);
            collision.GetComponent<Enemy>().PrendreDegats(degats);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}