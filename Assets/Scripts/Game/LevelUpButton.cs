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
    [SerializeField] GameObject[] stars;
    // Update is called once per frame
    void SetRarityStars(SO_UpgradeData data)
    { 
        foreach (GameObject star in stars)
        {
            star.SetActive(false);
        }
        for (int i = 0; i<(int)data.upgradeLevel; i++)
        {
            stars[i].SetActive(true);
        }
    }
    public void SetText()
    {
        text.text = upgradeToGive.data.upgradeText;
        SetRarityStars(upgradeToGive.data);
        
    }
    PlayerStats playerStats;
    public Upgrade upgradeToGive;
    public void GiveUpgrade()
    {
        if (upgradeToGive == null) return;
        if (upgradeToGive.data.soundClip != null)
        {
            AudioManager.Instance.PlayClip(upgradeToGive.data.soundClip, playerStats.transform.position, 1);
        }
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
                    if (m.coolDown <= 0.1f) 
                        m.coolDown = 0.1f;
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
                        if (m.coolDown < 0) m.coolDown = 0;
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
                    if (m.transform.localScale.x > 4)
                    {
                        m.transform.localScale = Vector3.one * 4;
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
        newWeapon.weaponData.rangePierce = upgradeToGive.data.rangeWeaponPierce;
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
