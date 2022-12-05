using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttackSystem : MonoBehaviour
{
	public float HitCooldownValue;
    public float HitCooldown = 2;
    public int AttackDamage;
	public Animator AnimatorController;
	public GameObject SwordModel;

	private void Awake()
	{
		SwordModel.SetActive(false);
	}

	public void Update()
	{
		InputSystem();

		if (HitCooldownValue > 0)
		{
			Invoke("TickingTimer", 1);
		}
	}

	private void TickingTimer()
	{
		HitCooldownValue -= 1;
	}

	private void InputSystem()
	{
		if (Input.GetKey(KeyCode.E))
		{
			if (HitCooldownValue <= 0)
			{
				Attack();
			}
		}
	}

    private void Attack()
	{
		HitCooldownValue += HitCooldown;
		SwordModel.SetActive(true);
		AnimatorController.Play(" ");
		Invoke("ResetAttack", HitCooldown);
	}

	private void ResetAttack()
	{
        SwordModel.SetActive(false);
    }

}
