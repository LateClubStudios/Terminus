//* Main Controller for player - jump, move, crouch, sprint, cover and stop on pause. 
//* Morgan Joshua Finney & Josh Lennon 
//* Sep 18 Through Feb 19
/// For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // Used for managing scene transitions.

public class PlayerController : MonoBehaviour
{

    public static bool lockOutAll = true; // Locks the entire player script. Static because it is referenced in other scripts.

    // Private variables. Getting the player's rigid body, animator, and collider.
    private Rigidbody playerRigidbody;
    private Animator playerAnimator;
    private CapsuleCollider playerCollider;
    string[] controllers;


    // dont know what these are for this is why we should name stuff properly
    private bool gamePaused; // This is the boolean to check if the game is paused. Is unused.
    private float oldSpeed; // This was initially used for storing speed. Is unused.
    Vector3 Legacy; // Initally stored the player's previous position before he warped to the cover plane. Is unused.
    public GameObject boxAttach; // Used for attaching the box. Is used.
    float difrance2; // Supposedly gets the distance between two objects. Goes unused.
    bool covered; // Used for the cover system. Determines whether or not the player is covered or not. Is used.

    // Don't delete these so we can reference them in our blogs.



    void Awake() // Starts as soon as the game is loaded
    {
        StartCoroutine(playerGroundedCheckSet());

        controllers = Input.GetJoystickNames();

        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        movementSpeed = movementSpeedWalk;
        // Setting variables at the beginning to certain values and components
    }

    void Update() // Contains all of the gameplay mechanics the player uses. Uses if statements and booleans to determine when these systems should be used.
    {
        FovSystem();
        CameraSystem();
        RagdollSystem();
        DeathSystem();
        if (lockOutAll != true && death != true)
        {
            if (jumpSwitch == true)
            {
                JumpSystem();

            }
            if (crouchSwitch == true)
            {
                CrouchSystem();
            }
            AttackSystem();
            CoverSystem();

            DeathSystem();
            if (covered != true && movementSwitch == true)
            {
                MovementSystem();
            }

            GrabSystem();

            if (death == false && gameIsOver == false && rotateSwitch == true && rotateSwitch2 == true)
            {
                RotationSystem();
            }

        }
    }




    //      ______ ______      __   _______     _______ _______ ______ __  __ 
    //     |  ____/ __ \ \    / /  / ____\ \   / / ____|__   __|  ____|  \/  |
    //     | |__ | |  | \ \  / /  | (___  \ \_/ / (___    | |  | |__  | \  / |
    //     |  __|| |  | |\ \/ /    \___ \  \   / \___ \   | |  |  __| | |\/| |
    //     | |   | |__| | \  /     ____) |  | |  ____) |  | |  | |____| |  | |
    //     |_|    \____/   \/     |_____/   |_| |_____/   |_|  |______|_|  |_|

    //*    Changes the main cameras FOV based on the players x position

    public Camera mainCamera;
    float FovSpeed = 0.015f;
    Vector3 lastFramePosition;

