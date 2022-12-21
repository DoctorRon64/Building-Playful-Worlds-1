using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUps : MonoBehaviour
{
    public int PickUpType;
    public int HealthAmount;
    public bool Collided;
    public PlayerMovement PlayerScript;

	private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PickUpType == 1)
			{
                PlayerScript.Health += HealthAmount;

            }

            Collided = true;
            gameObject.SetActive(false);
        }
    }
}
