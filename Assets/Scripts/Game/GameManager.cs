using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    private string filePath;
    public static GameManager Instance;
    public GameObject player;
    public SO_Enemy[] enemyList;
    [SerializeField] GameObject textPrefab;
    public float spawnDelay=1;
    public GameObject unPauseButton;
    [SerializeField] RangedEnemy rangedEnemyTemplate;
    [SerializeField] Enemy normalEnemyTemplate;
    [SerializeField] OrbitEnemy orbitEnemyTemplate;

    public List<SO_Enemy> spawnAbleEnemies;
    [SerializeField] int enemyCap;
    public int enemyCount;
   
    [SerializeField] public LayerMask enemy;
    [SerializeField] float spawnRadius;
    [SerializeField] InputActionReference pause;

    [SerializeField] AudioClip mainSong;

    
    public float timer;
    [SerializeField] float startTimer;
    float _HighScore;
    public float GetHighScore()
    {
        if (File.Exists(filePath))
        {
            LoadScore();
        }
        return _HighScore;
    }
    public Animator winAnimator;
    bool won = false;
    [SerializeField] AudioClip winSong;
    public void Win()
    {
        won = true;
        MusicPlayer.Instance.PlayMusic(winSong,true,0.5f);
        SetHighScore(player.GetComponent<PlayerStats>().GetScore());

        if (isPaused) UnPause();
        Time.timeScale = 0;
       
        
        winAnimator.Play("Win");
        
    }
    bool isPaused = false;
    
    public Animator pauseAnimator;
    
    private void OnDisable()
    {
        pause.action.Disable();
        pause.action.performed -= OnPausePressed;
    }
    public void OnPausePressed(InputAction.CallbackContext context)
    {
        if (pauseAnimator == null) return;
        if (pauseAnimator.GetCurrentAnimatorStateInfo(0).IsName("LevelUpClose")||
            pauseAnimator.GetCurrentAnimatorStateInfo(0).IsName("LevelUp")
            )
        {
            return;
        }
        
        if (isPaused)
        {
            UnPause();
        }
        else
        {
            Pause();
        }
    }
    SaveData saveData;
    public void LoadScore()
    {
        if (File.Exists(filePath))
        {
            var saveGame = JsonUtility.FromJson<SaveData>(File.ReadAllText(filePath));
            _HighScore = saveGame.highScore;
        }
        
        
    }
    public void SaveScore()
    {
        var saveGame = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, saveGame);
    }
    public void Pause()
    {
        if (won||Time.timeScale!=1)
            return;
        EventSystem.current.SetSelectedGameObject(unPauseButton);
        isPaused = true;
        pauseAnimator.Play("Pause");
        Time.timeScale = 0;
    }
    private void Update()
    {
        if (Keyboard.current.digit1Key.wasPressedThisFrame) timer = 30;
        if (won||!started)
            return;
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Win();
        }
    }
    public void UnPause()
    {
        isPaused = false;
        if(!won)
            Time.timeScale = 1;
        pauseAnimator.Play("UnPause");
    }
    public void SetHighScore(float score)
    {
        if(score> _HighScore)
            _HighScore = score;

        saveData.highScore = _HighScore;
        SaveScore();
    }
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (enemyCount <= enemyCap)
        {
            SO_Enemy enemy= spawnAbleEnemies[UnityEngine.Random.Range(0, spawnAbleEnemies.Count)];
            if (enemy.enemyType == EnemyType.Shooting)
            {
                RangedEnemy newEnemy = Instantiate(rangedEnemyTemplate, player.transform.position + ((Vector3)UnityEngine.Random.insideUnitCircle.normalized * spawnRadius), Quaternion.identity);
                newEnemy.enemyStats = enemy;
            }
            if (enemy.enemyType == EnemyType.Chasing)
            {
                Enemy newEnemy = Instantiate(normalEnemyTemplate, player.transform.position + ((Vector3)UnityEngine.Random.insideUnitCircle.normalized * spawnRadius), Quaternion.identity);
                newEnemy.enemyStats = enemy;
            }
            if (enemy.enemyType == EnemyType.Orbiting)
            {
                Enemy newEnemy = Instantiate(orbitEnemyTemplate, player.transform.position + ((Vector3)UnityEngine.Random.insideUnitCircle.normalized * spawnRadius), Quaternion.identity);
                newEnemy.enemyStats = enemy;
            }

            enemyCount++;
        }
        StartCoroutine(SpawnEnemy());
    }
    public void AddRemoveEnemies(int level)
    {
        
        foreach(SO_Enemy e in enemyList)
        {
            if (level >= e.startLevel && (level < e.endLevel ||e.endLevel==0))
            {
                if(!spawnAbleEnemies.Contains(e)) spawnAbleEnemies.Add(e);
            } 
            else if (spawnAbleEnemies.Contains(e)) spawnAbleEnemies.Remove(e);
        }
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SpawnText(string text,Color color, Vector3 position)
    {
        TextMeshPro textMesh = Instantiate(textPrefab, position, quaternion.identity).GetComponent<TextMeshPro>();
        textMesh.text = text;
        textMesh.color = color;
    }
    void Awake()
    {
       
        filePath= Application.persistentDataPath + "/saveGame.json";
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else 
        {
            Instance.started = false;
            Destroy(gameObject);
            Instance.StopAllCoroutines();
        }
        
        
    }
    bool started;

    public void StartGame() 
    {
        StopAllCoroutines();
        if (SceneManager.GetActiveScene().name != "MainGame" ) 
            return;
        LoadScore();
        started = true;
        enemyList = Resources.LoadAll<SO_Enemy>("Enemies");
        spawnDelay = 1;
        AddRemoveEnemies(1);
        isPaused = false;
        Time.timeScale = 1;
        MusicPlayer.Instance.PlayMusic(mainSong, true,0.2f);
        timer = startTimer;
        won = false;
        player = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnEnemy());
        pause.action.Enable();
        pause.action.performed += OnPausePressed;
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,spawnRadius);
    }
}
struct SaveData
{
    public float highScore;
}
