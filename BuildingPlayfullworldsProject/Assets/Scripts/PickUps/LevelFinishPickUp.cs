using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class LevelFinishPickUp : PickUps
{
	public GameObject WinScreen;
	public GameObject LoseScreen;
	public PlayerMovement PlayerScript;

	private void Awake()
	{
		WinScreen.SetActive(false);
		LoseScreen.SetActive(false);
		PlayerScript = FindObjectOfType<PlayerMovement>();
	}

	private void Update()
	{
		LoseScene();
	}

	public override void ChechWichPickUpState()
	{
		base.ChechWichPickUpState();
		WinScreen.SetActive(true);
		gameObject.SetActive(false);
		Collided = true;
	}

	private void LoseScene()
	{
		if (PlayerScript.Health <= 0)
		{
			LoseScreen.SetActive(true);
		}
	}
	public void ToNextScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void RestartingScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
	}
}
