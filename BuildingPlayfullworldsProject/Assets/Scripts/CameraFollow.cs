using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Transform player;
    public float smoothSpeed;
    Vector3 offSet;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        offSet = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offSet, ref velocity, smoothSpeed * Time.deltaTime);
    }
}
