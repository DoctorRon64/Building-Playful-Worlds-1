using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerBehindWall : MonoBehaviour
{
    public GameObject cam;
    public GameObject player;
    public GameObject model;
    public Material PlayerMaterial;
    public Material SeeTroughMaterial;
    public float LookingRadius;

	private void Update()
    {
        
        if (Physics.CapsuleCast(cam.transform.position, player.transform.position, LookingRadius, transform.forward))
        {
            model.GetComponent<Renderer>().material = PlayerMaterial;
        }
        else
        {
            model.GetComponent<Renderer>().material = SeeTroughMaterial;
        }

        Debug.DrawLine(cam.transform.position, player.transform.position, Color.black, 1);
    }
}
