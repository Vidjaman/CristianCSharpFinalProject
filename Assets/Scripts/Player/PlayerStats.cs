using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float Strength;
    public float Speed;
    public float Defence;

   


    [SerializeField] TextMeshProUGUI strText;
    [SerializeField] TextMeshProUGUI defText;
    [SerializeField] TextMeshProUGUI spdText;

    [SerializeField] TextMeshProUGUI levelText;


    [SerializeField] float EXP;
    public int Level;
    [SerializeField] LevelUpRandomizer levelUpManager;
    [SerializeField] Slider levelSlider;

    [SerializeField] float baseScoreGain;
    [SerializeField] float maxComboTimer;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;

    [SerializeField] int maxLevel;
    [SerializeField] float Score;
    int Combo;
    float comboTimer;
    public float GetScore()
    {
        return Score;
    }
    public float comboMultiplier;
    public void OnEnemyDeath(float expYield)
    {
       
        Combo++;
        Score += baseScoreGain * (Combo*comboMultiplier);
        comboTimer = maxComboTimer;
        if(Level<maxLevel) 
            XP += expYield;
        UpdateScoreText();
        
    }
    void UpdateScoreText()
    {
        comboText.text = "Combo: " + Combo;
        scoreText.text = "Score: " + Score.ToString("G10");
    }
    public void ResetCombo()
    {
        Combo = 0;
        UpdateScoreText();
    }
    [SerializeField] ParticleSystem confetti;
    public float XP
    {

        get { return EXP; }
        set { EXP = value;
            levelSlider.value = EXP / XPUntillNextLevel;
            if (EXP >= XPUntillNextLevel) LevelUp();
            if (Level >= maxLevel) levelSlider.value = 1;
        }
    }
    [SerializeField] AudioClip levelUpSound;
    int maxLevelToReduceCooldown = 40;
    void LevelUp()
    {
        confetti.Play();
        AudioManager.Instance.PlayClip(levelUpSound, transform.position, 0.3f,1);
        Level++;
        XPUntillNextLevel = 100 + (10 * Level * Level);
        levelUpManager.OnLevelUp();
        IncreaseStat(StatType.Strength, 10);
        IncreaseStat(StatType.Defence, 10);
        IncreaseStat(StatType.Speed, 10);
        if(Level<=maxLevelToReduceCooldown)
            GameManager.Instance.spawnDelay *= 0.9f;
        GameManager.Instance.AddRemoveEnemies(Level);
        levelSlider.value = 2;
        if (Level == maxLevel)
            levelText.text = "MAX LEVEL";
        else
            levelText.text = "Level " + Level;


    }
    [SerializeField] float XPUntillNextLevel;
    public void IncreaseStat(StatType type,int value)
    {
        switch (type)
        {
            case StatType.Strength:
                Strength += value;
                strText.text = "STR: " + Strength;
                break;
            case StatType.Speed:
                Speed += value;
                spdText.text = "SPD: " + Speed;
                break;
            case StatType.Defence:
                Defence += value;
                defText.text = "DEF: " + Defence;
                break;
        }
    }
    public void MultiplyStat(StatType type, int value)
    {
        switch (type)
        {
            case StatType.Strength:
                Strength *= value;
                strText.text = "STR: " + Strength;
                break;
            case StatType.Speed:
                Speed *= value;
                spdText.text = "SPD: " + Speed;
                break;
            case StatType.Defence:
                Defence *= value;
                defText.text = "DEF: " + Defence;
                break;
        }
    }
    private void Start()
    {
        XPUntillNextLevel = 100;
    }
    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LevelUp();
        }
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0) ResetCombo();
        }
    }

}
