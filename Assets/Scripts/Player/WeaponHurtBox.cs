using UnityEngine;

public class WeaponHurtBox : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public float damage;
    public bool pierce=true;

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<Weapon>()!=null ) damage = GetComponent<Weapon>().realDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            collision.GetComponent<Enemy>().Damage(damage);
            if(!pierce) Destroy(gameObject);
        }
    }
}
