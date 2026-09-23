using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable] public struct EnemySpawnConditions
{
    public int minLevel;
    public int maxLevel;
    public GameObject prefab;
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public SO_Enemy[] enemyList;
    [SerializeField] GameObject textPrefab;
    public float spawnDelay=1;

    [SerializeField] RangedEnemy rangedEnemyTemplate;
    [SerializeField] Enemy normalEnemyTemplate;

    public List<SO_Enemy> spawnAbleEnemies;
    [SerializeField] int enemyCap;
    public int enemyCount;
   
    [SerializeField] public LayerMask enemy;
    [SerializeField] float spawnRadius;

    float _HighScore;
    public float GetHighScore()
    {
        return _HighScore;
    }
    public void SetHighScore(float score)
    {
        if(score> _HighScore) _HighScore = score;
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

            enemyCount++;
        }
        StartCoroutine(SpawnEnemy());
    }
    public void AddRemoveEnemies(int level)
    {
        
        foreach(SO_Enemy e in enemyList)
        {
            if (level > e.startLevel && (level < e.endLevel ||e.endLevel==0))
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
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else Destroy(gameObject);
        
        
    }

    public void StartGame() 
    {
        if (SceneManager.GetActiveScene().name != "MainGame" ) 
            return;
        enemyList = Resources.LoadAll<SO_Enemy>("Enemies");
        AddRemoveEnemies(1);
        player = GameObject.FindWithTag("Player");
        StartCoroutine(SpawnEnemy());
    }
    

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,spawnRadius);
    }
}
