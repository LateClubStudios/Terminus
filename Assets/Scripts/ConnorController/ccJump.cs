using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccJump : MonoBehaviour
{

    public float movementSpeedClimb, movementSpeedClimbMax;
    public bool isGrounded = false;
    private Vector3 jump = new Vector3(0.0f, 2.0f, 0.0f);
    private float jumpForce = 30f;

    public bool climbingArea;
    bool beTheClimbing = false;

    [SerializeField] bool vaultingArea = false;

    bool vaultingAboveNot = true;

    bool climbUp = false, climbDown = false;

    Vector3 lookAtDirectionFix;
    GameObject boxAttach;


    bool jumpOn = false;

    bool jumpActive = false;

    bool jumpEnd = false;

    void Awake()
    {
        StartCoroutine(playerGroundedCheckSet());
    }


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


    private void FixedUpdate()
    {
        //* Xbox off
        if (GetComponent<ccLocks>().controllerSwitch == false && GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().jumpSwitch == true)
        {
            if (Input.GetButton("Jump"))
            {
                jumpOn = true;
            }
            else
            {
                jumpOn = false;
            }
        }
        //* Xbox on
        else if (GetComponent<ccLocks>().controllerSwitch == true && GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().jumpSwitch == true)
        {
            if (Input.GetButton("JumpXbox"))
            {
                jumpOn = true;
            }
            else
            {
                jumpOn = false;
            }
        }
    }

    void Update()
    {
        ClimbingSystem();
        VaultSystem();
        JumpSystem();
    }

    private void JumpSystem()
    {
        GetComponent<Animator>().SetFloat("Height", transform.position.y);

        if (Input.GetButton("JumpXbox") && GetComponent<ccLocks>().controllerSwitch == true && GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().jumpSwitch == true && isGrounded == true && vaultingArea == false && climbingArea == false && jumpActive == false)
        {
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            GetComponent<Animator>().SetBool("isJump", true);
            jumpActive = true;
            jumpOn = false;
        }
        if (Input.GetButton("Jump") && GetComponent<ccLocks>().controllerSwitch == false && GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().jumpSwitch == true && isGrounded == true && vaultingArea == false && climbingArea == false && jumpActive == false)
        {
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
            GetComponent<Animator>().SetBool("isJump", true);
            jumpActive = true;
            jumpOn = false;
        }

        if (jumpEnd == true)
        {
            jumpActive = false;
            GetComponent<Animator>().SetBool("isJump", false);
            jumpEnd = false;
        }
    }

    private void VaultSystem()
    {
        if (vaultingArea == true && transform.position.y > boxAttach.transform.position.y)
        {
            vaultingAboveNot = false;
        }
        else
        {
            vaultingAboveNot = true;
        }

        if (jumpOn == true && isGrounded == true && vaultingArea == true && climbingArea == false && vaultingAboveNot == true)
        {
            isGrounded = false;
            GetComponent<ccLocks>().rotationSwitch = false;
            GetComponent<ccLocks>().jumpSwitch = false;
            GetComponent<ccLocks>().movementSwitch = false;
            GetComponent<ccLocks>().grabSwitch = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            lookAtDirectionFix = new Vector3(boxAttach.transform.position.x, transform.position.y, boxAttach.transform.position.z);
            transform.LookAt(lookAtDirectionFix);
            GetComponent<Animator>().SetBool("isVault", true);
            vaultingArea = false;
            StartCoroutine("Vault01");
        }
    }

    IEnumerator Vault01()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Animator>().SetBool("isVault", false);
        yield return new WaitForSeconds(1.26f);
        transform.position = new Vector3(GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position.x, GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position.y - 0.1f, GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position.z);
        GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        GetComponent<ccLocks>().rotationSwitch = true;
        GetComponent<ccLocks>().jumpSwitch = true;
        GetComponent<ccLocks>().movementSwitch = true;
        GetComponent<ccLocks>().grabSwitch = true;
    }

    private void ClimbingSystem()
    {
        if (climbingArea == true)
        {
            GetComponent<Rigidbody>().useGravity = false;

            if (jumpOn == true)
            {
                climbUp = true;
                climbDown = false;
            }
            else if (jumpOn == false)
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

            //* ERROR LINE - this causes rotation and or jump switch to be permenently on this causes issues with the vaulting script
            //GetComponent<ccLocks>().rotationSwitch = true;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Animator>().SetBool("isClimbing", false);
        }


        if (climbUp == true)
        {
            GetComponent<ccLocks>().rotationSwitch = true;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(Vector3.up * movementSpeedClimb * Time.deltaTime);
            Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<ccMovement>().movementClamp.x, GetComponent<ccMovement>().movementClamp.y, GetComponent<ccMovement>().movementClamp.z);

            GetComponent<Animator>().SetFloat("UpOrDown", 0);
            GetComponent<Animator>().SetBool("isClimbing", true);
        }
        else if (climbDown == true)
        {
            GetComponent<ccLocks>().rotationSwitch = false;
            transform.rotation = new Quaternion(0, 180, 0, 0);
            Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
            isGrounded = false;
            GetComponent<Rigidbody>().AddForce(-Vector3.up * movementSpeedClimb * Time.deltaTime);
            Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<ccMovement>().movementClamp.x, GetComponent<ccMovement>().movementClamp.y, GetComponent<ccMovement>().movementClamp.z);

            GetComponent<Animator>().SetFloat("UpOrDown", 1);
            GetComponent<Animator>().SetBool("isClimbing", true);
        }
        else
        {
            //* ERROR LINE - this causes rotation and or jump switch to be permenently on this causes issues with the vaulting script
            //GetComponent<ccLocks>().rotationSwitch = true;
            GetComponent<Animator>().SetBool("isClimbing", false);
        }

    }

    void OnCollisionEnter(Collision hit)
    {
        if (jumpActive == true)
        {
            jumpEnd = true;
        }
    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vaultable" || other.tag == "VaultDrag")
        {
            vaultingArea = true;

            boxAttach = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vaultable" || other.tag == "VaultDrag")
        {
            vaultingArea = false;
            boxAttach = null;
        }
    }
}