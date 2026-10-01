using TMPro;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LevelUpButton : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] TextMeshProUGUI text;
    void Start()
    {
        playerStats = GameManager.Instance.player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetText()
    {
        text.text = upgradeToGive.data.upgradeText;
        /*if (upgradeToGive.data.upgradeType == UpgradeType.statChange)
        {
            text.text = "Stat Upgrade";
            switch (upgradeToGive.data.statType)
            {
                case StatType.Strength:
                    text.text += "\nStrength";
                    break;
                case StatType.Speed:
                    text.text += "\nSpeed";
                    break;
                case StatType.Defence:
                    text.text += "\nDefence";
                    break;
            }
            
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.newRangedWeapon)
        {
            text.text = "New Ranged Weapon";
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.reduceCoolDownMelee)
        {
            text.text = "Reduce Melee\nWeapon Cooldowns";
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.newMeleeWeapon)
        {
            text.text = "New Melee Weapon";
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.reduceCoolDownRanged)
        {
            text.text = "Reduce Ranged\nWeapon Cooldowns";
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.newShieldWeapon)
        {
            text.text = "New Shield";
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.heal)
        {
            text.text = "Heal";
        }
        if (upgradeToGive.data.upgradeType == UpgradeType.meleeWeaponSizeUp)
        {
            text.text = "Increase Melee\nWeapon size";
        }*/
    }
    PlayerStats playerStats;
    public Upgrade upgradeToGive;
    public void GiveUpgrade()
    {
        if (upgradeToGive == null) return;
        switch(upgradeToGive.data.upgradeType)
        {
            case UpgradeType.statChange:
                if (upgradeToGive.data.increaseType == IncreaseType.ADDITIVE)
                    playerStats.IncreaseStat(upgradeToGive.data.statType, Mathf.RoundToInt(upgradeToGive.data.increaseAmount));
                else
                    playerStats.MultiplyStat(upgradeToGive.data.statType, Mathf.RoundToInt(upgradeToGive.data.increaseAmount));
                break;
            case UpgradeType.newRangedWeapon:
                GiveNewRangedWeapon();
                break;
            case UpgradeType.reduceCoolDownMelee:
          
                foreach (MeleeWeapon m in playerStats.GetComponentsInChildren<MeleeWeapon>())
                {
                    if (upgradeToGive.data.increaseType == IncreaseType.ADDITIVE)
                    {
                        m.coolDown -= upgradeToGive.data.increaseAmount;
                    }
                    else
                    {
                        m.coolDown *= upgradeToGive.data.increaseAmount;
                    }
                }

                break;
            case UpgradeType.newMeleeWeapon:
                GiveNewMeleeWeapon();
                break;
            case UpgradeType.newShieldWeapon:
                GiveNewShieldWeapon();
                break;
            case UpgradeType.reduceCoolDownRanged:

                foreach (RangedWeapon m in playerStats.GetComponentsInChildren<RangedWeapon>())
                {
                    if (upgradeToGive.data.increaseType == IncreaseType.ADDITIVE)
                    {
                        m.coolDown -= upgradeToGive.data.increaseAmount;
                    }
                    else
                    {
                        m.coolDown *= upgradeToGive.data.increaseAmount;
                    }
                }

                break;
            case UpgradeType.heal:
                if (upgradeToGive.data.increaseType==IncreaseType.ADDITIVE)
                {
                    playerStats.GetComponent<PlayerHealth>().HealthProp += upgradeToGive.data.increaseAmount;
                }
                else
                {
                    playerStats.GetComponent<PlayerHealth>().HealthProp *=upgradeToGive.data.increaseAmount;
                }
                
                break;
            case UpgradeType.meleeWeaponSizeUp:
                foreach (MeleeWeapon m in playerStats.GetComponentsInChildren<MeleeWeapon>()) 
                {
                    if(upgradeToGive.data.increaseType != IncreaseType.ADDITIVE)
                    {
                        m.transform.localScale *= upgradeToGive.data.increaseAmount;
                    }
                    else
                    {
                        m.transform.localScale += Vector3.one*upgradeToGive.data.increaseAmount;
                    }
                    if (m.transform.localScale.x > 6)
                    {
                        m.transform.localScale = Vector3.one * 6;
                    }
                    
                }
                break;
            default:
                break;
                


        }
        GetComponentInParent<LevelUpRandomizer>().acquiredUpgrades .Add( upgradeToGive.data);
        upgradeToGive = null;
        
        GetComponentInParent<Animator>().Play("LevelUpClose");
        Time.timeScale = 1;
        playerStats.XP = 0;
    }
    void GiveNewRangedWeapon()
    {
        RangedWeapon newWeapon= Instantiate(GetComponentInParent<LevelUpRandomizer>().rangedWeaponTemplate, playerStats.transform).GetComponent<RangedWeapon>();
        newWeapon.weaponData = upgradeToGive.newWeaponData;
        Color color1 = Random.ColorHSV();
        Color color2 = Random.ColorHSV();
        var colors = new GradientColorKey[2];
        colors[0] = new GradientColorKey(color1,0);
        colors[1] = new GradientColorKey(color2, 1);
        Gradient newGradient = new Gradient();
        newGradient.SetColorKeys(colors);
        newWeapon.bulletGradient = newGradient;
    }
    void GiveNewMeleeWeapon()
    {
        MeleeWeapon newWeapon = Instantiate(GetComponentInParent<LevelUpRandomizer>().meleeWeaponTemplate, playerStats.transform).GetComponent<MeleeWeapon>();
        newWeapon.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 361));
        newWeapon.weaponData = upgradeToGive.newWeaponData;
    }
    public void GiveNewShieldWeapon()
    {
        OrbitShield newWeapon= Instantiate(GetComponentInParent<LevelUpRandomizer>().shieldTemplate,playerStats.transform.position,Quaternion.identity).GetComponent<OrbitShield>();
        newWeapon.weaponData = upgradeToGive.newWeaponData;
    }

}
