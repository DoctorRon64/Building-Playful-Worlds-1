using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
	public enum StateEnum { Idle, Walk, Sprint, Crouch, Attack }
	public StateEnum currentState;

	[Header("Combat")]
	public int Health;
	public int AttackDamage;
	public int TakingDamage;
	public bool AttackingEnemy;

	[Header("Movement")]
	public CharacterController CharacterController;
	private float currentSpeed;
	public float WalkSpeed;
	public float SprintSpeed;
	public float CrouchSpeed;
	public LayerMask CheckWallInfront;

	[Header("Ground Check")]
	public IsFeetOnGround IsFeetOnGround;
	public float GroundDrag;
	public LayerMask GroundMask;
	private bool isGrounded;

	[Header("import stuff")]
	public Transform Player;
	public Transform Head;
	public TMP_Text HealthText;
	public Camera Camera;
	private Animator anim;
	public float horizontalInput;
	public float verticalInput;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		currentState = StateEnum.Idle;
		AttackingEnemy = false;
	}
	private void Update()
	{
		GroundCheck();
		KeyInput();
		CheckState();
		IsPlayerDead();
		HealthText.text = Health.ToString();
	}

	private void FixedUpdate()
	{
		MovePlayer();
		RotatePlayer();
	}

	private void IsPlayerDead()
	{
		if (Health <= 0)
		{
			currentState = StateEnum.Idle;
			
		}
	}

    private void CheckState()
	{
		switch (currentState)
		{
			case StateEnum.Idle: Idle(); break;
			case StateEnum.Walk: NormalWalk(); break;
			case StateEnum.Sprint: Sprinting(); break;
			case StateEnum.Crouch: CrouchingPlayer(); break;
			case StateEnum.Attack: AttackWithSword(); break;
		}
	}
	private void Idle()
	{
		anim.SetInteger("PlayerState", 0);
		AttackingEnemy = false;
	}

	private void KeyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		if (Input.GetKey(KeyCode.E))
		{
			currentState = StateEnum.Attack;
		}

		if ((verticalInput != 0 || horizontalInput != 0) && currentState == StateEnum.Idle)
		{
			currentState = StateEnum.Walk;
		}
	}

	private void GroundCheck()
    {
        isGrounded = IsFeetOnGround.OnGround;
    }

    private void RotatePlayer()
	{
		RaycastHit hit;
		Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
		
		if (Physics.Raycast(ray, out hit))
		{
            transform.LookAt(hit.point);
			transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        }
    }

	private void MovePlayer()
	{
		if (verticalInput > 0) 
		{
			CharacterController.Move(transform.forward * Time.deltaTime * currentSpeed);
        }
		if (verticalInput < 0)
		{
			CharacterController.Move(-transform.forward * Time.deltaTime * currentSpeed);
		}
		if (horizontalInput > 0)
		{
			CharacterController.Move(Vector3.right * Time.deltaTime * currentSpeed);
		}
		if (horizontalInput < 0)
		{
			CharacterController.Move(-Vector3.right * Time.deltaTime * currentSpeed);
		}
	}

	private void NormalWalk()
	{
		anim.SetInteger("PlayerState", 1);

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			currentState = StateEnum.Sprint;
		}

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			currentState = StateEnum.Crouch;
		}

		if (horizontalInput == 0 && verticalInput == 0)
		{
			currentState = StateEnum.Idle;
		}

		currentSpeed = WalkSpeed;
		MovePlayer();
	}

	private void CrouchingPlayer()
	{
		anim.SetInteger("PlayerState", 2);
		currentSpeed = CrouchSpeed;
		MovePlayer();

		if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			currentState = StateEnum.Walk;
		}

		if (horizontalInput == 0 && verticalInput == 0)
		{
			currentState = StateEnum.Idle;
		}
	}

	private void Sprinting()
	{
		anim.SetInteger("PlayerState", 3);
		currentSpeed = SprintSpeed;
		MovePlayer();

		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			currentState = StateEnum.Walk;
		}

		if (horizontalInput == 0 && verticalInput == 0)
		{
			currentState = StateEnum.Idle;
		}
	}
	private void AttackWithSword()
	{
		anim.SetInteger("PlayerState", 5);
		AttackingEnemy = true;
		currentState = StateEnum.Idle;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Enemy"))
		{
			Health -= TakingDamage;
		}
	}
}
	