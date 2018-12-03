using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody playerRigidbody;
	private	Animator playerAnimator;
	private BoxCollider playerCollider;

	//* For MovmentSystem
	private float speed;
	public float walkSpeed;
	public float sprintSpeed;

	//* For JumpingSystem
	bool isGrounded = true;
	Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	float jumpForce = 1.5f;

	void Start ()
	{
		playerRigidbody = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();
		playerCollider = GetComponent<BoxCollider>();

	}

	void FixedUpdate ()
	{
		JumpingSystem ();
		MovmentSystem ();
		CrouchingSystem ();
	}

	private void MovmentSystem()
	{
		if (Input.GetButtonDown("Sprint"))
		{
			speed = sprintSpeed;
		}
		else if (Input.GetButtonUp("Sprint"))
		{
			speed = walkSpeed;
		}

		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (0.0f, 0.0f, moveHorizontal);

		playerRigidbody.AddForce (movement * speed);
	}
		
	private void JumpingSystem()
	{
		if (Input.GetButtonDown("Jump") && isGrounded == true)
		{
			Debug.Log("Player called jump");
			playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
		}
	}

	private void CrouchingSystem()
	{
		if (Input.GetButtonDown("Crouch"))
		{
			Debug.Log("Player called crouch");
			playerCollider.size = new Vector3(0.41f, 1.2f, 0.39f);
			playerCollider.center = new Vector3(0f, 0.6f, 0f);
			playerAnimator.SetBool("isCrouch", true);
		}

		if (Input.GetButtonUp("Crouch"))
		{
			Debug.Log("Player called stand");

			playerCollider.size = new Vector3(0.41f, 1.67f, 0.39f);
			playerCollider.center = new Vector3(0f, 0.84f, 0f);
		    playerAnimator.SetBool("isCrouch", false);
		}
	}

	//* event for JumpingSystem
	void OnCollisionEnter()
	{
		isGrounded = true;
	}
}