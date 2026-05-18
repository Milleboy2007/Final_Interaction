using UnityEngine;

[CreateAssetMenu(fileName = "PlayerForm", menuName = "Scriptable Objects/PlayerForm")]
public class PlayerForm : ScriptableObject
{
    [Header("Information")]
    public string formName;
    public Sprite formSprite;

    [Header("Collisions")]
    public Vector2 colliderSize = new Vector2(1f, 1f);
    public Vector2 colliderOffset = new Vector2(0f, 0f);

    [Header("Animation")]
    public RuntimeAnimatorController animatorController;

    [Header("Déplacement")]
    public float speed = 5f;
    public float jumpForce = 10f;

    [Header("Capacités")]
    public bool canFly;

    [Header("Combat")]
    public int degats = 10;
    public float cadenceAttaque = 0.5f;
    public bool attaqueDistante;
    public GameObject projectile;
}
