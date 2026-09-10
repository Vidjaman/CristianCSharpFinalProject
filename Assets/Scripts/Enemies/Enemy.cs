using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float Health=10;
    protected GameObject Player;
    [SerializeField] float speed;
    Rigidbody2D rb;
    Animator animator;

    public float expYield;

    protected Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        Player=GameManager.Instance.player;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    Vector3 DirectionTowardsTarget()
    {
        return (targetPosition- transform.position).normalized;
    }
    public void Damage(float damage)
    {
        Health -= damage;
        GameManager.Instance.SpawnText(damage.ToString(), Color.red, transform.position);
        if (Health <= 0)
        {
            Destroy(gameObject);
            Player.GetComponent<PlayerStats>().OnEnemyDeath(expYield);
        }

      
        animator.Play("EnemyHurt",0);
    }
    public virtual void SetTargetPosition()
    {
        targetPosition = Player.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        SetTargetPosition();
        rb.linearVelocity= DirectionTowardsTarget() * speed;
    }
    private void OnDestroy()
    {
        GameManager.Instance.enemyCount--;
    }
}
