using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float Health;
    [SerializeField] float maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    PlayerStats stats;
    float InvincibilityTime;

    [SerializeField] Animator deathAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health = maxHealth;
        stats = GetComponent<PlayerStats>();
        healthText.text = $"{Health}/{maxHealth}";
    }

    // Update is called once per frame
    void Update()
    {
        if (InvincibilityTime > 0) InvincibilityTime -= Time.deltaTime;
    }
    public void OnHit(float damage)
    {
        float realDamage= damage / (stats.Defence / 100);
        if (InvincibilityTime > 0) return;
        Health -= realDamage;
        if (Health < 0) Health = 0;
        InvincibilityTime = 0.3f;
        healthText.text = $"{Mathf.Round(Health*100)/100}/{maxHealth}";
        GetComponent<PlayerStats>().ResetCombo();
        GameManager.Instance.SpawnText((Mathf.Round(realDamage * 100) / 100).ToString(), Color.yellow, transform.position);
        if (Health <= 0) Die();

    }
    void Die()
    {
        Time.timeScale = 0;
        deathAnimator.Play("Death");
    }
}
