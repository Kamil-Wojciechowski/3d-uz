using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Enemy {
    public class EnemyFollow : MonoBehaviour {
        private GameObject[] players;
        private EnemyProperties properties;

        private void Start() {
            this.properties = this.GetComponent<EnemyProperties>();
        }

        private void Update() {
            this.players = GameObject.FindGameObjectsWithTag("Player");
            this.Follow();
        }

        private void Follow() {
            if (this.players.Length == 0) return;
            
            Vector3 currentPosition = this.transform.position;
            GameObject followPlayer = this.players[0];
            float distance = this.calculateDistance(followPlayer);
            
            foreach (GameObject player in this.players) {
                if (player.Equals(followPlayer)) continue;
                
                float compareDistance = this.calculateDistance(player);
                if (distance > compareDistance) {
                    distance = compareDistance;
                    followPlayer = player;
                }
            }

            Vector3 playerPosition = followPlayer.transform.position;
            Vector2 direction = playerPosition - currentPosition;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (distance > this.properties.minimumFollowDistance && distance < this.properties.maximumFollowDistance) {
                this.transform.position = Vector2.MoveTowards(currentPosition, playerPosition,
                    this.properties.speed * Time.deltaTime);
                this.transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            }
        }

        private float calculateDistance(GameObject player) {
            return Vector2.Distance(player.transform.position, this.transform.position);
        }
    }
}