using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttackSystem : MonoBehaviour
{
	public float HitCooldownValue;
    public float HitCooldown;
    public int AttackDamage;
	public Animator AnimatorController;

	public void Update()
	{
		InputSystem();
	}

	private void TickingTimer()
	{
		if (HitCooldown > 0)
		{
			HitCooldown -= Time.deltaTime;
		}
	}

	private void InputSystem()
	{
		if (Input.GetKey(KeyCode.E))
		{
            if (HitCooldownValue < 0)
			{
				Attack();
                HitCooldown = HitCooldownValue;
				TickingTimer();
            }
		}
	}

	private void Attack()
	{
		Debug.Log("hitted");
		//AnimatorController.Play(" ");
	}

}
