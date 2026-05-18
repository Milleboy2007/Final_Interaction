using UnityEngine;

[CreateAssetMenu(fileName = "EnemyForm", menuName = "Scriptable Objects/EnemyForm")]
public class EnemyForm : ScriptableObject
{
    [Header("Visuels & Infos")]
    public string nomEnnemi;
    public Sprite spriteBase;
    public RuntimeAnimatorController animatorController;

    [Header("Statistiques")]
    public int pointsDeVieMax = 30;
    public float vitessePatrouille = 2f;

    [Header("Collisions")]
    public Vector2 colliderSize = new Vector2(1f, 1f);
    public Vector2 colliderOffset = new Vector2(0f, 0f);

    [Header("Combat")]
    public float rayonDetection = 6f;
    public float rayonAttaque = 1.2f;
    public int degatsAttaque = 10;
    public float cadenceAttaque = 1.5f;
}
