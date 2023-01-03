using Enemy;
using Photon.Pun;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float health;
    private TextMeshProUGUI healthText;
    [SerializeField] private KeyCode moveUp;
    [SerializeField] private KeyCode moveDown;
    [SerializeField] private KeyCode moveLeft;
    [SerializeField] private KeyCode moveRight;
    [SerializeField] private MouseButton Attack;
    [SerializeField] private MouseButton Defense;
    [SerializeField] private KeyCode altAttack;
    [SerializeField] private KeyCode altDefense;
    [SerializeField] private AudioSource audioSource;

    private Animator animator;    
    public int speed;

    private Rigidbody2D rb2d;
    private Vector3 startPosition;
    private Boolean canMove = true;
    private PhotonView photonView;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponentInChildren<Animator>();
        photonView = GetComponent<PhotonView>();
        StartCoroutine(Heal());

    }
    void Update()
    {
        
        if (photonView.IsMine)
        {
            Movement();
            AttackManager();
            PlayerFaceDirection();
            damageManager();

            if (healthText != null)
            {
                healthText.text = "HP: "+Convert.ToInt32(health).ToString();
            }

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length == 0)
            {
                GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "You won!";
                StartCoroutine(delay(10));
                
            }

        }
    }
    private IEnumerator Heal() {
        while (true) {
            yield return new WaitForSeconds(2);
            if (health < 99 && canMove) {
                health++;
            }
        }
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

    private void AttackManager()
    {
        if (Input.GetMouseButtonDown((int) Attack) && canMove)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("stay"))
            {
                animator.SetBool("cli", true);
            }
            audioSource.Play();
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("back")) {
            animator.SetBool("cli", false);
        }
    }
    private IEnumerator delay(float x)
    {
        yield return new WaitForSeconds(x);

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
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
        if (photonView.IsMine)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                float enemyDamage = collision.gameObject.GetComponent<EnemyProperties>().GetDamage();
                
                if ((health -= enemyDamage) <= 0)
                {
                    health = 0;
                }
            }
        }
    }
}
