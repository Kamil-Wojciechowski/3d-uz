using UnityEngine;

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
	}	
}