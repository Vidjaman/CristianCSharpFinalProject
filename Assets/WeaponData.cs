using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class WeaponData : ScriptableObject
{
    public float coolDown;
    public float baseDamage;
    public float speed;
    public bool rangePierce;

    public float radius;
    public WeaponData()
    {
      

    }
    public void Randomize(SO_UpgradeData data)
    {
        coolDown = Random.Range(data.minWeaponCooldown,data.maxWeaponCooldown);
        baseDamage = Random.Range(data.minWeaponAttack,data.maxWeaponAttack);
        speed = Random.Range(data.minWeaponSpeed, data.maxWeaponSpeed);
        radius = Random.Range(data.minWeaponRadius, data.maxWeaponRadius);
    }
    private void OnEnable()
    {
       
    }
}
