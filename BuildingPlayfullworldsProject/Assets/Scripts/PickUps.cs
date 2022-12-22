using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickUps : MonoBehaviour
{
    public int PickUpType;
    public bool Collided;

	public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("yobama");
            ChechWichPickUpState();
        }
    }

    public virtual void ChechWichPickUpState()
	{
        if (PickUpType == 0)
		{
            //do nothing
            Debug.Log("do notthing");
            Collided = true;
            gameObject.SetActive(false);
        }
	}
}

public class HealthPickUps : PickUps
{
    public int HealthAmount = 10;
    public PlayerMovement PlayerScript;

	public override void ChechWichPickUpState()
	{
        Debug.Log("joebiden");
        base.ChechWichPickUpState();
        Debug.Log("joebiden");
        if (PickUpType == 1)
		{
            PlayerScript.Health += HealthAmount;
            Collided = true;
            gameObject.SetActive(false);
        }
	}
}

public class LevelFinishPickup : PickUps
{
    public GameObject WinScreen;

	private void Start()
	{
		WinScreen.SetActive(false);
	}

    public override void ChechWichPickUpState()
    {
        Debug.Log("joebiden2");
        base.ChechWichPickUpState();
        Debug.Log("joebiden2");
        if (PickUpType == 2)
        {
		    WinScreen.SetActive(true);
            Invoke("ToNextScene", 5);
            Collided = true;
            gameObject.SetActive(false);
        }
    }

    private void ToNextScene()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
