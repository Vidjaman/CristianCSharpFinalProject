using System;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeathWinAnimation : MonoBehaviour
{
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] Transform deathSprite;
    [SerializeField] Animator textAnimator;

    [SerializeField] AudioClip explosionSound;

    [SerializeField] ParticleSystem confetti;
    [SerializeField]  TextMeshProUGUI finalScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFinalScoreText()
    {
        finalScoreText.text = "Final Score: "+GameObject.FindWithTag("Player").GetComponent<PlayerStats>().GetScore();
    }
    [SerializeField] AudioClip loseSong;
    public void PlayGameOverSong()
    {
        MusicPlayer.Instance.PlayMusic(loseSong, false);
    }
    [SerializeField] AudioClip winClip;
    public void PlayConfetti()
    {
        AudioManager.Instance.PlayClip(winClip, transform.position,0.2f,UnityEngine.Random.Range(0.9f,1.1f),50);
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
        AudioManager.Instance.PlayClip(explosionSound, transform.position,1);
        Instantiate(explosionPrefab, deathSprite.position,quaternion.identity);
    }
}
