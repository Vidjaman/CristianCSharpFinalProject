using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference Move;
    Vector2 moveValue;
    PlayerStats stats;
    [SerializeField] float speed;
    int direction;
    public int GetDirection()
    {
        return direction;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        GameManager.Instance.StartGame();
        
    }
    private void OnEnable()
    {
        Move.action.Enable();
    }
    private void OnDisable()
    {
        Move.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        speed = 10 * (stats.Speed / 100);
        moveValue=Move.action.ReadValue<Vector2>();
        if (moveValue.x != 0&&Time.timeScale!=0) direction = Math.Sign(moveValue.x);
        transform.position += (Vector3)moveValue * speed * Time.deltaTime;
        if (transform.position.x > 425)
        {
            transform.position = new Vector3(425, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -425)
        {
            transform.position = new Vector3(-425, transform.position.y, transform.position.z);
        }
        if (transform.position.y > 425)
        {
            transform.position = new Vector3(transform.position.x, 425, transform.position.z);
        }
        if (transform.position.y < -425)
        {
            transform.position = new Vector3(transform.position.x, -425, transform.position.z);
        }
       
        if (direction == 1)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);

        }
        else transform.eulerAngles = new Vector3(0, 180, 0);
    }
    
}
