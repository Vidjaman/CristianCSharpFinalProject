using Mono.Cecil;
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
public enum IncreaseType
{
    ADDITIVE,
    MULTIPLICATIVE
}
public class Upgrade
{
   

    public WeaponData newWeaponData;
    public AimMode newWeaponAimMode;
  
  

    public SO_UpgradeData data;


    public GameObject newWeapon;
    public Upgrade(SO_UpgradeData newData)
    {

        data = newData;

        if(data.upgradeType== UpgradeType.newRangedWeapon|| data.upgradeType == UpgradeType.newMeleeWeapon|| data.upgradeType == UpgradeType.newShieldWeapon)
        {
            newWeaponData = (WeaponData)ScriptableObject.CreateInstance("WeaponData");
            newWeaponData.Randomize(newData);
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

    
    SO_UpgradeData RandomWeighted()
    {
        SO_UpgradeData[] upgradeList = Resources.LoadAll<SO_UpgradeData>("Upgrades");
        float totalWeight = 0;
        foreach(SO_UpgradeData upgrade in upgradeList)
        {
            totalWeight += 1 / upgrade.upgradeLevel;
        }
        float randomWeight=Random.Range(0, totalWeight);

        SO_UpgradeData selectedUpgrade=upgradeList[0];
        float cumulativeWeight = 0;
        foreach (SO_UpgradeData upgrade in upgradeList)
        {
            cumulativeWeight += 1 / upgrade.upgradeLevel;
            if (randomWeight <= cumulativeWeight)
            {
                selectedUpgrade = upgrade;
                break;
                
            }
        }
        return selectedUpgrade;

    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.Instance.player;
        GameManager.Instance.pauseAnimator=animator;
    }
    public List<SO_UpgradeData> acquiredUpgrades=new List<SO_UpgradeData>();
    public void OnLevelUp()
    {
        Time.timeScale = 0;
        animator.Play("LevelUp");
        List<SO_UpgradeData> exclusions = new List<SO_UpgradeData>();
        SO_UpgradeData[] upgradeList= Resources.LoadAll<SO_UpgradeData>("Upgrades");
        
        foreach (LevelUpButton b in buttons)
        {
            bool acceptable = false;
            SO_UpgradeData t;
            t = RandomWeighted();

            while (acceptable==false)
            {
                t = RandomWeighted();
                if (exclusions.Contains(t))
                    continue;

                if (t.dependancy != null)
                {
                    if (!acquiredUpgrades.Contains(t.dependancy))
                        continue;   
                }
               
                
                acceptable = true;
            }
            exclusions.Add(t);
            Upgrade newUpgrade = new Upgrade(t);
            b.upgradeToGive = newUpgrade;
            b.SetText();

        }
       
       


    }
}
