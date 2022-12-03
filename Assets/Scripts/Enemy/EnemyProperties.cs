using System;
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
        [SerializeField] public int followDistance;

		void Start()
		{
			damage = Random.Range(minDamage, maxDamage);	
			health = Random.Range(minHealth, maxHealth);
		}

		void Update()
		{
			if(health <= 0)
			{
				isDead = true;
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

		public Boolean GetIsDead()
		{
			return isDead;
		} 

		void OnCollisionEnter2D(Collision2D collision)
		{
			if(collision.gameObject.tag == "Weapon")
			{
				float playerDamage = collision.gameObject.GetComponent<WeaponProperties>().GetDamage();
				health -= playerDamage;
				Debug.Log(health);
			}	
		}
	}	
}