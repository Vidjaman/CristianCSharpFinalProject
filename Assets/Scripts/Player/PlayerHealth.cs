using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float _Health;

    [SerializeField] AudioClip hurtClip;
    [SerializeField] AudioClip bigHurtClip;

    public float HealthProp
    {
        get { return _Health; }
        set { _Health = value; 
        if(_Health>maxHealth ) 
                _Health = maxHealth;
            healthText.text = $"{Mathf.Round(_Health * 10) / 10}/{maxHealth}";
        }
    }

    [SerializeField] float maxHealth;
    [SerializeField] TextMeshProUGUI healthText;
    PlayerStats stats;
    float InvincibilityTime;

    [SerializeField] Animator deathAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        HealthProp = maxHealth;
        stats = GetComponent<PlayerStats>();
        healthText.text = $"{_Health}/{maxHealth}";
    }

    // Update is called once per frame
    void Update()
    {
        if (InvincibilityTime > 0) InvincibilityTime -= Time.deltaTime;
    }
    public void OnHit(float damage)
    {
        float realDamage= damage / (stats.Defence / 100);
        if (InvincibilityTime > 0) 
            return;
       
        Camera.main.GetComponent<CameraTrackPlayer>().shakeTime = 0.2f;
        HealthProp -= realDamage;
        InvincibilityTime = 0.3f;
        if(_Health>0) 
            AudioManager.Instance.PlayClip(hurtClip, transform.position,1 ,Random.Range(0.9f, 1.1f),5);
        GetComponent<PlayerStats>().ResetCombo();
        GameManager.Instance.SpawnText((Mathf.Round(realDamage * 10) / 10).ToString(), Color.yellow, transform.position);
        if (_Health <= 0) Die();
        

    }
    void Die()
    {
        MusicPlayer.Instance.StopMusic();
        AudioManager.Instance.PlayClip(bigHurtClip, transform.position,1);
        GameManager.Instance.SetHighScore(GetComponent<PlayerStats>().GetScore());
        _Health = 0;
        Time.timeScale = 0;
        deathAnimator.Play("Death");
    }
    
}
