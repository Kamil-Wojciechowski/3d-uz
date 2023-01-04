using Enemy;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapon;

public class PlayerController : MonoBehaviourPun
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
    private List<GameObject> players;

    private Rigidbody2D rb2d;
    private Vector3 startPosition;
    public Boolean canMove = true;
    
    private Color cAlive = Color.black;
    private Color cDead = Color.cyan;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        healthText = GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponentInChildren<Animator>();
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
            PlayerController[] controllers = FindObjectsOfType<PlayerController>();
            this.players = (from controller in controllers where controller.canMove select controller.gameObject).ToList();
            if (players.Count == 0) {
                GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "You lost!";
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

        PhotonNetwork.Disconnect();
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
            this.photonView.RPC("SetPlayerColor", RpcTarget.AllBuffered, true);
            canMove = false;
            GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = "You're dead!";
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
            if (collision.gameObject.CompareTag("Player") && !canMove) {
                this.photonView.RPC("SetPlayerColor", RpcTarget.AllBuffered, false);
                health = 1;
                canMove = true;
                GameObject.Find("WinText").GetComponent<TextMeshProUGUI>().text = " ";
            }
        }
    }

    [PunRPC]
    private void SetPlayerColor(bool isDead) {
        this.photonView.gameObject.GetComponent<SpriteRenderer>().color = isDead ? this.cDead : this.cAlive;
    }
}
