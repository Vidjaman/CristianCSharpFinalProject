using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class WeaponData : ScriptableObject
{
    public float coolDown;
    public float baseDamage;
    public float speed;

    public float radius;
    public WeaponData()
    {
      

    }
    public void Randomize()
    {
        coolDown = Random.Range(0.5f, 2f);
        baseDamage = Random.Range(4, 6);
        speed = Random.Range(8, 20);
        radius = Random.Range(3, 7f);
    }
    private void OnEnable()
    {
       
    }
}
