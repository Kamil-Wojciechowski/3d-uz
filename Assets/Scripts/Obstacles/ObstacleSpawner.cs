using Photon.Pun;
using UnityEngine;
using Random = System.Random;

namespace Obstacles {
    public class ObstacleSpawner : MonoBehaviourPunCallbacks {
        [SerializeField] private GameObject[] prefabs;
    
        private void OnDrawGizmos() {
#if UNITY_EDITOR
            foreach (Transform obstacleTransform in this.GetComponentsInChildren<Transform>()) {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(obstacleTransform.position, Vector3.one);
            }        
#endif
        }
        
        public override void OnCreatedRoom() {
            Transform[] transforms = this.GetComponentsInChildren<Transform>();
            Random random = new Random();
            foreach (Transform obstacleTransform in transforms) {
                int index = random.Next(0, this.prefabs.Length);
                GameObject obstacle = PhotonNetwork.InstantiateRoomObject(this.prefabs[index].name, obstacleTransform.position,
                    obstacleTransform.rotation);
                obstacle.transform.parent = obstacleTransform;
            }
        }
    }
}
