using UnityEngine;

public class OrbitEnemy : Enemy
{

    public float angle=0;
    float distance=20;

    float rotateSpeed=70;


    public override void Start()
    {
        rotateSpeed = enemyStats.orbitSpeed;
        angle = Random.Range(0, 360f);
        base.Start();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void SetTargetPosition()
    {
        
    }
    private void Update()
    {
        angle += rotateSpeed * Time.deltaTime;
        if (distance > 0) distance -= speed/2 * Time.deltaTime;
        Vector2 relativePos = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        Vector3 pos = Player.transform.position + (Vector3)relativePos * distance;
        if (distance < 13)
        {
            transform.position = Vector3.MoveTowards(transform.position, pos, speed * 2 * Time.deltaTime);
        }
        else
            transform.position =pos;
    }
    // Update is called once per frame

}
