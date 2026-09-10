using UnityEngine;

public class CameraTrackPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    Vector3 position;
    // Update is called once per frame
    void Update()
    {
        
        position = GameManager.Instance.player.transform.position;
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }
}
