using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Transform deathSprite;
    [SerializeField] Animator textAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Explode()
    {
        Instantiate(explosionPrefab, deathSprite.position,quaternion.identity);
    }
}
