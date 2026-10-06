using Mono.Cecil;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum UpgradeType
{
    statChange,
    heal,
    newMeleeWeapon,
    newRangedWeapon,
    newShieldWeapon,
    meleeWeaponSizeUp,
    reduceCoolDownMelee,
    reduceCoolDownRanged,



   
   
   
    
   
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
public enum Rarity
{
    COMMON = 1,
    UNCOMMON = 2,
    RARE = 3,
    EPIC=4,
    LEGENDARY = 5
}
public class LevelUpRandomizer : MonoBehaviour
{
    GameObject player;
    public GameObject meleeWeaponTemplate;
    Animator animator;
    [SerializeField] LevelUpButton[] buttons;
    [SerializeField] public GameObject rangedWeaponTemplate;
    public GameObject shieldTemplate;

    List<SO_UpgradeData> upgradeList=new List<SO_UpgradeData>();
    void SetUpgradeList()
    {
        upgradeList.Clear();
        foreach(SO_UpgradeData u in Resources.LoadAll<SO_UpgradeData>("Upgrades"))
        {
            if (u.minLevel <= player.GetComponent<PlayerStats>().Level)
            {
                upgradeList.Add(u);
            }
        }
    }
    
    SO_UpgradeData RandomWeighted()
    {
        
        float totalWeight = 0;
        foreach(SO_UpgradeData upgrade in upgradeList)
        {
            totalWeight += 1f / (int)upgrade.upgradeLevel;
        }
        float randomWeight=Random.Range(0f, totalWeight);

        SO_UpgradeData selectedUpgrade=upgradeList[0];
        float cumulativeWeight = 0;
        foreach (SO_UpgradeData upgrade in upgradeList)
        {
            cumulativeWeight += 1f / (int)upgrade.upgradeLevel;
            if (randomWeight <= cumulativeWeight)
            {
                selectedUpgrade = upgrade;
                break;
                
            }
        }
        return selectedUpgrade;

    }
    [SerializeField] GameObject unPauseButton;
    public void SetUnPauseSelected()
    {
        EventSystem.current.SetSelectedGameObject(unPauseButton);
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameManager.Instance.player;
        GameManager.Instance.pauseAnimator = animator;
    }
    public List<SO_UpgradeData> acquiredUpgrades=new List<SO_UpgradeData>();
    public void OnLevelUp()
    {
        SetUpgradeList();
        Time.timeScale = 0;
        animator.Play("LevelUp");
        List<SO_UpgradeData> exclusions = new List<SO_UpgradeData>();
       
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
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
