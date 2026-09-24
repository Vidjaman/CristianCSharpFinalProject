using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    statChange,

    newRangedWeapon,

    reduceCoolDownMelee,
    newMeleeWeapon,

    reduceCoolDownRanged,

    newShieldWeapon,
    heal,
    meleeWeaponSizeUp,
   
    
   
}
public enum StatType
{
    Strength,
    Speed,
    Defence
}
public class Upgrade
{
   public UpgradeType type;
    public StatType stat;

    public WeaponData newWeaponData;
    public AimMode newWeaponAimMode;


    public GameObject newWeapon;
    public Upgrade(UpgradeType t)
    {

        type = t;
        stat = (StatType)Random.Range(0,3);
        if( type== UpgradeType.newRangedWeapon||type==UpgradeType.newMeleeWeapon||type==UpgradeType.newShieldWeapon)
        {
            newWeaponData = (WeaponData)ScriptableObject.CreateInstance("WeaponData");
            newWeaponData.Randomize();
        }
        
    }
}
public class LevelUpRandomizer : MonoBehaviour
{
    GameObject player;
    public GameObject meleeWeaponTemplate;
    Animator animator;
    [SerializeField] LevelUpButton[] buttons;
    [SerializeField] public GameObject rangedWeaponTemplate;
    public GameObject shieldTemplate;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.Instance.player;
    }
    public void OnLevelUp()
    {
        Time.timeScale = 0;
        animator.Play("LevelUp");
        List<int> exclusions = new List<int>();
        
        foreach (LevelUpButton b in buttons)
        {
            UpgradeType t=UpgradeType.statChange;
            if (player.GetComponentInChildren<RangedWeapon>() == null) exclusions.Add(4);
            while (exclusions.Contains((int)t))
            {
                t= (UpgradeType)Random.Range(0, 8);
            }
            exclusions.Add((int)t);
            Upgrade newUpgrade = new Upgrade(t);
            b.upgradeToGive = newUpgrade;
            b.SetText();

        }
       
       


    }
}
