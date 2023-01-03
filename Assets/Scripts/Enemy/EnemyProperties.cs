using System;
using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;
using Random = UnityEngine.Random;

namespace Enemy {
	public class EnemyProperties : MonoBehaviour, IPunObservable {
		[SerializeField] public float speed;
        [SerializeField] private float minHealth;
        [SerializeField] private float maxHealth;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        [SerializeField] private Slider slider;
        private float damage;
		private float health;
        private float setHealth;
        private Boolean isDead = false;
		private Boolean isCounted = false;
		private TextMeshProUGUI scoreText;
        [SerializeField] public float minimumFollowDistance;
        [SerializeField] public float maximumFollowDistance;
        [SerializeField] private AudioSource audioSource;


        void Start()
		{
			damage = Random.Range(minDamage, maxDamage);	
			health = Random.Range(minHealth, maxHealth);
			setHealth = health;
            scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
			slider.value = CalculateHealth();
		}

		void Update()
		{
            slider.value = CalculateHealth();

            if (isDead)
			{
				if(!isCounted)
				{
                    if (scoreText != null)
					{
                        int score = Convert.ToInt32(scoreText.text.Substring(6));
						Debug.Log(score);
						scoreText.text = "Score: " + (score+1).ToString();
						isCounted = true;
					}
				}
            } else
			{
                if (health <= 0)
                {
                    this.damage = 0;
                    this.speed = 0;
                    isDead = true;
					audioSource.Play();
                    StartCoroutine(delay(10));
                    this.gameObject.SetActive(false);
                }
            }
			
		}

		public float GetDamage()
		{
			return damage;
		}

		public float GetHealth()
		{
			return health;
		}

		public void SetHealth(float health)
		{
			this.health = health;
		}

		public Boolean GetIsDead()
		{
			return isDead;
		}

        private float CalculateHealth()
		{
			return health / setHealth;
		}

        private IEnumerator delay(float x)
        {
            yield return new WaitForSeconds(x);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
	        if (stream.IsWriting) {
		        stream.SendNext(this.isDead);
		        stream.SendNext(this.health);
		        stream.SendNext(this.setHealth);
				stream.SendNext(this.damage);
	        }
	        else {
		        this.isDead = (bool)stream.ReceiveNext();
		        this.health = (float)stream.ReceiveNext();
		        this.setHealth = (float)stream.ReceiveNext();
				this.damage = (float)stream.ReceiveNext();
	        }
        }
	}
}