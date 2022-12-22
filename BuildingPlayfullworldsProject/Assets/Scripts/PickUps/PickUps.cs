using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUps : MonoBehaviour
{
    public bool Collided;

	public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChechWichPickUpState();
        }
    }

    public virtual void ChechWichPickUpState()
	{
		Collided = true;
		gameObject.SetActive(false);
	}
}