using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	[Header("Movement")]
	public float moveSpeed;

	[Header("Jumping")]
	public float jumpForce;
	public float jumpCooldown;
	public float airMultiplier;

	[Header("Ground Check")]
	public float groundDrag;
	public LayerMask groundMask;
	public bool isGrounded;

	[Header("import stuff")]
	public Transform Player;
	public Rigidbody rb;
	public Camera cam;
	float horizontalInput;
	float verticalInput;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		rb.freezeRotation = true;
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
		isGrounded = true;
	}

	private void OnTriggerExit(Collider other)
	{
		isGrounded = false;
	}

	private void KeyInput()
	{
		horizontalInput = Input.GetAxisRaw("Horizontal");
		verticalInput = Input.GetAxisRaw("Vertical");

		if (Input.GetKey(KeyCode.Space) && isGrounded)
		{
			Jump();		
		}
	}

	private void GroundCheck()
	{
		if (isGrounded)
		{
			rb.drag = groundDrag;
		}
		if (!isGrounded)
		{
			rb.drag = 0;
		}
	}

	private void RotatePlayer()
	{
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		
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
		if (isGrounded)
		{
			if (horizontalInput > 0) { transform.Translate(Vector3.right * Time.deltaTime * moveSpeed, Space.World); }
            if (horizontalInput < 0) { transform.Translate(Vector3.left * Time.deltaTime * moveSpeed, Space.World); }
            if (verticalInput > 0) { transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World); }
            if (verticalInput < 0) { transform.Translate(Vector3.back * Time.deltaTime * moveSpeed, Space.World); }
        }
	}

	private void SpeedControl()
	{
		Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
		if (flatVel.magnitude > moveSpeed)
		{
			Vector3 limiedVel = flatVel.normalized * moveSpeed;
			rb.velocity = new Vector3(limiedVel.x, rb.velocity.y, limiedVel.z);
		}
	}

	private void Jump()
	{
		rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
		rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
	}
}
	