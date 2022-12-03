using UnityEngine;

namespace Enemy {
    public class EnemyFollow : MonoBehaviour
    {
        private GameObject player;
        private EnemyProperties properties;
        private float minimumDistance;

        // Start is called before the first frame update
        private void Start()
        {
            this.player = GameObject.FindGameObjectWithTag("Player");
            this.properties = this.GetComponent<EnemyProperties>();
            this.minimumDistance = this.player.transform.localScale.x * 1.4f;
        }

        private void Update() {
            this.Follow();
        }

        private void Follow() {
            Vector3 currentPosition = this.transform.position;
            Vector3 playerPosition = this.player.transform.position;
        
            float distance = Vector2.Distance(currentPosition, playerPosition);
            Vector2 direction = (playerPosition - currentPosition);
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance > this.minimumDistance && distance < this.properties.followDistance) {
                this.transform.position = Vector2.MoveTowards(currentPosition, playerPosition, this.properties.speed * Time.deltaTime);
                this.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }
    }
}
