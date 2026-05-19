using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    [Header("Form: ")]
    public PlayerForm formeMelee;
    public PlayerForm formeDist;
    public PlayerForm formeVol;

    private PlayerForm formeActuelle;
    private Animator animator;

    private BoxCollider2D boxCollider;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float moveX;
    private float moveY;
    private bool isGrounded = true;

    public GameObject effetFumeePrefab;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        ChangerForme(formeMelee);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) ChangerForme(formeMelee);
        if (Input.GetKeyDown(KeyCode.Alpha2)) ChangerForme(formeDist);
        if (Input.GetKeyDown(KeyCode.Alpha3)) ChangerForme(formeVol);

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (formeActuelle.canFly)
        {
            if (moveX > 0)
            {
                transform.localScale = new Vector3(-3, 3, 3);
            }
            else if (moveX < 0)
            {
                transform.localScale = new Vector3(3, 3, 3);
            }
        }
        else {
            if (moveX > 0)
            {
                transform.localScale = new Vector3(3, 3, 3);
            }
            else if (moveX < 0)
            {
                transform.localScale = new Vector3(-3, 3, 3);
            }
        }

        if (animator != null) {
            animator.SetFloat("Speed", Mathf.Abs(moveX));
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("VeloY", rb.linearVelocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !formeActuelle.canFly)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, formeActuelle.jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (formeActuelle.canFly)
        {
            Vector2 directionVol = new Vector2(moveX, moveY);
            directionVol = directionVol.normalized; 
            rb.linearVelocity = directionVol * formeActuelle.speed;
        }
        else
        {
            rb.linearVelocity = new Vector2(moveX * formeActuelle.speed, rb.linearVelocity.y);
        }
    }

    void ChangerForme(PlayerForm nouvelleForme)
    {
        formeActuelle = nouvelleForme;

        if (effetFumeePrefab != null)
        {
            Instantiate(effetFumeePrefab, transform.position, Quaternion.identity);
        }

        if (spriteRenderer != null && formeActuelle.formSprite != null)
        {
            spriteRenderer.sprite = formeActuelle.formSprite;
        }

        if (formeActuelle.canFly)
        {
            rb.gravityScale = 0f;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.gravityScale = 3f;
        }

        if (boxCollider != null)
        {
            boxCollider.size = formeActuelle.colliderSize;
            boxCollider.offset = formeActuelle.colliderOffset;
        }

        if (animator != null && formeActuelle.animatorController != null) {
            animator.runtimeAnimatorController = formeActuelle.animatorController;
        }
    }

    public PlayerForm GetForm() {
        return formeActuelle;
    }
}