using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    void LateUpdate()
    {
            transform.position = target.position + offset;
    }
}
