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
        if (upgradeToGive.type == UpgradeType.statChange)
        {
            text.text = "Stat Upgrade";
            switch (upgradeToGive.stat)
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
        if (upgradeToGive.type == UpgradeType.newRangedWeapon)
        {
            text.text = "New Ranged Weapon";
        }
        if (upgradeToGive.type == UpgradeType.reduceCoolDownMelee)
        {
            text.text = "Reduce Melee Weapon Cooldowns";
        }
        if (upgradeToGive.type == UpgradeType.newMeleeWeapon)
        {
            text.text = "New Melee Weapon";
        }
        if (upgradeToGive.type == UpgradeType.reduceCoolDownRanged)
        {
            text.text = "Reduce Ranged Weapon Cooldowns";
        }
        if (upgradeToGive.type == UpgradeType.newShieldWeapon)
        {
            text.text = "New Shield";
        }
    }
    PlayerStats playerStats;
    public Upgrade upgradeToGive;
    public void GiveUpgrade()
    {
        if (upgradeToGive == null) return;
        switch(upgradeToGive.type)
        {
            case UpgradeType.statChange:
                playerStats.IncreaseStat(upgradeToGive.stat, 20);
                break;
            case UpgradeType.newRangedWeapon:
                GiveNewRangedWeapon();
                break;
            case UpgradeType.reduceCoolDownMelee:
          
                foreach (MeleeWeapon m in playerStats.GetComponentsInChildren<MeleeWeapon>())
                {
                    m.coolDown *= 0.9f;
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
                    m.coolDown *= 0.8f;
                }

                break;


        }
        upgradeToGive = null;
        GetComponentInParent<Animator>().Play("LevelUpClose");
        Time.timeScale = 1;
    }
    void GiveNewRangedWeapon()
    {
        RangedWeapon newWeapon= Instantiate(GetComponentInParent<LevelUpRandomizer>().rangedWeaponTemplate, playerStats.transform).GetComponent<RangedWeapon>();
        newWeapon.weaponData = upgradeToGive.newWeaponData;
        newWeapon.color1 = Random.ColorHSV();
        newWeapon.color2 = Random.ColorHSV();
    }
    void GiveNewMeleeWeapon()
    {
        MeleeWeapon newWeapon = Instantiate(GetComponentInParent<LevelUpRandomizer>().meleeWeaponTemplate, playerStats.transform).GetComponent<MeleeWeapon>();
        newWeapon.transform.eulerAngles = new Vector3(0, 0, Random.Range(0, 361));
        newWeapon.weaponData = upgradeToGive.newWeaponData;
    }
    public void GiveNewShieldWeapon()
    {
        OrbitShield newWeapon= Instantiate(GetComponentInParent<LevelUpRandomizer>().shieldTemplate,playerStats.transform).GetComponent<OrbitShield>();
        newWeapon.weaponData = upgradeToGive.newWeaponData;
    }

}
