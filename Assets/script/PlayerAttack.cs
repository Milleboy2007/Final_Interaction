using UnityEngine;
using TMPro;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public float rayonAttaque = 0.5f;

    private PlayerMouvement playerMouvement;
    private Animator animator;

    private float tempsProchaineAttaque = 0f;
    public int maxMunition = 20;
    public int currentMunitions = 5;
    public TextMeshProUGUI texteMunitions;

    void Start()
    {
        playerMouvement = GetComponent<PlayerMouvement>();
        animator = GetComponent<Animator>();
        UpdateMunitionText();
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
            if (currentMunitions > 0)
            {
                if (forme.projectile != null)
                {
                    currentMunitions--;
                    UpdateMunitionText();
                    GameObject nouveauProjectile = Instantiate(forme.projectile, attackPoint.position, attackPoint.rotation);
                    Projectile scriptProjectile = nouveauProjectile.GetComponent<Projectile>();

                    if (scriptProjectile != null)
                    {
                        scriptProjectile.ConfigurerDegats(forme.degats, Mathf.Sign(transform.localScale.x));
                    }
                } else Debug.Log("Projectil Null");
            } else Debug.Log("A court de munition!");
        }
        else
        {
            Collider2D[] objetsTouches = Physics2D.OverlapCircleAll(attackPoint.position, rayonAttaque);
            foreach (Collider2D objet in objetsTouches)
            {
                if (objet.CompareTag("Enemy"))
                {
                    Enemy script = objet.GetComponent<Enemy>();

                    if (script != null)
                    {
                        script.PrendreDegats(forme.degats);
                    }
                }
            }
        }
    }

    public void AddMunition(int amount) {
        currentMunitions += amount;

        if (currentMunitions > maxMunition) currentMunitions = maxMunition;

        UpdateMunitionText();
    }

    void UpdateMunitionText()
    {
        if (texteMunitions != null)
        {
            texteMunitions.text = currentMunitions.ToString();
        }
    }

    public bool GetStateMunition()
    {
        return currentMunitions < maxMunition && currentMunitions > 0;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, rayonAttaque);
    }
}