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
    [SerializeField] AudioClip deathClip;
    [SerializeField] AudioClip hurtClip;

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
        float textDamage = MathF.Round(damage * 10);
        GameManager.Instance.SpawnText((textDamage).ToString(), Color.red, transform.position);
        if (Health <= 0)
        {
            AudioManager.Instance.PlayClip(deathClip, transform.position, 0.2f,6);
            GameObject boom = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            boom.transform.localScale = Vector3.one * 0.5f;
            Destroy(gameObject);
            
            Player.GetComponent<PlayerStats>().OnEnemyDeath(expYield);
        }
        else
        {
            AudioManager.Instance.PlayClip(hurtClip, transform.position,0.2f, 10);
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
        if (Vector3.Distance(transform.position, Player.transform.position) > 150)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnDestroy()
    {   
        GameManager.Instance.enemyCount--;
    }
}
