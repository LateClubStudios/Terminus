using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody playerRigidbody;
	private	Animator playerAnimator;
	private BoxCollider playerCollider;

	//* For MovmentSystem
	private float speed;
	public float walkSpeed;

	private float spritXbox;
	public float sprintSpeed;

	//* For JumpingSystem
	bool isGrounded = true;
	Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	float jumpForce = 1.5f;

	//* For StopOnPauseSystem
	private bool gamePaused;
	private float oldSpeed;

	void Start ()
	{
		playerRigidbody = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();
		playerCollider = GetComponent<BoxCollider>();
		speed = walkSpeed;
	}

	void FixedUpdate ()
	{
		JumpingSystem ();
		MovmentSystem ();
		CrouchingSystem ();
		SprintSystem ();
		StopOnPauseSystem ();
	}

	void StopOnPauseSystem()
	{
		gamePaused = PauseMenu.gameIsPaused;
		if (speed != 0) {
			oldSpeed = speed;
		}
		if (gamePaused == false) {
			speed = oldSpeed;
		} else if (gamePaused == true) {
			speed = 0;
		}
	}

	private void SprintSystem()
	{

		spritXbox = Input.GetAxis ("Sprint");

		if (spritXbox > 0.5)
		{
			speed = sprintSpeed;
		}
		else if (spritXbox < 0.5)
		{
			speed = walkSpeed;
		}
	}

	private void MovmentSystem()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (0.0f, 0.0f, moveHorizontal);

		transform.Translate(movement * speed);
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