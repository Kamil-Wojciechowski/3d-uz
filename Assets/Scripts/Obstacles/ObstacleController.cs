using Photon.Pun;
using UnityEngine;

namespace Obstacles {
    public class ObstacleController : MonoBehaviourPun, IPunObservable {

        private int collisions = 1;
    
        private void OnCollisionEnter2D(Collision2D collision) {
            if (!PhotonNetwork.IsMasterClient) return;
        
            if (collision.gameObject.CompareTag("Enemy"))
            {
                this.collisions--;
                if (this.collisions < 0) {
                    PhotonNetwork.Destroy(this.gameObject);   
                }
            }
        }
    
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
            if (stream.IsWriting) {
                stream.SendNext(this.transform.position);
            }
            else {
                this.transform.position = (Vector3)stream.ReceiveNext();
            }
        }
    }
}
