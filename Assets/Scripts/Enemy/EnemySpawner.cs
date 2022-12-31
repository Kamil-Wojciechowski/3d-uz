using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy {
    public class EnemySpawner : MonoBehaviourPunCallbacks {
        [SerializeField] private Transform container;
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private int enemyCount = 20;
        [SerializeField] private float spawnRadius = 15f;

        public override void OnCreatedRoom() {
            base.OnCreatedRoom();
            for (int i = 0; i < this.enemyCount; i++) {
                this.SpawnEnemy();
            }            
        }

        private void SpawnEnemy() {
            Vector3 position = this.GetRandomPosition();
            GameObject enemy = PhotonNetwork.Instantiate(this.enemyPrefab.name, position,
                Quaternion.FromToRotation(Vector3.up, (this.transform.position - position)));
            enemy.transform.parent = this.container;
        }

        private Vector3 GetRandomPosition() {
            Vector3 position = Random.insideUnitCircle;
            position *= this.spawnRadius;
            position += this.container.position;

            return position;
        }
    }
}
