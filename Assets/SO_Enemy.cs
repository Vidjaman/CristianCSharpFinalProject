using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_Enemy : ScriptableObject
{
    public int MaxHP;
    public string EnemyName;
    public Sprite Sprite;
    public EnemyType enemyType;

    public float Speed;
    public float expYield;
    public float shotSpeed;
    public float distanceToKeep;
    public int startLevel;
    public int endLevel;
    public Gradient bulletColorGradient;
    public float orbitSpeed;

}
