using UnityEngine;

[CreateAssetMenu(fileName = "SO_Enemy", menuName = "Scriptable Objects/SO_Enemy")]
public class SO_UpgradeData : ScriptableObject
{
    [Header("Base properties")]
    public UpgradeType upgradeType;

    public string upgradeText;

    public int minLevel;
    public SO_UpgradeData dependancy;

    public Rarity upgradeLevel;

    [Header("Number Change properties")]
    public StatType statType;

    public IncreaseType increaseType;
    public float increaseAmount;

    

    [Header("New Weapon Properties")]
    public float minWeaponCooldown;
    public float maxWeaponCooldown;
    public float minWeaponAttack;
    public float maxWeaponAttack;
    public float minWeaponSpeed;
    public float maxWeaponSpeed;
    public float minWeaponRadius;
    public float maxWeaponRadius;

}
