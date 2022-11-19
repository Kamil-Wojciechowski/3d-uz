using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMove : MonoBehaviour {
    //private Transform[] players;
    private Transform player;
    private Vector3 targetVector;
    private float moveSpeed = 5f;
    private float proximity = 1.2f;
    private bool doMove;
    
    void Start() {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        // this.players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update() {
        // GameObject player = this.GetClosestPlayer();
        
        Vector3 differenceVector = this.player.position - this.transform.position;
        this.doMove = Math.Abs(differenceVector.x) > this.proximity || Math.Abs(differenceVector.y) > this.proximity;
        differenceVector.Normalize();
        this.targetVector = differenceVector;
    }

    private void FixedUpdate() {
        if (this.doMove) {
            this.transform.Translate(this.targetVector * (this.moveSpeed * Time.deltaTime));
        }
    }
    

    // Do implementacji jak będzie multiplayer
    private Transform GetClosestPlayer() {
        throw new NotImplementedException();
    }
}
