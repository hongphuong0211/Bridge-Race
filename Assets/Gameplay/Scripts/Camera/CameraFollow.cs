using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTransform;
    public Vector3 offset;
    private Vector3 smoothPos;
    private Transform camTransform;
    public float smoothSpeed = 0.5f;

    private void Start()
    {
        camTransform = transform;
    }
    void LateUpdate()
    { 
        smoothPos = Vector3.Lerp(camTransform.position, followTransform.position + offset, smoothSpeed);
        camTransform.position = smoothPos;
        //camTransform.position = Vector3.right * camX + Vector3.up * camY + camTransform.position.z * Vector3.forward;
    }
}
