using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Transform deathSprite;
    [SerializeField] Animator textAnimator;

    [SerializeField] AudioClip explosionSound;

    [SerializeField] ParticleSystem confetti;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayConfetti()
    {
        confetti.Play();
    }
    private void Start()
    {
        GameManager.Instance.winAnimator = GetComponent<Animator>();
    }
    [SerializeField] GameObject retryButton;
    [SerializeField] GameObject menuButton;
    public void SetRetryButtonSelected()
    {
        EventSystem.current.SetSelectedGameObject(retryButton);
    }
    public void SetMenuButtonSelected()
    {
        EventSystem.current.SetSelectedGameObject(menuButton);
    }

    public void Explode()
    {
        AudioManager.Instance.PlayClip(explosionSound, transform.position);
        Instantiate(explosionPrefab, deathSprite.position,quaternion.identity);
    }
}
