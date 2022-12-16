using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public enum StateEnum { Idle, Walk, Crouch, Sprint, Jump, Attack }
	public StateEnum currentState;

	[Header("Movement")]
	private float currentSpeed;
	public float WalkSpeed;
	public float SprintSpeed;
	public float CrouchSpeed;

	[Header("Jumping")]
	public float JumpForce;
	public float JumpCooldown;
	public float AirMultiplier;

	[Header("Ground Check")]
	public IsFeetOnGround IsFeetOnGround;
	public float GroundDrag;
	public LayerMask GroundMask;
	private bool isGrounded;

	[Header("import stuff")]
	public Transform Player;
	public Camera Camera;
	private Rigidbody rigidBody;
	private Animator anim;
	public float horizontalInput;
	public float verticalInput;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.freezeRotation = true;
		anim = GetComponent<Animator>();
		currentState = StateEnum.Idle;
	}
	private void Update()
	{
		GroundCheck();
		KeyInput();
		SpeedControl();
	}

	private void FixedUpdate()
	{
		MovePlayer();
		RotatePlayer();
		CheckState();
	}

    private void CheckState()
	{
		switch (currentState)
		{
			case StateEnum.Idle: Idle(); break;
			case StateEnum.Walk: NormalWalk(); break;
			case StateEnum.Crouch: CrouchingPlayer(); break;
			case StateEnum.Sprint: Sprinting(); break;
			case StateEnum.Jump: Jump(); break;
			case StateEnum.Attack: AttackWithSword(); break;
		}
	}
	private void Idle()
	{
		anim.SetInteger("PlayerState", 0);
		if (horizontalInput != 0 || verticalInput != 0)
		{
			currentState = StateEnum.Walk;
		}

		if (Input.GetKey(KeyCode.Space) && isGrounded)
		{
			currentState = StateEnum.Jump;
		}

		if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			currentState = StateEnum.Sprint;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			currentState = StateEnum.Idle;
		} 

		if (Input.GetKeyDown(KeyCode.LeftControl))
		{
			currentState = StateEnum.Crouch;
		}
		else if (Input.GetKeyUp(KeyCode.LeftControl))
		{
			currentState = StateEnum.Idle;
		}

		if (Input.GetKey(KeyCode.E))
		{
			currentState = StateEnum.Attack;
		}
	}

	private void KeyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");
	}

    private void GroundCheck()
    {
        isGrounded = IsFeetOnGround.OnGround;

        if (isGrounded)
        {
            rigidBody.drag = GroundDrag;
        }
        if (!isGrounded)
        {
            rigidBody.drag = 0;
        }
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

	private void SpeedControl()
	{
		Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
		if (flatVel.magnitude > currentSpeed)
		{
			Vector3 limiedVel = flatVel.normalized * currentSpeed;
			rigidBody.velocity = new Vector3(limiedVel.x, rigidBody.velocity.y, limiedVel.z);
		}
	}

	private void MovePlayer()
	{
		if (horizontalInput > 0) { transform.Translate(Vector3.right * Time.deltaTime * currentSpeed, Space.World); }
		if (horizontalInput < 0) { transform.Translate(Vector3.left * Time.deltaTime * currentSpeed, Space.World); }
		if (verticalInput > 0) { transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.World); }
		if (verticalInput < 0) { transform.Translate(Vector3.back * Time.deltaTime * currentSpeed, Space.World); }
	}

	private void NormalWalk()
	{
		anim.SetInteger("PlayerState", 1);
		currentSpeed = WalkSpeed;
		MovePlayer();
		if (horizontalInput == 0 && verticalInput == 0)
		{
			currentState = StateEnum.Idle;
		}
	}

	private void CrouchingPlayer()
	{
		anim.SetInteger("PlayerState", 2);
		currentSpeed = CrouchSpeed;
		MovePlayer();
	}

	private void Sprinting()
	{
		anim.SetInteger("PlayerState", 3);
		currentSpeed = SprintSpeed;
		MovePlayer();

		
	}

	private void Jump()
	{
		anim.SetInteger("PlayerState", 4);
		rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
		rigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
		currentState = StateEnum.Idle;
	}
	

	private void AttackWithSword()
	{
		anim.SetInteger("PlayerState", 5);
		currentState = StateEnum.Idle;
	}
}
	