using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	public float MoveSpeed;

	[Header("Jumping")]
	public float JumpForce;
	public float JumpCooldown;
	public float AirMultiplier;

	[Header("Ground Check")]
	public float GroundDrag;
	public LayerMask GroundMask;
	public bool IsGrounded;

	[Header("import stuff")]
	public Transform Player;
	public Rigidbody RigidBody;
	public Camera Camera;
	private float horizontalInput;
	private float verticalInput;

	private void Awake()
	{
		RigidBody = GetComponent<Rigidbody>();
		RigidBody.freezeRotation = true;
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
	}

	private void OnTriggerStay(Collider other)
	{
		IsGrounded = true;
	}

	private void OnTriggerExit(Collider other)
	{
		IsGrounded = false;
	}

	private void KeyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (Input.GetKey(KeyCode.Space) && IsGrounded)
		{
			Jump();		
		}
	}

	private void GroundCheck()
	{
		if (IsGrounded)
		{
			RigidBody.drag = GroundDrag;
		}
		if (!IsGrounded)
		{
			RigidBody.drag = 0;
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
		if (IsGrounded)
		{
			if (horizontalInput > 0) { transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed, Space.World); }
            if (horizontalInput < 0) { transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed, Space.World); }
            if (verticalInput > 0) { transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed, Space.World); }
            if (verticalInput < 0) { transform.Translate(Vector3.back * Time.deltaTime * MoveSpeed, Space.World); }
        }
	}

	private void SpeedControl()
	{
		Vector3 flatVel = new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z);
		if (flatVel.magnitude > MoveSpeed)
		{
			Vector3 limiedVel = flatVel.normalized * MoveSpeed;
			RigidBody.velocity = new Vector3(limiedVel.x, RigidBody.velocity.y, limiedVel.z);
		}
	}

	private void Jump()
	{
		RigidBody.velocity = new Vector3(RigidBody.velocity.x, 0f, RigidBody.velocity.z);
		RigidBody.AddForce(transform.up * JumpForce, ForceMode.Impulse);
	}
}
	