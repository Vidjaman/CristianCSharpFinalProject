using System.Collections;
using UnityEngine;

public class OrbitShield : Weapon
{
    float angle=0;
    float speed;
    float orbitRadius = 3;
    protected override void Start()
    {
        angle = Random.Range(0, 361);
        speed = weaponData.speed/3;
        orbitRadius = weaponData.radius;
        base.Start();
    }
    IEnumerator ProjectileStopDelay()
    {
        yield return new WaitForSeconds(0.5f);
    }
    private void Update()
    {
        angle += Time.deltaTime * speed;
        transform.localPosition=new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0f)*orbitRadius;
        transform.eulerAngles= Vector3.zero;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null && collision.GetComponent<EnemyHurtBox>() != null)
        {
            Destroy(collision.gameObject); 
        }
        
            
        
    }
}
