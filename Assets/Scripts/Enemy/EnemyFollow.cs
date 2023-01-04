using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Unity.Mathematics;
using UnityEngine;

namespace Enemy {
    public class EnemyFollow : MonoBehaviourPun, IPunObservable {
        private List<GameObject> players;
        private EnemyProperties properties;

        private void Start() {
            this.properties = this.GetComponent<EnemyProperties>();
        }

        private void Update() {
            PlayerController[] controllers = FindObjectsOfType<PlayerController>();
            this.players = (from controller in controllers where controller.canMove select controller.gameObject).ToList();
            this.Follow();
        }

        private void Follow() {
            if (this.players.Count == 0 || this.properties.GetIsDead() || !PhotonNetwork.IsMasterClient) return;
            
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

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                stream.SendNext(this.transform.position);
                stream.SendNext(this.transform.rotation);
            }
            else {
                this.transform.position = (Vector3)stream.ReceiveNext();
                this.transform.rotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}
