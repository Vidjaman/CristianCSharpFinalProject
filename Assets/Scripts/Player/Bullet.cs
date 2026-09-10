using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Vector3 direction;
    public float speed;

    public ParticleSystem particle;

    public Color startColor;
    public Color endColor;
    

    
    private void Start()
    {
        if (particle == null) return;
        var pfxMain = particle.main;
        pfxMain.startColor = startColor;
       
        
        ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
        colorModule.color = new ParticleSystem.MinMaxGradient(startColor,endColor);
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
