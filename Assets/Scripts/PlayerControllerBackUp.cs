//* Morgan Joshua Finney
//* For Next Gen - ProjectHiro
//* 10-Oct-18
//* A simple movment script for a playable earil stage game demo.


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerControllerBackUp : MonoBehaviour {

	//* veribaqles for player movment
	float playerSpeed = 0f;			//* The current speed of the player
	float walkPower = 0.0005f;		//* The power of walk.
	float walkSpeedMax = 0.02f;		//* The max power of walk.
	float sprintPower = 0.003f;		//* The power of sprint playerSpeed.
	float sprintSpeedMax = 0.05f;	//* The max power of sprint playerSpeed.
	Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	float jumpForce = 1.5f;

	//* veribaqles for player animattion
	bool walkBool = false;			//* Defines if the player is idle or walking
	bool walkBackBool = false;		//* Defines if the player is idle or walking backwards
	bool sprintBool = false;        //* Defines if the player is walking or sprinting
	bool isJumping = false;			//* Defines if the player is jumping
	bool isGrounded = true;			//* Defines if the player is on the ground

	//* Veribels for components of the player
	Rigidbody playerRigidbody;
	Animator playerAnimator;



	// Use this for initialization
	void Start () {
		playerRigidbody = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();
	}



	// Update is called once per frame
	void Update () {

		//* sets the playerSpeed bool values dependent on the users imput
		if (Input.GetButton("Horizontal")) {
			walkBool = true;
		}
		if (Input.GetButtonUp("Horizontal")) {
			walkBool = false;
		}
		if (Input.GetKeyDown("a"))
		{
			walkBackBool = true;
		}
		if (Input.GetKeyUp("a"))
		{
			walkBackBool = false;
		}
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			sprintBool = true;
		}
		if (Input.GetKeyUp (KeyCode.LeftShift)) {
			sprintBool = false;
		}

		//* jump
		if (Input.GetButtonDown("Jump") && isGrounded == true)
		{
			Debug.Log("Player called jump");
			playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse);
			isGrounded = false;
			isJumping = true;
			playerAnimator.SetBool("isJump", true);
		}

		if (isGrounded == true)
		{
			playerAnimator.SetBool("isJump", false);
		}

		if (Input.GetKeyDown("s"))
		{
			Debug.Log("Player called crouch");

			BoxCollider b = gameObject.GetComponent<Collider>() as BoxCollider;
			b.size = new Vector3(0.41f, 1.2f, 0.39f);
			b.center = new Vector3(0f, 0.6f, 0f);
			playerAnimator.SetBool("isCrouch", true);

			//gameObject.gameObject.transform.localScale = new Vector3(4f, 2f, 4f);
		}

		if (Input.GetKeyUp("s"))
		{
			Debug.Log("Player called stand");


			BoxCollider b = gameObject.GetComponent<Collider>() as BoxCollider;
			b.size = new Vector3(0.41f, 1.67f, 0.39f);
			b.center = new Vector3(0f, 0.84f, 0f);
			playerAnimator.SetBool("isCrouch", false);
		}

		// west virginia, mountain mama, take me home, country roads


		//* changes the position of the player based on the value of veriable playerSpeed
		transform.Translate(Vector3.forward * playerSpeed);
	}


	// FixedUpdate is called when a value in the Update event is changed
	void FixedUpdate()
	{

		//* applies the playerSpeed of the player determind by bool value
		if (walkBool == true && sprintBool == false)
		{
			Debug.Log ("Player is walking");
			playerSpeed += walkPower;

			playerAnimator.SetBool("isWalk", true);
			playerAnimator.SetBool("isSprint", false);

			if (playerSpeed > walkSpeedMax)
			{
				playerSpeed = walkSpeedMax;
				Debug.Log("Player is walking MAX");
			}
		}
		else if (walkBool == true && sprintBool == true)
		{
			Debug.Log ("Player is sprinting");
			playerSpeed += sprintPower;

			playerAnimator.SetBool("isWalk", true);
			playerAnimator.SetBool("isSprint", true);

			if (playerSpeed > sprintSpeedMax)
			{
				playerSpeed = sprintSpeedMax;
				Debug.Log("Player is sprinting MAX");
			}
		}

		// bvackwards
		else if (walkBackBool == true && sprintBool == false)
		{
			Debug.Log("Player is walking");
			playerSpeed += -walkPower;

			playerAnimator.SetBool("isWalk", true);
			playerAnimator.SetBool("isSprint", false);

			if (playerSpeed < -walkSpeedMax)
			{
				playerSpeed = -walkSpeedMax;
				Debug.Log("Player is walking MAX");
			}
		}
		else if (walkBackBool == true && sprintBool == true)
		{
			Debug.Log("Player is sprinting");
			playerSpeed += -sprintPower;

			playerAnimator.SetBool("isWalk", true);
			playerAnimator.SetBool("isSprint", true);

			if (playerSpeed < -sprintSpeedMax)
			{
				playerSpeed = -sprintSpeedMax;
				Debug.Log("Player is sprinting MAX");
			}
		}
		else if (walkBackBool == false && walkBool == false)
		{

			playerAnimator.SetBool("isWalk", false);
			playerAnimator.SetBool("isSprint", false);

			Debug.Log ("Player is idle");
			playerSpeed *= 0.9f;
		}
	}

	//* event for jumping mechnics
	void OnCollisionEnter()
	{
		isGrounded = true;
	}



	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Floor")
		{
			playerAnimator.SetBool("isJump", false);
		}
	}
}

// west virginia, mountain mama, take me home, country roads