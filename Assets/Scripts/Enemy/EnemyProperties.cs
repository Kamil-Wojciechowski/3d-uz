using System;
using TMPro;
using UnityEngine;
using Weapon;
using Random = UnityEngine.Random;

namespace Enemy {
	public class EnemyProperties : MonoBehaviour {
		[SerializeField] public float speed;
        [SerializeField] private float minHealth;
        [SerializeField] private float maxHealth;
        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        private float damage;
		private float health;
		private Boolean isDead = false;
		private Boolean isCounted = false;
		private TextMeshProUGUI scoreText;
        [SerializeField] public int followDistance;

		void Start()
		{
			damage = Random.Range(minDamage, maxDamage);	
			health = Random.Range(minHealth, maxHealth);
			scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
		}

		void Update()
		{
			if(isDead)
			{
				Debug.Log("Enemy Dead v2");
                if(!isCounted)
				{
                    Debug.Log("Is not counted");
                    if (scoreText != null)
					{
                        Debug.Log("text is not null");
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
					Debug.Log("Enemy Dead");
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
	}	
}