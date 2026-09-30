using System;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float Health=10;
    protected GameObject Player;
    [SerializeField] protected float speed;
    Rigidbody2D rb;
    Animator animator;
    public SO_Enemy enemyStats;

    public float expYield;
    [SerializeField] GameObject explosionPrefab;
    protected Vector3 targetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Start()
    {
        Player=GameManager.Instance.player;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = enemyStats.Speed;
        expYield = enemyStats.expYield;
        Health = enemyStats.MaxHP;
        GetComponent<SpriteRenderer>().sprite = enemyStats.Sprite;

    }
    Vector3 DirectionTowardsTarget()
    {
        return (targetPosition- transform.position).normalized;
    }
    public void Damage(float damage)
    {
        Health -= damage;
        GameManager.Instance.SpawnText((damage*10).ToString(), Color.red, transform.position);
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
    bool isQuitting = false;
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    private void OnDestroy()
    {
        if (isQuitting) return;
        GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        boom.transform.localScale= Vector3.one*0.5f;
        GameManager.Instance.enemyCount--;
    }
}
