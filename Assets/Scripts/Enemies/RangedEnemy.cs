using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] float distanceToKeep;
    [SerializeField] GameObject shotPrefab;
    [SerializeField] float shotSpeed;
  
    public override void SetTargetPosition()           
    {
        Vector3 vectorTowardsPlayer=(transform.position-Player.transform.position).normalized;
        targetPosition = Player.transform.position + (vectorTowardsPlayer * distanceToKeep);
    }
   
    void OnBecameVisible()
    {
        StartCoroutine(Shoot());
    }
    private void OnBecameInvisible()
    {
        StopAllCoroutines();
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2);
        GameObject shot = Instantiate(shotPrefab,transform.position,Quaternion.identity);
        shot.GetComponent<Bullet>().direction = (Player.transform.position - transform.position).normalized;
        shot.GetComponent<Bullet>().speed = shotSpeed;
        StartCoroutine(Shoot());
    }
}
