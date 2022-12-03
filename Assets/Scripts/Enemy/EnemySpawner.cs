using UnityEngine;

namespace Enemy {
    public class EnemySpawner : MonoBehaviour {
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemyCount = 20;
        [SerializeField] private float spawnRadius = 15f;

        private void Start() {
            for (int i = 0; i < this.enemyCount; i++) {
                this.SpawnEnemy();
            }
        }

        private void SpawnEnemy() {
            Vector3 position = this.GetRandomPosition();
            Instantiate(this.enemyPrefab,
                position,
                Quaternion.FromToRotation(Vector3.up, (this.transform.position - position)),
                this.gameObject.transform
            );
        }

        private Vector3 GetRandomPosition() {
            Vector3 position = Random.insideUnitCircle;
            position *= this.spawnRadius;
            position += this.transform.position;

            return position;
        }
    }
}
