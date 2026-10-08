using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    AudioSource source;
    public static MusicPlayer Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
    }
    public void StopMusic()
    {
        source.Stop();
    }
    public void PlayMusic(AudioClip song,bool loop)
    {
        source.volume = 1;
        source.clip = song;
        source.loop = loop;
        source.Play();
    }
    public void PlayMusic(AudioClip song, bool loop,float volume)
    {
        source.volume= volume;
        source.clip = song;
        source.loop = loop;
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
