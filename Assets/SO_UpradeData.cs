using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;

    public StatType statType;

    public IncreaseType increaseType;
    public float increaseAmount;

    public string upgradeText;

    public SO_UpgradeData dependancy;

    public float minWeaponCooldown;
    public float maxWeaponCooldown;
    public float minWeaponAttack;
    public float maxWeaponAttack;
    public float minWeaponSpeed;
    public float maxWeaponSpeed;
    public float minWeaponRadius;
    public float maxWeaponRadius;

}
