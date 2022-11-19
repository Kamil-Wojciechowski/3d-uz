using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private MouseButton Attack;
    [SerializeField] private MouseButton Defense;
    [SerializeField] private KeyCode altAttack;
    [SerializeField] private KeyCode altDefense;
    public int speed;

    private Rigidbody2D rb2d;
    private Vector3 startPosition;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
    void Update()
    {
        Movement();
        PlayerFaceDirection();
    }

    private void Movement()
    {
        if (Input.GetKey(moveUp))
        {
            transform.Translate(new Vector3(0, 5.0f, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(moveDown))
        {
            transform.Translate(new Vector3(0, -5.0f, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(moveRight))
        {
            transform.Translate(new Vector3(5.0f, 0, 0) * speed * Time.deltaTime);
        }
        if (Input.GetKey(moveLeft))
        {
            transform.Translate(new Vector3(-5.0f, 0, 0) * speed * Time.deltaTime);
        }
    }


    private void PlayerFaceDirection()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
            );

        transform.up = direction;
    }
}
