using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Player {
	public class PlayerSpawner : MonoBehaviourPunCallbacks {
		[SerializeField] private Transform container;
		[SerializeField] private GameObject player;
    
		void Start() {
			PhotonNetwork.ConnectUsingSettings();
		}

		public override void OnConnectedToMaster() {
			base.OnConnectedToMaster();
			PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions(), TypedLobby.Default);
		}

		public override void OnJoinedRoom() {
			base.OnJoinedRoom();
			GameObject player = PhotonNetwork.Instantiate(this.player.name, Vector3.zero, Quaternion.identity);
			player.transform.parent = this.container;
		}
	}    
}