using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public enum StateEnum { Idle, Walk, Crouch, Jump, Sprint }
	public StateEnum currentState;

	[Header("Movement")]
	public float MoveSpeed;
	public float SprintSpeed;
	public float CroachSpeed;

	[Header("Jumping")]
	public float JumpForce;
	public float JumpCooldown;
	public float AirMultiplier;

	[Header("Ground Check")]
	public float GroundDrag;
	public LayerMask GroundMask;
	private bool isGrounded;

	[Header("import stuff")]
	public Transform Player;
	private Rigidbody rigidBody;
	public Camera Camera;
	private float horizontalInput;
	private float verticalInput;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.freezeRotation = true;
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
			case StateEnum.Walk: MovePlayer(); break;
			case StateEnum.Crouch: CrouchingPlayer(); break;
			case StateEnum.Jump: Jump(); break;
			case StateEnum.Sprint: Sprinting(); break;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		isGrounded = true;
	}

	private void OnTriggerExit(Collider other)
	{
		isGrounded = false;
	}

	private void Idle()
	{
		currentState = StateEnum.Walk;
	}

	private void KeyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (Input.GetKey(KeyCode.Space) && isGrounded)
		{
			currentState = StateEnum.Jump;
		}

		if (Input.GetKey(KeyCode.LeftShift))
		{
			currentState = StateEnum.Sprint;
		}

		if (Input.GetKey(KeyCode.LeftControl))
		{
			currentState = StateEnum.Crouch;
		}
	}

	private void GroundCheck()
	{
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
			/*Vector3 lookAt = hit.point - transform.position;
			lookAt.y = 0;
			transform.rotation = Quaternion.LookRotation(lookAt);*/
            transform.LookAt(hit.point);
			transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

        }
    }

    private void MovePlayer()
	{
		if (horizontalInput > 0) { transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed, Space.World); }
		if (horizontalInput < 0) { transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed, Space.World); }
		if (verticalInput > 0) { transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed, Space.World); }
		if (verticalInput < 0) { transform.Translate(Vector3.back * Time.deltaTime * MoveSpeed, Space.World); }
	}

	private void CrouchingPlayer()
	{

	}

	private void SpeedControl()
	{
		Vector3 flatVel = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
		if (flatVel.magnitude > MoveSpeed)
		{
			Vector3 limiedVel = flatVel.normalized * MoveSpeed;
			rigidBody.velocity = new Vector3(limiedVel.x, rigidBody.velocity.y, limiedVel.z);
		}
	}

	private void Jump()
	{
		rigidBody.velocity = new Vector3(rigidBody.velocity.x, 0f, rigidBody.velocity.z);
		rigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
	}

	private void Sprinting()
	{

	}
}
	