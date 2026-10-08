using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        AudioManager.Instance.clipsPlaying.Remove(GetComponent<AudioSource>().clip);
    }
}
