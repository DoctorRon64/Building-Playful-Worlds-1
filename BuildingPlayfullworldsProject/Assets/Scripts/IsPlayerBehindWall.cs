using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPlayerBehindWall : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Target;
    public GameObject Model;
    public Material PlayerMaterial;
    public Material SeeTroughMaterial;
    public LayerMask PlayerMask;

	private void Update()
    {
        Ray ray = new Ray(Camera.transform.position, Target.transform.position - Camera.transform.position);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, PlayerMask))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Model.GetComponent<Renderer>().material = PlayerMaterial;
                
            }
            else
            {
                Model.GetComponent<Renderer>().material = SeeTroughMaterial;
            }       
        }

        Debug.DrawLine(Camera.transform.position, Target.transform.position, Color.black, 1);
    }
}
