using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelFinishPickUp : PickUps
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
		WinScreen.SetActive(true);
		Invoke("ToNextScene", 5);
		Collided = true;
		gameObject.SetActive(false);
	}

	private void ToNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
