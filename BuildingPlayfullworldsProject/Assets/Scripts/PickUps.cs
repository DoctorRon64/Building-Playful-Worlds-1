using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int PickUpType;
    public bool Collided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collided = true;
            gameObject.SetActive(false);
            Debug.Log("pickupgone");
        }
    }
}
