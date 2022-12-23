using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public float SmoothSpeed;
    private Vector3 offSetCamera;
    private Vector3 velocityCamera = Vector3.zero;
    private PostProcessVolume PostProsVol;

	private void Awake()
	{
		PostProsVol = GetComponent<PostProcessVolume>();
        PostProsVol.isGlobal = true;
	}


	private void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        offSetCamera = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offSetCamera, ref velocityCamera, SmoothSpeed * Time.deltaTime);
    }
}
