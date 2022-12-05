using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAttackSystem : MonoBehaviour
{
	public float hitCooldownValue;
    public float hitCooldown = 2;
    public int attackDamage;
	public Animator anim;
	public GameObject swordModel;

	private void Awake()
	{
		swordModel.SetActive(false);
	}

	public void Update()
	{
		InputSystem();

		if (hitCooldownValue > 0)
		{
			Invoke("TickingTimer", 1);
		}
	}

	private void TickingTimer()
	{
		hitCooldownValue -= 1;
	}

	private void InputSystem()
	{
		if (Input.GetKey(KeyCode.E))
		{
			if (hitCooldownValue <= 0)
			{
				Attack();
			}
		}
	}

    private void Attack()
	{
		hitCooldownValue += hitCooldown;
		swordModel.SetActive(true);
		anim.Play(" ");
		Invoke("ResetAttack", hitCooldown);
	}

	private void ResetAttack()
	{
        swordModel.SetActive(false);
    }

}
