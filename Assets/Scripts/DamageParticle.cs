using UnityEngine;

public class DamageParticle : MonoBehaviour
{
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * 20, ForceMode2D.Impulse);
    }
    public void End()
    {
        Destroy(gameObject);
    }
}
