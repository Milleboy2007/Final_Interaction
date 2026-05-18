using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float rayonAttaque = 0.5f;

    private PlayerMouvement playerMouvement;
    private Animator animator;

    private float tempsProchaineAttaque = 0f;

    void Start()
    {
        playerMouvement = GetComponent<PlayerMouvement>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerForm forme = playerMouvement.GetForm();

        if (forme == null) return;

        if (Time.time >= tempsProchaineAttaque)
        {
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(0))
            {
                Attaquer(forme);
                tempsProchaineAttaque = Time.time + forme.cadenceAttaque;
            }
        }
    }

    void Attaquer(PlayerForm forme)
    {
        if (animator != null) animator.SetTrigger("Attack");

        if (forme.attaqueDistante)
        {
            if (forme.projectile != null)
            {
                GameObject nouveauProjectile = Instantiate(forme.projectile, attackPoint.position, attackPoint.rotation);
                Projectile scriptProjectile = nouveauProjectile.GetComponent<Projectile>();

                if (scriptProjectile != null) {
                    scriptProjectile.ConfigurerDegats(forme.degats);
                }
            }
        }
        else
        {
            Collider2D[] ennemisTouches = Physics2D.OverlapCircleAll(attackPoint.position, rayonAttaque, enemyLayers);

            foreach (Collider2D ennemi in ennemisTouches)
            {
                Debug.Log("J'ai frappé : " + ennemi.name);
                // ennemi.GetComponent<EnnemiScript>().PrendreDegats(forme.degats);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, rayonAttaque);
    }
}