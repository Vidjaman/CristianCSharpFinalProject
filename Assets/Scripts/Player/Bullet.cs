using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public Vector3 direction;
    public float speed;

    public ParticleSystem particle;

    public Gradient gradient;




    private void Start()
    {
        if (particle == null) return;
        var pfxMain = particle.main;
        pfxMain.startColor = Color.white;
       
        
        ParticleSystem.ColorOverLifetimeModule colorModule = particle.colorOverLifetime;
        colorModule.color = gradient;
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
