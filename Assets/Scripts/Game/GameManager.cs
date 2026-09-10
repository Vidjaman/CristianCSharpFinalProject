using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

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
    public EnemySpawnConditions[] enemyList;
    [SerializeField] GameObject textPrefab;
    public float spawnDelay=1;

    public List<GameObject> spawnAbleEnemies;
    [SerializeField] int enemyCap;
    public int enemyCount;
   
    [SerializeField] public LayerMask enemy;
    [SerializeField] float spawnRadius;
    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnDelay);
        if (enemyCount <= enemyCap)
        {
            GameObject Enemy = spawnAbleEnemies[UnityEngine.Random.Range(0, spawnAbleEnemies.Count)];
            Instantiate(Enemy, player.transform.position + ((Vector3)UnityEngine.Random.insideUnitCircle.normalized * spawnRadius), Quaternion.identity);
            enemyCount++;
        }
        StartCoroutine(SpawnEnemy());
    }
    public void AddRemoveEnemies(int level)
    {
        foreach(EnemySpawnConditions e in enemyList)
        {
            if (level > e.minLevel && (level < e.maxLevel ||e.maxLevel==0))
            {
                if(!spawnAbleEnemies.Contains(e.prefab)) spawnAbleEnemies.Add(e.prefab);
            } 
            else if (spawnAbleEnemies.Contains(e.prefab)) spawnAbleEnemies.Remove(e.prefab);
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
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        
    }
    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,spawnRadius);
    }
}
