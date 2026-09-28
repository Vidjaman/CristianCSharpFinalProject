using System.Collections;
using UnityEngine;

public class OrbitShield : Weapon
{
    float angle=0;
    float speed;
    float orbitRadius = 3;
    Transform player;
    PlayerMovement pm;
    protected override void Start()
    {
        player = GameManager.Instance.player.transform;
        angle = Random.Range(0, 361);
        speed = weaponData.speed/3;
        orbitRadius = weaponData.radius;
        playerStats = player.GetComponent<PlayerStats>();
        pm = player.GetComponent<PlayerMovement>();

        base.Start();
    }
   
    private void Update()
    {
        angle += Time.deltaTime * speed*pm.GetDirection();
        transform.position=player.position+new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0f)*orbitRadius;
        transform.eulerAngles= Vector3.zero;
        realDamage = (playerStats.Strength / 100) * baseDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Bullet>() != null && collision.GetComponent<EnemyHurtBox>() != null)
        {
            Destroy(collision.gameObject); 
        }
        
            
        
    }
}
