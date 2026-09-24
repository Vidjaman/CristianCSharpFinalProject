using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;

public enum AimMode
{
    MouseAim,
    AutoAim
}
public class RangedWeapon : Weapon
{
    public AimMode aimMode;
    public bool pierce;
    public float speed;
    [SerializeField] public float range;
    

    [SerializeField] Bullet bulletPrefab;
    public Gradient bulletGradient;
    protected override void Start()
    {
        speed = weaponData.speed;
        base.Start();
    }
    
    public override void Attack()
    {
        base.Attack();
        Vector3 direction=Vector3.right;
        if (aimMode == AimMode.AutoAim)
        {
            Collider2D[] hits;
            hits = Physics2D.OverlapCircleAll(transform.position, range, GameManager.Instance.enemy);
            if (hits.Length > 0)
            {
                direction = (hits[0].transform.position - transform.position).normalized;
            }
        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            direction=(mousePos- transform.position).normalized;
        }
        Bullet bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bulletInstance.direction= direction;
        bulletInstance.speed = speed;
        bulletInstance.gradient = bulletGradient;
        bulletInstance.GetComponent<WeaponHurtBox>().damage = realDamage;
        bulletInstance.GetComponent<WeaponHurtBox>().pierce = pierce;
    }
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
