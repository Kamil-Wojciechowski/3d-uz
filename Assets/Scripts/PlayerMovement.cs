using Enemy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float health;
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
    private Boolean canMove = true;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
    }
    void Update()
    {
        Movement();
        PlayerFaceDirection();
        damageManager();
    }

    private void Movement()
    {
        if(canMove)
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
        
    }


    private void PlayerFaceDirection()
    {
        if(canMove)
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

    private void damageManager()
    {
        if(health <= 0)
        {
            canMove = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            float enemyDamage = collision.gameObject.GetComponent<EnemyProperties>().GetDamage();
            health -= enemyDamage;
        }    
    }
}
