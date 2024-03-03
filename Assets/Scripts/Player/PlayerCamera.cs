using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Player {
	public class PlayerCamera : MonoBehaviourPunCallbacks {
		private GameObject playerCamera;

		private void Start() {
			this.playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
		}

		void Update() {
			if (this.photonView.IsMine) {
				Vector3 position = this.transform.position;
				this.playerCamera.transform.position = new Vector3(position.x, position.y, this.playerCamera.transform.position.z);
			}
		}
	}   
}