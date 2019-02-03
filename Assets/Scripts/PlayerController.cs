//* Main Controller for player - jump, move, crouch, sprint, cover and stop on pause. 
//* Morgan Joshua Finney & Josh Lennon 
//* Sep 18 Through Jan 19
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    // Private variables. Getting the player's rigid body, animator, and collider.
	private Rigidbody playerRigidbody;
	private	Animator playerAnimator;
	private BoxCollider playerCollider;

    //* For MovmentSystem and Sprint System
    private bool movementInputForward, movementInputBackward, sprintInputKeyboard;
    private float horizontalInput, movementSpeed, sprintInputXbox, movementSpeedMax, movementSpeedWalk = 10, movementSpeedSprint = 16, movementSpeedMaxWalk = 1.25f, movementSpeedMaxSprint = 2.75f;
    public Transform movementTargetTrack, movmentTarget, cameraTrack;
    Vector3 movementTargetDirection;

    //* For JumpingSystem
    public static bool jumpSwitch = true;
	bool isGrounded = true;
	private Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
	private float jumpForce = 17f;

    //* For VaultingSystem
    bool vaultingArea = false;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);
    public Vector3 endPos = new Vector3(0f, 1.5f, 0.75f);

    //* RotationSystem
    float turnVal;
    int loop;
    public static bool rotateSwitch = true;

    //* DeathSystem
    public static bool death = false;
    public GameObject hips;

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
        movementSpeed = movementSpeedWalk;
        playerPos.x = transform.position.x;
        playerAnimator.SetBool("isCovered", false);
	}

    void Update()
    {
        DeathSystem();
        MovmentSystem();

        if (death == false && gameIsOver == false && rotateSwitch == true)
        {
            RotationSystem();
        }
    }

    void FixedUpdate ()
	{
        if (gameIsOver == false)
        {
            MovementInputSystem();
            SprintSystem();

            if (jumpSwitch == true)
            {
                JumpingSystem();
            }

            CrouchingSystem();
            AttackingSystem();
            //CoverSystem();
        }

    }

    private void MovementInputSystem()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            movementInputForward = true;
            movementInputBackward = false;
        }
        else if (horizontalInput < 0)
        {
            movementInputForward = false;
            movementInputBackward = true;
        }
        else if (horizontalInput == 0)
        {
            movementInputForward = false;
            movementInputBackward = false;
        }
    }

    private void MovmentSystem()
    {
        cameraTrack.transform.position = new Vector3(cameraTrack.transform.position.x, transform.position.y, transform.position.z);

        movementTargetTrack.transform.position = transform.position;
        movementTargetTrack.transform.rotation = transform.rotation;

        //float movementTargetDistance = Vector3.Distance(transform.position, movmentTarget.position);

        movementTargetDirection = movmentTarget.position - transform.position;
        movementTargetDirection = movementTargetDirection.normalized;

        if (movementInputForward == true && movementInputBackward == false)
        {
            GetComponent<Rigidbody>().AddForce(movementTargetDirection * movementSpeed);
            playerAnimator.SetBool("isWalk", true);
        }
        else if (movementInputBackward == true && movementInputForward == false)
        {
            GetComponent<Rigidbody>().AddForce(-movementTargetDirection * movementSpeedWalk);
            playerAnimator.SetBool("isWalk", true);
        }
        else if (isGrounded == false)
        {
            Debug.Log("player be flossin'");
        }

        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            playerAnimator.SetBool("isWalk", false);
        }

        if (GetComponent<Rigidbody>().velocity.magnitude > movementSpeedMax)
        {
            GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedMax);
        }
    }

    private void SprintSystem()
    {
        sprintInputXbox = Input.GetAxis("Sprint");
        sprintInputKeyboard = Input.GetButtonDown("Sprint");

        if (sprintInputXbox > 0.5 || sprintInputKeyboard == true)
        {
            movementSpeed = movementSpeedSprint;
            movementSpeedMax = movementSpeedMaxSprint;
            playerAnimator.SetBool("isSprint", true);
        }
        else if (sprintInputXbox < 0.5 || sprintInputKeyboard == false)
        {
            movementSpeed = movementSpeedWalk;
            movementSpeedMax = movementSpeedMaxWalk;
            playerAnimator.SetBool("isSprint", false);
        }
    }

    private void JumpingSystem()
    {
        if (Input.GetButtonDown("Jump") && isGrounded == true && vaultingArea == false)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * 20, ForceMode.Impulse);
            isGrounded = false;
        }
        else if (Input.GetButtonDown("Jump") && isGrounded == true && vaultingArea == true)
        {
            isGrounded = false;
            transform.position += new Vector3(0.0f, 1.5f, 0.75f);
            vaultingArea = false;
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

    private void AttackingSystem()
    {
        if (Input.GetButtonDown("Attack"))
        {
            playerAnimator.SetBool("isAttack", true);
        }

        if (Input.GetButtonUp("Attack"))
        {
            playerAnimator.SetBool("isAttack", false);
        }
    }

    private void RotationSystem()
    {
        for (loop = 0; loop <= 180; loop++)
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            int screenXCut = Screen.width / 8;

            int screenXTemp = screenXCut * 6;
            int screenX = screenXTemp / 180;
            int screenY = Screen.height / 2;

            int screenSpaceUpper = screenX * loop + screenXCut;
            int screenSpaceLower = screenSpaceUpper - screenX;

            //* if mouse is in 1st 8th player walks straight left
            if (mouseX < screenXCut && transform.rotation != Quaternion.Euler(0, 180, 0))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            //* if mouse is in last 8th player walks straight rigth
            else if (mouseX > screenXCut * 7 && transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //* if mouse is between first and last 8th player walks on angle dependent on position between the 8th's
            else if (mouseX > screenXCut && mouseX < screenXCut * 7 && mouseX < screenSpaceUpper && mouseX > screenSpaceLower && turnVal != loop)
            {

                if (mouseY < screenY)
                {
                    turnVal = 180 - loop;
                }
                else if (mouseY > screenY)
                {
                    turnVal = 180 + loop;
                }

                transform.rotation = Quaternion.Euler(0, turnVal, 0);
            }
        }

        if (loop > 180)
        {
            loop = 0;
        }



    }

    private void DeathSystem()
    {
        if (death == true)
        {

            PlayerController.gameIsOver = true;
            playerAnimator.enabled = false;
            hips.SetActive(true);
            StartCoroutine(gOverScene());

        }
        else if (death == false)
        {
            PlayerController.gameIsOver = false;
            playerAnimator.enabled = true;
            hips.SetActive(false);
        }
    }

    IEnumerator gOverScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(4);
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
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Floor")
        {
            isGrounded = true; // If the player is on the floor, then this boolean is set to true.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kill")
        {
            death = true;
        }

        if (other.tag == "Vaultable")
        {
            vaultingArea = true;
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
        }
    }
}