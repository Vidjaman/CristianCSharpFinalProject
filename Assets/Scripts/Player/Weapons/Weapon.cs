using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public float coolDown;
    [SerializeField] public float baseDamage;
    public float realDamage;
    protected PlayerStats playerStats;

    public WeaponData weaponData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   
    IEnumerator AttackAfterCooldown()
    {
        yield return new WaitForSeconds(coolDown);
        Attack();
        StartCoroutine(AttackAfterCooldown());
    }
    public virtual void Attack()
    {
        realDamage=(playerStats.Strength/100)*baseDamage;
    }
    protected virtual void Start()
    {
        coolDown = weaponData.coolDown;
        baseDamage= weaponData.baseDamage;
        if(transform.parent!=null) 
            playerStats = GetComponentInParent<PlayerStats>();
        StartCoroutine(AttackAfterCooldown());
    }

    // Update is called once per frame

}
