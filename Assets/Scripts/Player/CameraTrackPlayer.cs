using Unity.VisualScripting;
using UnityEngine;

public class CameraTrackPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    Vector3 position;
    Vector3 shakeOffset;
    public float shakeTime;
    [SerializeField] float shakeIntensity;
    // Update is called once per frame
    void Update()
    {
        
        position = GameManager.Instance.player.transform.position;
        if(shakeTime>0)
        {
            shakeTime -= Time.unscaledDeltaTime;
            shakeOffset = Random.insideUnitSphere*shakeIntensity;
        }
        transform.position = new Vector3(position.x+shakeOffset.x, position.y+shakeOffset.y, transform.position.z);
    }
}