    private void FovSystem()
    {
        if (transform.position.x < 1 && transform.position.x > -1)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 65, FovSpeed);
        }
        else if (transform.position.x > 2)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 75, FovSpeed);
        }
        else if (transform.position.x < -3.5)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 30, FovSpeed);
        }


        if (transform.position.x < lastFramePosition.x - 0.0005)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 30, FovSpeed);
        }
        else if (transform.position.x > lastFramePosition.x + 0.0005)
        {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, 75, FovSpeed);
        }
        lastFramePosition = transform.position;
    }



    //       _____          __  __ ______ _____               _______     _______ _______ ______ __  __ 
    //      / ____|   /\   |  \/  |  ____|  __ \     /\      / ____\ \   / / ____|__   __|  ____|  \/  |
    //     | |       /  \  | \  / | |__  | |__) |   /  \    | (___  \ \_/ / (___    | |  | |__  | \  / |
    //     | |      / /\ \ | |\/| |  __| |  _  /   / /\ \    \___ \  \   / \___ \   | |  |  __| | |\/| |
    //     | |____ / ____ \| |  | | |____| | \ \  / ____ \   ____) |  | |  ____) |  | |  | |____| |  | |
    //      \_____/_/    \_\_|  |_|______|_|  \_\/_/    \_\ |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* Changes the main cameras y and z position based on the players y and z position.

    public Transform cameraTrack;
    float tempX, tempY, tempZ;

    private void CameraSystem()
    {
        tempX = Mathf.Lerp(tempX, hips.transform.position.x, 0.1f);
        tempY = Mathf.Lerp(tempY, hips.transform.position.y, 0.1f);
        tempZ = Mathf.Lerp(tempZ, hips.transform.position.z, 0.1f);

        cameraTrack.transform.position = new Vector3(tempX, tempY - 1, tempZ);

        //if (hipsTrack == true)
        //{
        //    cameraTrack.transform.position = new Vector3(hips.transform.position.x, hips.transform.position.y - 1, hips.transform.position.z);
        //}
        //else
        //{

        //    cameraTrack.transform.position = new Vector3(cameraTrack.transform.position.x, transform.position.y, transform.position.z);
        //}
    }



    //     __  __  ______      ________ __  __ ______ _   _ _______    _______     _______ _______ ______ __  __ 
    //    |  \/  |/ __ \ \    / /  ____|  \/  |  ____| \ | |__   __|  / ____\ \   / / ____|__   __|  ____|  \/  |
    //    | \  / | |  | \ \  / /| |__  | \  / | |__  |  \| |  | |    | (___  \ \_/ / (___    | |  | |__  | \  / |
    //    | |\/| | |  | |\ \/ / |  __| | |\/| |  __| | . ` |  | |     \___ \  \   / \___ \   | |  |  __| | |\/| |
    //    | |  | | |__| | \  /  | |____| |  | | |____| |\  |  | |     ____) |  | |  ____) |  | |  | |____| |  | |
    //    |_|  |_|\____/   \/   |______|_|  |_|______|_| \_|  |_|    |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* moves the player, basically sprint and walk.

    public static bool movementSwitch = true;
    private bool sprintInputKeyboard;
    private float horizontalInput, movementSpeed, sprintInputXbox, movementSpeedMax, movementSpeedWalk = 10000, movementSpeedSprint = 12500, movementSpeedMaxWalk = 1.25f, movementSpeedMaxSprint = 2.75f;
    public Transform movementTargetTrack, movementTarget, xboxTargetTrack;
    Vector3 movementTargetDirection, movementClamp;

    private void MovementSystem()
    {
        //* sets variables based on certain inputs
        sprintInputXbox = Input.GetAxis("Sprint");
        sprintInputKeyboard = Input.GetButton("Sprint");
        horizontalInput = Input.GetAxis("Horizontal");

        //* moves the player using physics and force 
        movementTargetDirection = movementTarget.position - transform.position;
        movementTargetDirection = movementTargetDirection.normalized;
        movementTargetTrack.transform.position = transform.position;
        movementTargetTrack.transform.rotation = transform.rotation;

        xboxTargetTrack.transform.position = transform.position;
        xboxTargetTrack.transform.rotation = transform.rotation;

        //* turns sprint on and off
        if (Input.GetButton("Sprint") && horizontalInput > 0.5f || sprintInputKeyboard == true && horizontalInput >= 0) // Turns the sprint on
        {
            movementSpeed = movementSpeedSprint;
            movementSpeedMax = movementSpeedMaxSprint;
            playerAnimator.SetBool("isSprint", true);
        }
        else if (Input.GetButton("Sprint") && horizontalInput < -0.5f/*sprintInputKeyboard == true && horizontalInput <= -0.5f*/)
        {
            movementSpeed = movementSpeedWalk;
            movementSpeedMax = movementSpeedMaxWalk;
        }
        else if (Input.GetButton("Sprint") == false/*sprintInputXbox == 0 && horizontalInput == 0 || sprintInputKeyboard == false*/) // Turns the sprint off
        {
            movementSpeed = movementSpeedWalk;
            movementSpeedMax = movementSpeedMaxWalk;
            playerAnimator.SetBool("isSprint", false);
        }

        //* adds corect force towards the target.
        if (horizontalInput > 0)
        {
            GetComponent<Rigidbody>().AddForce(movementTargetDirection * movementSpeed * Time.deltaTime);
            playerAnimator.SetBool("isWalk", true);
            if (movementSpeed == movementSpeedSprint)
            {
                playerAnimator.SetFloat("Direction", 1, 1f, Time.deltaTime * 10f);
            }
            else
            {
                playerAnimator.SetFloat("Direction", 0.5f, 1f, Time.deltaTime * 10f);
            }
        }
        else if (horizontalInput < 0)
        {
            GetComponent<Rigidbody>().AddForce(-movementTargetDirection * movementSpeedWalk * Time.deltaTime);
            playerAnimator.SetBool("isWalk", true);
            if (movementSpeed == -movementSpeedSprint)
            {
                playerAnimator.SetFloat("Direction", -1, 1f, Time.deltaTime * 10f);
            }
            else
            {
                playerAnimator.SetFloat("Direction", -0.5f, 1f, Time.deltaTime * 10f);
            }
        }
        else if (horizontalInput == 0)
        {
            playerAnimator.SetBool("isWalk", false);
            playerAnimator.SetFloat("Direction", 0, 1f, Time.deltaTime * 10f);
        }

        //* stops the player going to fast
        movementClamp = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedMax);
        GetComponent<Rigidbody>().velocity = new Vector3(movementClamp.x, playerRigidbody.velocity.y, movementClamp.z);
    }


    //   _____   ____ _______    _______ _____ ____  _   _    _______     _______ _______ ______ __  __ 
    //  |  __ \ / __ \__   __|/\|__   __|_   _/ __ \| \ | |  / ____\ \   / / ____|__   __|  ____|  \/  |
    //  | |__) | |  | | | |  /  \  | |    | || |  | |  \| | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //  |  _  /| |  | | | | / /\ \ | |    | || |  | | . ` |  \___ \  \   / \___ \   | |  |  __| | |\/| |
    //  | | \ \| |__| | | |/ ____ \| |   _| || |__| | |\  |  ____) |  | |  ____) |  | |  | |____| |  | |
    //  |_|  \_\\____/  |_/_/    \_\_|  |_____\____/|_| \_| |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* Gets the mouses screen position and converst it to a rotation for the player. 

    float turnVal;
    int rotationLoop;
    public static bool rotateSwitch = true;
    public float xboxRHor, xboxRVer;
    public GameObject XboxAim;
    float rotSpeed = 200000;
    int i;


    private void RotationSystem()
    {
        controllers = Input.GetJoystickNames();
        if (controllers.Length > 0)
        {
            //Iterate over every element
            //for (i = 0; i < controllers.Length; ++i)
            //{
            //Check if the string is empty or not
            if (!string.IsNullOrEmpty(controllers[i]))
            {
                RotationSystemXbox();
            }
            else
            {
                RotationSystemMouse();
            }
            //}
        }
        else
        {
            RotationSystemMouse();
        }

    }

    private void RotationSystemXbox()
    {
        //Not empty, controller temp[i] is connected
        Debug.Log("Controller " + i + " is connected using: " + controllers[i]);

        Debug.Log("Player Controller | RotationSystem | Controller connected");
        xboxRHor = Input.GetAxis("RightHorizontal") + 1;
        xboxRVer = Input.GetAxis("RightVertical") + 1;

        if (xboxRHor > 0.1 || xboxRVer > 0.1)
        {

            float playerX, playerZ;
            playerX = transform.position.x + 1;
            playerZ = transform.position.z - 1;
            XboxAim.transform.position = new Vector3(playerX - xboxRVer, transform.position.y, playerZ + xboxRHor);

            Vector3 targetDir = XboxAim.transform.position - transform.position;

            // The step size is equal to speed times frame time.
            float step = rotSpeed * Time.deltaTime;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            // Move our position a step closer to the target.
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }

    private void RotationSystemMouse()
    {
        //If it is empty, controller i is disconnected
        //where i indicates the controller number
        Debug.Log("Controller: " + i + " is disconnected.");

        Debug.Log("Player Controller | RotationSystem | Controller not connected");
        for (rotationLoop = 0; rotationLoop <= 180; rotationLoop++)
        {
            float mouseX = Input.mousePosition.x;
            float mouseY = Input.mousePosition.y;

            int screenXCut = Screen.width / 8;

            int screenXTemp = screenXCut * 6;
            int screenX = screenXTemp / 180;
            int screenY = Screen.height / 3;

            int screenSpaceUpper = screenX * rotationLoop + screenXCut;
            int screenSpaceLower = screenSpaceUpper - screenX;

            // if mouse is in 1st 8th player walks straight left
            if (mouseX < screenXCut && transform.rotation != Quaternion.Euler(0, 180, 0))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            // if mouse is in last 8th player walks straight right
            else if (mouseX > screenXCut * 7 && transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            // if mouse is between first and last 8th player walks on angle dependent on position between the 8th's
            else if (mouseX > screenXCut && mouseX < screenXCut * 7 && mouseX < screenSpaceUpper && mouseX > screenSpaceLower && turnVal != rotationLoop)
            {

                if (mouseY < screenY)
                {
                    turnVal = 180 - rotationLoop;
                }
                else if (mouseY > screenY)
                {
                    turnVal = 180 + rotationLoop;
                }

                transform.rotation = Quaternion.Euler(0, turnVal, 0);
            }
        }

        if (rotationLoop > 180)
        {
            rotationLoop = 0;
        }
    }



    //        _ _    _ __  __ _____     _______     _______ _______ ______ __  __ 
    //       | | |  | |  \/  |  __ \   / ____\ \   / / ____|__   __|  ____|  \/  |
    //       | | |  | | \  / | |__) | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //   _   | | |  | | |\/| |  ___/   \___ \  \   / \___ \   | |  |  __| | |\/| |
    //  | |__| | |__| | |  | | |       ____) |  | |  ____) |  | |  | |____| |  | |
    //   \____/ \____/|_|  |_|_|      |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* powers the jump vault and climb machanics

    public float movementSpeedClimb, movementSpeedClimbMax;
    public static bool jumpSwitch = true;
    public bool isGrounded = false;
    private Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    private float jumpForce = 30f;

    public bool climbingArea;
    bool beTheClimbing = false;

    [SerializeField] bool vaultingArea = false;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);
    public Vector3 endPos = new Vector3(0f, 1.5f, 0.75f);

    bool hipsTrack = false;

    bool rotateSwitch2 = true;
    bool vaultingAboveNot = true;

	Vector3 test;
    IEnumerator Vault01()
    {
        yield return new WaitForSeconds(1.0f);
        playerAnimator.SetBool("isVault", false);
        yield return new WaitForSeconds(1.26f);
        hipsTrack = false;
        transform.position = new Vector3(hips.transform.position.x, hips.transform.position.y - 0.1f, hips.transform.position.z);
        hips.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        rotateSwitch2 = true;
        jumpSwitch = true;
        movementSwitch = true;
    }

    bool climbUp = false, climbDown = false;

    private void ClimbingSystem()
    {
        if (climbingArea == true)
        {
            GetComponent<Rigidbody>().useGravity = false;
            
            if (Input.GetButton("Jump") == true)
            {
                climbUp = true;
                climbDown = false;
            }
            else if (Input.GetButton("Jump") == false)
            {
                if (isGrounded != true)
                {
                    climbUp = false;
                    climbDown = true;
                }
                else if (isGrounded == true)
                {
                    climbUp = false;
                    climbDown = false;
                }

            }
        }
        else
        {
            climbUp = false;
            climbDown = false;

            rotateSwitch = true;
            GetComponent<Rigidbody>().useGravity = true;
            playerAnimator.SetBool("isClimbing", false);
        }


        if (climbUp == true)
        {
            rotateSwitch = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * movementSpeedClimb * Time.deltaTime);
            Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
            GetComponent<Rigidbody>().velocity = new Vector3(movementClamp.x, movementClamp.y, movementClamp.z);

            playerAnimator.SetFloat("UpOrDown", 0);
            playerAnimator.SetBool("isClimbing", true);
        }
        else if (climbDown == true)
        {
            rotateSwitch = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(-Vector3.up * movementSpeedClimb * Time.deltaTime);
            Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
            GetComponent<Rigidbody>().velocity = new Vector3(movementClamp.x, movementClamp.y, movementClamp.z);

            playerAnimator.SetFloat("UpOrDown", 1);
            playerAnimator.SetBool("isClimbing", true);
        }
        else
        {
            rotateSwitch = true;
            playerAnimator.SetBool("isClimbing", false);
        }


        //Legacy system
        //if (Input.GetButtonDown("Jump") && vaultingArea == false && climbingArea == true)
        //{
        //    beTheClimbing = true;
        //    playerAnimator.SetBool("isClimbing", true);
        //    playerAnimator.SetFloat("UpOrDown", 0);
        //}
        //else if (Input.GetButtonUp("Jump") && beTheClimbing == true || climbingArea == false && beTheClimbing == true)
        //{
        //    //rotateSwitch = true;
        //    playerAnimator.SetFloat("UpOrDown", 1);
        //    beTheClimbing = false;
        //    Debug.Log("Player Controller | ClimbingSystem() | Climbing off");
        //}
        //if (climbingArea == false)
        //{
        //    playerAnimator.SetBool("isClimbing", false);
        //}

        //if (beTheClimbing == true)
        //{
        //    rotateSwitch = false;
        //    transform.rotation = new Quaternion(0, 180, 0, 0);
        //    Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
        //    isGrounded = false;
        //    GetComponent<Rigidbody>().AddForce(Vector3.up * movementSpeedClimb * Time.deltaTime);

        //    Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
        //    GetComponent<Rigidbody>().velocity = new Vector3(movementClamp.x, movementClamp.y, movementClamp.z);
        //    Debug.Log("Player Controller | Player's movement locked, fall you fucking cunt");
        //}
    }



    private void JumpSystem()
    {
        ClimbingSystem();

        playerAnimator.SetFloat("Height", transform.position.y);

        if (vaultingArea == true && transform.position.y > boxAttach.transform.position.y)
        {
            vaultingAboveNot = false;
        }
        else
        {
            vaultingAboveNot = true;
        }

        if (Input.GetButtonDown("Jump") && isGrounded == true && vaultingArea == false && climbingArea == false)
        {
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            playerAnimator.SetBool("isJump", true);
        }
        else if (Input.GetButtonDown("Jump") /*&& isGrounded == true*/ && vaultingArea == true && climbingArea == false && vaultingAboveNot == true)
        {
            isGrounded = false;
            rotateSwitch2 = false;
            jumpSwitch = false;
            movementSwitch = false;

			GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            
            test = new Vector3 (boxAttach.transform.position.x, transform.position.y, boxAttach.transform.position.z);

            transform.LookAt(test);



            playerAnimator.SetBool("isVault", true);
            hipsTrack = true;
            vaultingArea = false;
            StartCoroutine("Vault01");
        }
        else if (Input.GetButtonUp("Jump"))
        {
            playerAnimator.SetBool("isJump", false);
        }


       

        if (isGrounded == true)
        {
            playerAnimator.SetBool("isJump", false);
        }
    }



    //    _____ _____   ____  _    _ _   _ _____  ______ _____     _______     _______ _______ ______ __  __ 
    //   / ____|  __ \ / __ \| |  | | \ | |  __ \|  ____|  __ \   / ____\ \   / / ____|__   __|  ____|  \/  |
    //  | |  __| |__) | |  | | |  | |  \| | |  | | |__  | |  | | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //  | | |_ |  _  /| |  | | |  | | . ` | |  | |  __| | |  | |  \___ \  \   / \___ \   | |  |  __| | |\/| |
    //  | |__| | | \ \| |__| | |__| | |\  | |__| | |____| |__| |  ____) |  | |  ____) |  | |  | |____| |  | |
    //   \_____|_|  \_\\____/ \____/|_| \_|_____/|______|_____/  |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* Checks wether the player is at the same hight as befor therefor telling us if he is on the ground

    float lastHight;

    IEnumerator playerGroundedCheck()
    {
        if (transform.position.y < lastHight + 0.00005f && transform.position.y > lastHight - 0.00005f)
        {
            yield return new WaitForSeconds(2.0f);
            Debug.Log("PlayerController | PlayerGroundCheck - Player is Last Height");
            isGrounded = true; // If the player is on the floor, then this boolean is set to true.
        }
        else if (transform.position.y > lastHight + 0.00005f || transform.position.y < lastHight - 0.00005f)
        {
            yield return new WaitForSeconds(0.01f);
            Debug.Log("PlayerController | PlayerGroundCheck - Player is lower or higher than last height");
            isGrounded = false; // If the player is on the floor, then this boolean is set to true.
        }

        StartCoroutine(playerGroundedCheckSet());
    }

    IEnumerator playerGroundedCheckSet()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("PlayerController | PlayerGroundCheck - setting last height");
        lastHight = transform.position.y;

        StartCoroutine(playerGroundedCheck());
    }



    //       _____ _____   ____  _    _  _____ _    _    _______     _______ _______ ______ __  __ 
    //      / ____|  __ \ / __ \| |  | |/ ____| |  | |  / ____\ \   / / ____|__   __|  ____|  \/  |
    //     | |    | |__) | |  | | |  | | |    | |__| | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //     | |    |  _  /| |  | | |  | | |    |  __  |  \___ \  \   / \___ \   | |  |  __| | |\/| |
    //     | |____| | \ \| |__| | |__| | |____| |  | |  ____) |  | |  ____) |  | |  | |____| |  | |
    //      \_____|_|  \_\\____/ \____/ \_____|_|  |_| |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* changes the colidder and animation on the player

    bool crouchTimeout = false;
    bool crouchTimeoutOnce = false;
    bool crouchOutBool = false;
    public static bool crouchSwitch = true;

    private void CrouchSystem()
    {
        if (Input.GetButtonDown("Crouch") && crouchTimeout == false)
        {
            Debug.Log("PlayerController | CrouchSystem - Player Crouched");
            playerCollider.height = 1.0f;
            playerCollider.center = new Vector3(0f, 0.6f, 0f);
            playerAnimator.SetBool("isCrouch", true);
            if (movementSpeed > 0)
            {
                playerAnimator.SetFloat("Direction", 0.5f, 1f, Time.deltaTime * 10f);
            }
            else if (movementSpeed < 0)
            {
                playerAnimator.SetFloat("Direction", -0.5f, 1f, Time.deltaTime * 10f);
            }
            else
            {
                playerAnimator.SetFloat("Direction", 0f, 1f, Time.deltaTime * 10f);
            }
            crouchTimeout = true;
         
        }

        if (crouchTimeout == true && crouchTimeoutOnce == false)
        {
            crouchTimeoutOnce = true;
            StartCoroutine("crouchTimer");
        }

        if (Input.GetButtonUp("Crouch") && crouchOutBool == false)
        {
            Debug.Log("PlayerController | CrouchSystem - Player UnCrouched");
            playerCollider.height = 1.7f;
            playerCollider.center = new Vector3(0f, 0.85f, 0f);
            playerAnimator.SetBool("isCrouch", false);
        }
    }

    IEnumerator crouchTimer()
    {
        yield return new WaitForSeconds(1.0f);
        crouchTimeout = false;
        crouchTimeoutOnce = false;
        Debug.Log("PlayerController | CrouchSystem - Player Crouch Timeout Ended");
    }



    //        _____ _____            ____     _______     _______ _______ ______ __  __ 
    //       / ____|  __ \     /\   |  _ \   / ____\ \   / / ____|__   __|  ____|  \/  |
    //      | |  __| |__) |   /  \  | |_) | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //      | | |_ |  _  /   / /\ \ |  _  <  \___ \  \   / \___ \   | |  |  __| | |\/| |
    //      | |__| | | \ \  / ____ \| |_) |  ____) |  | |  ____) |  | |  | |____| |  | |
    //       \_____|_|  \_\/_/    \_\____/  |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* If the player is in the trigger...

    public GameObject Box;
    bool dragOn = false;
    bool dragLOL = false;

    public GameObject player;  // Variable for getting the player
    public Transform boxPos;
    Quaternion rotation;       // Rotation variable to stop the box from rotating awkwardly - rigid body constraints stop working when useing perant
    Vector3 difrance, boyBeLitAndHoldingTheBox, boxStart;

    void GrabSystem()
    {
        if (dragOn == true)
        {
            Debug.Log("PlayerController | GrabSystem - Player in Grab Area");

            if (Input.GetButtonDown("Grab"))
            {
                Debug.Log("PlayerController | GrabSystem - Player Grabed");
                boyBeLitAndHoldingTheBox = transform.position;
                boxStart = Box.transform.position;
            }

            if (Input.GetButton("Grab"))
            {
                playerAnimator.SetBool("isDrag", true);
                Debug.Log("PlayerController | GrabSystem - Grab Movment");

                Box.GetComponent<Rigidbody>().mass = 0;


                rotateSwitch = false; // Turns player rotation off
                jumpSwitch = false; // Turns player's jump mechanic off

                difrance.z = boyBeLitAndHoldingTheBox.z - transform.position.z;

                Box.transform.position = new Vector3(boxStart.x, boxStart.y, boxStart.z - difrance.z);

                if (transform.position.z < Box.transform.position.z)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else if (transform.position.z > Box.transform.position.z)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                }
            }
            else
            {
                Debug.Log("PlayerController | GrabSystem - Box UnGrabbed");
                playerAnimator.SetBool("isDrag", false);

                if (Box != null)
                {
                    Box.GetComponent<Rigidbody>().mass = 10;
                }

                rotateSwitch = true; // Turn rotation back on
                jumpSwitch = true; // Turn jumping back on
            }


        }
        else if (dragOn == false && Input.GetButton("Grab"))
        {
            Debug.Log("PlayerController | GrabSystem - Box UnGrabbed");
            playerAnimator.SetBool("isDrag", false);

            if (Box != null)
            {
                Box.GetComponent<Rigidbody>().mass = 10;
            }

            rotateSwitch = true; // Turn rotation back on
            jumpSwitch = true; // Turn jumping back on
        }

    }


    //        _____ ______      ________ _____     _______     _______ _______ ______ __  __ 
    //       / ____/ __ \ \    / /  ____|  __ \   / ____\ \   / / ____|__   __|  ____|  \/  |
    //      | |   | |  | \ \  / /| |__  | |__) | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //      | |   | |  | |\ \/ / |  __| |  _  /   \___ \  \   / \___ \   | |  |  __| | |\/| |
    //      | |___| |__| | \  /  | |____| | \ \   ____) |  | |  ____) |  | |  | |____| |  | |
    //       \_____\____/   \/   |______|_|  \_\ |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* allows the player to cover

    bool coverAllowed = false;
    public GameObject hidePlane;
    private Vector3 playerPos;
    public bool isCovered = false;

    private void CoverSystem()
    {
        if (Input.GetButtonDown("Cover") && hidePlane != null && covered == false)
        {
            covered = true;
            rotateSwitch = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            Debug.Log("PlayerController | CoverSystem - player covered");
            playerAnimator.SetBool("isCovered", true);
            //Legacy = transform.position;
            transform.position = hidePlane.transform.position;
        }
        if (Input.GetButtonUp("Cover") && hidePlane != null && covered == true)
        {
            covered = false;
            rotateSwitch = true;
            Debug.Log("PlayerController | CoverSystem - Cover system off");
            playerAnimator.SetBool("isCovered", false);
            //transform.position = Legacy;
        }
    }



    //             _______ _______       _____ _  __   _______     _______ _______ ______ __  __ 
    //          /\|__   __|__   __|/\   / ____| |/ /  / ____\ \   / / ____|__   __|  ____|  \/  |
    //         /  \  | |     | |  /  \ | |    | ' /  | (___  \ \_/ / (___    | |  | |__  | \  / |
    //        / /\ \ | |     | | / /\ \| |    |  <    \___ \  \   / \___ \   | |  |  __| | |\/| |
    //       / ____ \| |     | |/ ____ \ |____| . \   ____) |  | |  ____) |  | |  | |____| |  | |
    //      /_/    \_\_|     |_/_/    \_\_____|_|\_\ |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* allows the player to attack

    public static Bandits banditScript;
    public static bool attackActive = false;
    bool bandit = false;
    public static GameObject banditObj;
    bool attackCooldown = false;

    private void AttackSystem()
    {
        Debug.Log(string.Format("Bandit: {0}", banditObj));

        if (Input.GetButtonDown("Attack") && attackCooldown == false)
        {
            attackActive = true;
            //movementSwitch = false;
            crouchSwitch = false;
            jumpSwitch = false;
            rotateSwitch = false;
            playerAnimator.SetBool("isAttack", true);
            attackCooldown = true;
            Invoke("ResetCooldown", 1.0f);
            float floatRand = Random.Range(1f,3f);
            float intRound = Mathf.Round(floatRand);

            playerAnimator.SetFloat("Attack", intRound);

            if (bandit == true && banditObj != null)
            {
                banditScript = banditObj.GetComponent<Bandits>();
            }
        }

        if (Input.GetButtonUp("Attack"))
        {
            playerAnimator.SetBool("isAttack", false);
            StartCoroutine("AttackTimer");
        }
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1.0f);
        attackActive = false;
        //movementSwitch = true;
        crouchSwitch = true;
        jumpSwitch = true;
        rotateSwitch = true;
    }

    void ResetCooldown()
    {
        attackCooldown = false;
    }

    //       _____  ______       _______ _    _    _______     _______ _______ ______ __  __ 
    //      |  __ \|  ____|   /\|__   __| |  | |  / ____\ \   / / ____|__   __|  ____|  \/  |
    //      | |  | | |__     /  \  | |  | |__| | | (___  \ \_/ / (___    | |  | |__  | \  / |
    //      | |  | |  __|   / /\ \ | |  |  __  |  \___ \  \   / \___ \   | |  |  __| | |\/| |
    //      | |__| | |____ / ____ \| |  | |  | |  ____) |  | |  ____) |  | |  | |____| |  | |
    //      |_____/|______/_/    \_\_|  |_|  |_| |_____/   |_| |_____/   |_|  |______|_|  |_|

    //* kills player and opens game over scene

    public static bool death = false;
    public GameObject hips;
    public static bool gameIsOver = false;
    public Animator transAnim;

    public static bool ragdollMe = false;
    public static bool ragdollToggle = false;

    private void RagdollSystem()
    {
        if (ragdollToggle == true)
        {Debug.Log("RAGon");
            Collider[] bodies = hips.GetComponentsInChildren<Collider>();

            //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
            foreach (Collider rb in bodies)
            {
                rb.isTrigger = false;
            }
            GetComponent<Rigidbody>().isKinematic = true;
            ragdollMe = true;

            
        }
        else if (ragdollToggle == false)
        {Debug.Log("RAGoff");
            Collider[] bodies = hips.GetComponentsInChildren<Collider>();

            //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
            foreach (Collider rb in bodies)
            {
                rb.isTrigger = true;
            }

            ragdollMe = false;
            GetComponent<Rigidbody>().isKinematic = false;
            
        }
    }


    private void DeathSystem()
    {
        if (death == true)
        {

            PlayerController.gameIsOver = true;
            ragdollToggle = true;
            StartCoroutine(gOverScene());

        }
        else if (death == false)
        {
            PlayerController.gameIsOver = false;
        }
    }

    IEnumerator gOverScene()
    {
        transAnim.SetBool("animaFadeOut", true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(4);
    }



    //        _____ ____  _      _      _____  _____ _____ ____  _   _      ____________  _____ _____  _____ ______ _____  
    //       / ____/ __ \| |    | |    |_   _|/ ____|_   _/ __ \| \ | |  __|__  __|  __ \|_   _/ ____|/ ____|  ____|  __ \ 
    //      | |   | |  | | |    | |      | | | (___   | || |  | |  \| |( _ ) | |  | |__) | | || |  __| |  __| |__  | |__) |
    //      | |   | |  | | |    | |      | |  \___ \  | || |  | | . ` |/ _ \/\ |  |  _  /  | || | |_ | | |_ |  __| |  _  / 
    //      | |___| |__| | |____| |____ _| |_ ____) |_| || |__| | |\  | (_>  < |  | | \ \ _| || |__| | |__| | |____| | \ \ 
    //       \_____\____/|______|______|_____|_____/|_____\____/|_| \_|\___/\/_|  |_|  \_\_____\_____|\_____|______|_|  \_\

    //* detects collisions and triggers and sets veribels to be used by verious systems.

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Floor")
        {
            isGrounded = true; // If the player is on the floor, then this boolean is set to true.
            jumpSwitch = true;
        }

        if (hit.transform.gameObject.tag == "Kill")
        {
            death = true;
        }

        if (hit.transform.gameObject.tag == "Bandit")
        {
            banditObj = hit.gameObject;
            Debug.Log("Player At Bandit");
            bandit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bandit")
        {
            banditObj = other.gameObject;
            Debug.Log("Player At Bandit");
            bandit = true;
        }

        if (other.tag == "Kill")
        {
            death = true;
        }

        if (other.tag == "Vaultable" || other.tag == "VaultDrag")
        {
            vaultingArea = true;

            boxAttach = other.gameObject;
        }

        if (other.tag == "Cover")
        {
            hidePlane = other.gameObject;
        }

        if (other.tag == "Drag" || other.tag == "VaultDrag") // ...and the object's tag is Player...
        {
            Box = other.gameObject;
            dragOn = true;
        }

        if (other.tag == "Climb")
        {
            climbingArea = true;
        }

        if (other.tag == "CrouchOut")
        {
            crouchOutBool = true;
            Debug.Log("PlayerController | CrouchSystem - Player can not stand here");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Bandit")
        {
            banditObj = other.gameObject;

            if (attackActive == true)
            {
                transform.LookAt(banditObj.transform);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Bandit")
        {
            banditObj = null;
            Debug.Log("Player At Bandit");
            bandit = false;
        }

        if (other.tag == "Vaultable" || other.tag == "VaultDrag")
        {
            vaultingArea = false;
            boxAttach = null;
        }

        if (other.tag == "Climb")
        {
            climbingArea = false;
        }

        if (other.tag == "Cover")
        {
            hidePlane = null;
        }

        if (other.tag == "CrouchOut")
        {
            crouchOutBool = false;
            Debug.Log("PlayerController | CrouchSystem - Player can stand here");
        }
    }
}

//* Welcome to 1000 lines of hell.
//* So guys, we did it. We reached 1,000 lines of hell - a quarter of 4,000 lines of hell and still counting. The fact that we reached this number in such a short amount of time is phenomenal - I'm just amazed at how quick we reached this milestone.