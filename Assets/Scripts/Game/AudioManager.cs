using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource soundPrefab;
    public static AudioManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        else if (Instance!=this) 
            Destroy(gameObject);
    }
    public int PlayingCount(AudioClip clip)
    {
        int count = 0;
        foreach (AudioClip c in clipsPlaying)
        {
            if (c == clip)
                count++;
        }
        return count;
    }
    public List<AudioClip> clipsPlaying = new List<AudioClip>();
    public void PlayClip(AudioClip clip,Vector3 position,int maxCount)
    {
        if (PlayingCount(clip) > maxCount) return;
        AudioSource source = Instantiate(soundPrefab, position, Quaternion.identity);
        source.clip = clip;
        source.Play();
        clipsPlaying.Add(clip);
        Destroy(source.gameObject, clip.length);
    }
    public void PlayClip(AudioClip clip, Vector3 position,float volume,float pitch, int maxCount)
    {
        if (PlayingCount(clip) > maxCount) return;
        AudioSource source = Instantiate(soundPrefab, position, Quaternion.identity);
        source.clip = clip;
        source.pitch= pitch;
        source.volume = volume;
        clipsPlaying.Add(clip);
        source.Play();
        Destroy(source.gameObject, clip.length);
    }
    public void PlayClip(AudioClip clip, Vector3 position, float volume, int maxCount)
    {
        if (PlayingCount(clip) > maxCount) return;
        AudioSource source = Instantiate(soundPrefab, position, Quaternion.identity);
        source.clip = clip;
        source.volume = volume;
        clipsPlaying.Add(clip);
        source.Play();
        Destroy(source.gameObject, clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
