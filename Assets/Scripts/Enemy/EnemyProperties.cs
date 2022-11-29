using UnityEngine;

namespace Enemy {
	public class EnemyProperties : MonoBehaviour {
		[SerializeField] public float speed;
		[SerializeField] public float damage;
		[SerializeField] public int followDistance;
	}	
}