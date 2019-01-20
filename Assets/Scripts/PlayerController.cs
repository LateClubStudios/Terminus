﻿//* Main Controller for player - jump, move, crouch, sprint, cover and stop on pause. 
//* Morgan Joshua Finney & Josh Lennon 
//* Sep 18 Through Jan 19
//* For NextGen Synoptic Project Game Outnumbered

using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Private variables. Getting the player's rigid body, animator, and collider.
	private Rigidbody playerRigidbody;
	private	Animator playerAnimator;
	private BoxCollider playerCollider;

	//* For MovmentSystem
	private float speed;
	private float walkSpeed = 0.025f;
    private float horizontalMovment;

	//* For LaneSystem
	private float verticalMovment;
	int lane = 0;

    // Variables for speed. One for the XBOX, one for the player's sprinting speed.
    private float spritXbox;
	private float sprintSpeed = 0.055f;

	//* For JumpingSystem
	bool isGrounded = true;
	private Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	private float jumpForce = 1.7f;

    //* For VaultingSystem
    bool vaultingArea = false;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);
    public Vector3 endPos = new Vector3(0f, 1.5f, 0.75f);


    //* For StopOnPauseSystem
    private bool gamePaused;
	private float oldSpeed;

    // For GameOver
    public static bool gameIsOver = false;

    // For CoverSystem
    bool coverAllowed = false;
    public GameObject hidePlane;
    private Vector3 playerPos;
    public bool isCovered = false;


    void Start ()
	{
		playerRigidbody = GetComponent<Rigidbody>();
		playerAnimator = GetComponent<Animator>();
		playerCollider = GetComponent<BoxCollider>();
		speed = walkSpeed; // Stores walkSpeed in the speed variable.
        //gameIsOver = false;
        playerPos.x = transform.position.x;
        playerAnimator.SetBool("isCovered", false);
	}

    void Update()
    {
      
    }

    void FixedUpdate ()
	{
        if (gameIsOver == false)
        {
            JumpingSystem();
            MovmentSystem();
            CrouchingSystem();
            SprintSystem();
            StopOnPauseSystem();
            //LaneSystem();
            CoverSystem();
            // Calls functions for the player's movement, as well as the pause system.
        }

    }

	void StopOnPauseSystem() // Pause menu
	{
		gamePaused = OurPauseMenu.gameIsPaused;
		if (speed != 0) {
			oldSpeed = speed; // If the player is moving, store the speed
		}
		if (gamePaused == false) {
			speed = oldSpeed; // If the player unpauses, set the speed to the speed that was stored before the player paused.
		} else if (gamePaused == true) {
			speed = 0; // If the player pauses, set the speed to 0
		}
	}

	private void SprintSystem()
	{

		spritXbox = Input.GetAxis ("Sprint");

		if (spritXbox > 0.5)
		{
			speed = sprintSpeed; // If the player's speed is more than 0.5, then the player is sprinting.
            playerAnimator.SetBool("isSprint", true);
		}
		else if (spritXbox < 0.5)
		{
			speed = walkSpeed; // If the player's speed is less, the player is walking.
            playerAnimator.SetBool("isSprint", false);
		}
	}

	private void LaneSystem()
	{
		verticalMovment = Input.GetAxis("Vertical");

		if (verticalMovment > 0.5 && lane == 0)
		{
			transform.position += new Vector3 (1.0f, 0.0f, 0.0f); // Only moving on the X axis.
			lane = 1;
		}
		else if (verticalMovment < -0.5 && lane == 1)
		{
			transform.position -= new Vector3 (1.0f, 0.0f, 0.0f); // Only moving on the X axis.
			lane = 0;
		}
	}

	private void MovmentSystem()
	{

        horizontalMovment = Input.GetAxis("Horizontal");

        float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (0.0f, 0.0f, moveHorizontal); // Only moving on the Z axis.

		transform.Translate(movement * speed);
        if (horizontalMovment != 0)
        {
            playerAnimator.SetBool("isWalk", true); // Triggers animation for walking.
        }
        else if (horizontalMovment == 0)
        {
            playerAnimator.SetBool("isWalk", false); // Triggers animation for walking.
        }

    }
		
	private void JumpingSystem()
	{
		if (Input.GetButtonDown("Jump") && isGrounded == true && vaultingArea == false)
		{
			Debug.Log("Player called jump");
			playerRigidbody.AddForce(jump * jumpForce, ForceMode.Impulse); // Makes the player jump via the rigidbody.
			isGrounded = false;
		}
        else if (Input.GetButtonDown("Jump") && isGrounded == true && vaultingArea == true)
        {
            isGrounded = false;
            transform.position += new Vector3(0.0f, 1.5f, 0.75f);
            vaultingArea = false;
            Debug.Log("Player called vault");
        }
	}

	private void CrouchingSystem()
	{
		if (Input.GetButtonDown("Crouch"))
		{
			Debug.Log("Player called crouch");
			playerCollider.size = new Vector3(0.41f, 1.2f, 0.39f);
			playerCollider.center = new Vector3(0f, 0.6f, 0f);
			playerAnimator.SetBool("isCrouch", true); // Halves the collider on the player and sets the paramenter on the animator to true.
		}

		if (Input.GetButtonUp("Crouch"))
		{
			Debug.Log("Player called stand");

			playerCollider.size = new Vector3(0.41f, 1.67f, 0.39f);
			playerCollider.center = new Vector3(0f, 0.84f, 0f);
		    playerAnimator.SetBool("isCrouch", false); // Returns collider back to normal and sets parameter to false.
		}
	}

    private void CoverSystem()
    {
        if (Input.GetButtonDown("Cover") && coverAllowed == true)
        {
            if (isCovered == true)
            {
                isCovered = false;
                playerAnimator.SetBool("isCovered", true); // [Supposed to] play the animation to cover behind the box.
                Debug.Log("Player called COVER");
                playerPos.x = transform.position.x - hidePlane.transform.position.x; // Subtracts the player's X position by the plane's X position. The plane is used for covering.
                transform.position = new Vector3(playerPos.x, transform.position.y, transform.position.z); // Warps the player to the plane.
            }
            else if (isCovered == false)
            {
                isCovered = true;
                playerAnimator.SetBool("isCovered", true); // [Supposed to] return the player back to their idle state.
                Debug.Log("Player be dying");
                transform.position = new Vector3(-playerPos.x, transform.position.y, transform.position.z); // Warps the player back to the place they were previously.
            }
        }
    } // ** TODO ** - Stop player movement during covering

	//* event for JumpingSystem
	void OnCollisionEnter()
	{
		isGrounded = true; // If the player is on the floor, then this boolean is set to true.
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vaultable")
        {
                vaultingArea = true;
            Debug.Log("Inside the vaulting area");
        }

        if (other.tag == "Cover")
        {
            coverAllowed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vaultable")
        {
            vaultingArea = false;
            Debug.Log("Outside the vaulting area");
        }
        
        if (other.tag == "Cover")
        {
            coverAllowed = false;
            isCovered = false;
            transform.position = new Vector3(-playerPos.x, transform.position.y, transform.position.z);
            playerAnimator.SetBool("isCovered", false);
        }
    }
}