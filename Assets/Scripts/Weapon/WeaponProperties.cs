using Enemy;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapon {
	public class WeaponProperties : MonoBehaviour {

        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        private float damage;

		void Start()
		{
			damage = Random.Range(minDamage, maxDamage);
		}


		public float GetDamage()
		{
			return damage;
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			if(collision.gameObject.CompareTag("Enemy"))
			{
				EnemyProperties enemyProperties = collision.gameObject.GetComponent<EnemyProperties>();
				enemyProperties.SetHealth(enemyProperties.GetHealth() - damage);
			}	
		}
	}	
}