using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{
    [Header("Base enemy properties")]
    public int MaxHP;
    public string EnemyName;
    public Sprite Sprite;
    public EnemyType enemyType;

    public float Speed;
    public float expYield;

    public int startLevel;
    public int endLevel;
    [Header("Ranged enemy properties")]
    public float shotSpeed;
    public float distanceToKeep;
    
    public Gradient bulletColorGradient;
    [Header("Orbit enemy property")]
    public float orbitSpeed;

}
