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
    public void PlayClip(AudioClip clip,Vector3 position)
    {
        AudioSource source = Instantiate(soundPrefab, position, Quaternion.identity);
        source.clip = clip;
        source.Play();
        Destroy(source.gameObject, clip.length);
    }
    public void PlayClip(AudioClip clip, Vector3 position,float volume,float pitch)
    {
        AudioSource source = Instantiate(soundPrefab, position, Quaternion.identity);
        source.clip = clip;
        source.pitch= pitch;
        source.volume = volume;
        source.Play();
        Destroy(source.gameObject, clip.length);
    }
    public void PlayClip(AudioClip clip, Vector3 position, float volume)
    {
        AudioSource source = Instantiate(soundPrefab, position, Quaternion.identity);
        source.clip = clip;
        source.volume = volume;
        source.Play();
        Destroy(source.gameObject, clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
