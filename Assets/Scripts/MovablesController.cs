using Enemy;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovablesController : MonoBehaviour
{
    // Start is called before the first frame update
    private int collisions = 1;
    private Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collisions--;
            if (collisions < 0) {
                gameObject.SetActive(false);   
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.AddForce(rb.velocity);
        }
    }
}
