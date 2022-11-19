using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using FixedUpdate = Unity.VisualScripting.FixedUpdate;

public class OpponentScript : MonoBehaviour
{
    private GameObject player;
    private int proximity = 1;
    private float moveSpeed = 5;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate() {
        Vector3 playerPosition = this.player.transform.position;
        Vector3 currentPosition = this.transform.position;

        if (Math.Abs(currentPosition.x - playerPosition.x) > this.proximity) {
            Vector3 move = Vector3.right * (this.isBigger(currentPosition.x, playerPosition.x) * Time.deltaTime * this.moveSpeed);
            this.transform.Translate(move);
        }

        if (Math.Abs(currentPosition.y - playerPosition.y) > this.proximity) {
            Vector3 move = Vector3.up * (this.isBigger(currentPosition.y, playerPosition.y) * Time.deltaTime * this.moveSpeed);
            this.transform.Translate(move);
        }
    }

    private float isBigger(float thisPosition, float targetPosition) {
        return thisPosition < targetPosition ? 1 : -1;
    }
}
