using Enemy;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapon {
	public class WeaponProperties : MonoBehaviour {

        [SerializeField] private float minDamage;
        [SerializeField] private float maxDamage;
        private float damage;
		private Animator animator;

		void Start()
		{
			damage = Random.Range(minDamage, maxDamage);
			animator = GetComponent<Animator>();
		}

		public float GetDamage()
		{
			return damage;
		}

		void OnTriggerEnter2D(Collider2D collision)
		{
            if (collision.gameObject.CompareTag("Enemy") && animator.GetCurrentAnimatorStateInfo(0).IsName("use"))
            {
                Debug.Log("HIT");
                EnemyProperties enemyProperties = collision.gameObject.GetComponent<EnemyProperties>();
                enemyProperties.SetHealth(enemyProperties.GetHealth() - damage);
            }
        }
	}	
}