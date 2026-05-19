using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyForm forme;

    private int pointsDeVieActuels;
    private int direction = 1; // -1 = gauche, 1 = droite
    private bool estMort = false;
    private float coolDownAttack = 0f;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D hitbox;

    private Transform player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        hitbox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;    
        }

        if (forme != null)
        {
            pointsDeVieActuels = forme.pointsDeVieMax;
            if (spriteRenderer != null) spriteRenderer.sprite = forme.spriteBase;
            if (animator != null) animator.runtimeAnimatorController = forme.animatorController;

            if (hitbox != null)
            {
                hitbox.size = forme.colliderSize;
                hitbox.offset = forme.colliderOffset;
            }
        }
        else
        {
            Debug.LogError("Il manque la fiche EnemyForm sur " + gameObject.name);
        }
    }

    void Update()
    {
        if (estMort) return;

        if (player == null) 
        {
            Patrouiller();
            return;
        }

        float distWithPlayer = Vector2.Distance(transform.position, player.position);

        rb.linearVelocity = new Vector2(direction * forme.vitessePatrouille, rb.linearVelocity.y);

        if (distWithPlayer <= forme.rayonAttaque)
        {
            Attack();
        }
        else if (distWithPlayer <= forme.rayonDetection)
        {
            Follow();
        }
        else 
        {
            Patrouiller();
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
        }
    }

    void Patrouiller() {
        rb.linearVelocity = new Vector2(direction * forme.vitessePatrouille, rb.linearVelocity.y);
    }

    void Attack() {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

        if (Time.time >= coolDownAttack)
        {
            if (animator != null) animator.SetTrigger("Attack");

            Debug.Log(forme.nomEnnemi + " attaque le joueur !");

            player.GetComponent<PlayerHealth>().PrendreDegats(forme.degatsAttaque);

            coolDownAttack = Time.time + forme.cadenceAttaque;
        }
    }

    void Follow() {
        CheckPlayer(player.position.x);
        rb.linearVelocity = new Vector2(direction * forme.vitessePatrouille, rb.linearVelocity.y);
    }

    void CheckPlayer(float posXPlayer) {
        if (posXPlayer > transform.position.x && direction == -1)
        {
            FaireDemiTour();
        }
        else if (posXPlayer < transform.position.x && direction == 1) {
            FaireDemiTour();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            FaireDemiTour();
        }
    }

    void FaireDemiTour()
    {
        direction *= -1;

        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;
        transform.localScale = scale;
    }

    public void PrendreDegats(int degats)
    {
        if (estMort) return;

        pointsDeVieActuels -= degats;
        Debug.Log(forme.nomEnnemi + " prend " + degats + " dégâts ! PV restants : " + pointsDeVieActuels);

        if (pointsDeVieActuels <= 0)
        {
            Mourir();
        } else if (animator != null) animator.SetTrigger("Hurt");
    }

    void Mourir()
    {
        estMort = true;
        rb.linearVelocity = Vector2.zero;
        rb.gravityScale = 0f;
        hitbox.enabled = false;
        
        if (animator != null) animator.SetTrigger("Die");
        Debug.Log(forme.nomEnnemi + " a été vaincu !");
        Destroy(gameObject, 10f);
    }

    void OnDrawGizmosSelected()
    {
        if (forme == null) return;

        // Rayon de détection (Jaune)
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, forme.rayonDetection);

        // Rayon d'attaque (Rouge)
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, forme.rayonAttaque);
    }
}