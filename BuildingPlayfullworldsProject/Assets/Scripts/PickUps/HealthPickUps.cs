using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class HealthPickUps : PickUps
{
    public int HealthAmount = 10;
    public PlayerMovement PlayerScript;

	public void Awake()
	{
		PlayerScript = FindObjectOfType<PlayerMovement>();
	}

	public override void ChechWichPickUpState()
    {
        base.ChechWichPickUpState();
        PlayerScript.Health += HealthAmount;
        Collided = true;
        gameObject.SetActive(false);
    }
}