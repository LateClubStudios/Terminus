using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccJumpNew : MonoBehaviour
{

    float lastHight;
    bool climbArea, vaultArea, jumpActive, vaultingAboveNot;
    public bool isGrounded;
    public float jumpForce = 3000000000000000f, movementSpeedClimb = 280000f, movementSpeedClimbMax = 2000000f;
    GameObject boxAttach;
    public bool playerGrounchCheck = true;

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(playerGroundedCheckSet());
    }

    //IEnumerator playerGroundedCheck()
    //{
    //    if (playerGrounchCheck == true)
    //    {
    //        if (transform.position.y < lastHight + 0.00005f && transform.position.y > lastHight - 0.00005f)
    //        {
    //            yield return new WaitForSeconds(2.0f);
    //            Debug.Log("PlayerController | PlayerGroundCheck - Player is Last Height");
    //            //if (playerGrounchCheck == true)
    //            //{
    //            isGrounded = true; // If the player is on the floor, then this boolean is set to true.
    //            //}
    //        }
    //        else if (transform.position.y > lastHight + 0.00005f || transform.position.y < lastHight - 0.00005f)
    //        {
    //            yield return new WaitForSeconds(0.01f);
    //            Debug.Log("PlayerController | PlayerGroundCheck - Player is lower or higher than last height");
    //            isGrounded = false; // If the player is on the floor, then this boolean is set to true.
    //        }

    //        StartCoroutine(playerGroundedCheckSet());
    //    }
    //    else
    //    {
    //        Debug.Log("End");
    //    }
    //}

    //IEnumerator playerGroundedCheckSet()
    //{
    //    if (playerGrounchCheck == true)
    //    {
    //        yield return new WaitForEndOfFrame();
    //        Debug.Log("PlayerController | PlayerGroundCheck - setting last height");
    //        lastHight = transform.position.y;
    //        StartCoroutine(playerGroundedCheck());
    //    }
    //    else
    //    {
    //        Debug.Log("End");
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().jumpSwitch == true)
        {

            GetComponent<Animator>().SetFloat("Height", transform.position.y);

            if (vaultArea == false && climbArea == false)
            {
                if (Input.GetButtonDown("JumpXbox") && GetComponent<ccLocks>().controllerSwitch == true && isGrounded == true || Input.GetButtonDown("Jump") && GetComponent<ccLocks>().controllerSwitch == false && isGrounded == true)
                {
                    isGrounded = false;
                    jumpActive = true;
                        //GetComponent<Rigidbody>().AddForce(transform.up * jumpForce, ForceMode.Impulse);
                        GetComponent<Animator>().SetBool("isJump", true);
                        GetComponent<Rigidbody>().AddRelativeForce(0, 40, 30, ForceMode.Impulse);

                }
            }
            else if (vaultArea == true && climbArea == false)
            {
                if (vaultArea == true && transform.position.y > boxAttach.transform.position.y)
                {
                    vaultingAboveNot = false;
                }
                else
                {
                    vaultingAboveNot = true;
                }

                if (Input.GetButtonDown("JumpXbox") && GetComponent<ccLocks>().controllerSwitch == true && isGrounded == true && vaultingAboveNot == false || Input.GetButtonDown("Jump") && GetComponent<ccLocks>().controllerSwitch == false && isGrounded == true && vaultingAboveNot == false)
                {
                    isGrounded = false;
                    GetComponent<ccLocks>().rotationSwitch = false;
                    GetComponent<ccLocks>().jumpSwitch = false;
                    GetComponent<ccLocks>().movementSwitch = false;
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    transform.LookAt(new Vector3(boxAttach.transform.position.x, transform.position.y, boxAttach.transform.position.z));
                    GetComponent<Animator>().SetBool("isVault", true);
                    vaultArea = false;
                    StartCoroutine("Vault01");
                }
            }
            else if (vaultArea == false && climbArea == true)
            {
                if (Input.GetButtonDown("CoverXbox") && GetComponent<ccLocks>().controllerSwitch == true || Input.GetButton("Cover") && GetComponent<ccLocks>().controllerSwitch == false)
                {
                    Debug.Log("shut the fuck up liberal");
                    transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
                }
                if (Input.GetButton("JumpXbox") && GetComponent<ccLocks>().controllerSwitch == true || Input.GetButton("Jump") && GetComponent<ccLocks>().controllerSwitch == false)
                {
                    GetComponent<Rigidbody>().useGravity = false;
                    GetComponent<ccLocks>().rotationSwitch = false;
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                    Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
                    isGrounded = false;
                    GetComponent<Rigidbody>().AddForce(Vector3.up * movementSpeedClimb * Time.deltaTime);
                    Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
                    GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<ccMovement>().movementClamp.x, GetComponent<ccMovement>().movementClamp.y, GetComponent<ccMovement>().movementClamp.z);

                    GetComponent<Animator>().SetFloat("UpOrDown", 1, 1f, Time.deltaTime * 10f);
                    GetComponent<Animator>().SetBool("isClimbing", true);

                    if (playerGrounchCheck == false)
                    {
                        playerGrounchCheck = true;
                        //StartCoroutine(playerGroundedCheckSet());
                    }
                }
                else if (Input.GetButton("JumpXbox") == false && Input.GetButton("CrouchXbox") == false && GetComponent<ccLocks>().controllerSwitch == true || Input.GetButton("Jump") == false && Input.GetButton("Crouch") == false && GetComponent<ccLocks>().controllerSwitch == false)
                {
                    if (isGrounded == false)
                    {
                        GetComponent<Rigidbody>().velocity = Vector3.zero;
                        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                        GetComponent<Animator>().SetFloat("UpOrDown", 0, 1f, Time.deltaTime * 10f);
                        isGrounded = false;
                        GetComponent<Rigidbody>().useGravity = false;
                        playerGrounchCheck = false;
                    }
                    else
                    {
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Animator>().SetBool("isClimbing", false);
                    }
                }
                else if (Input.GetButton("CrouchXbox") && GetComponent<ccLocks>().controllerSwitch == true || Input.GetButton("Crouch") && GetComponent<ccLocks>().controllerSwitch == false)
                {
                    if (isGrounded == false || isGrounded == true)
                    {
                        GetComponent<Rigidbody>().useGravity = false;
                        GetComponent<ccLocks>().rotationSwitch = false;
                        transform.rotation = new Quaternion(0, 180, 0, 0);
                        Debug.Log("PlayerController | ClimbingSystem - Climbing Force");
                        GetComponent<Rigidbody>().AddForce(Vector3.down * movementSpeedClimb * Time.deltaTime);
                        Vector3 temp2 = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedClimbMax);
                        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<ccMovement>().movementClamp.x, GetComponent<ccMovement>().movementClamp.y, GetComponent<ccMovement>().movementClamp.z);

                        GetComponent<Animator>().SetFloat("UpOrDown", 0.5f, 1f, Time.deltaTime * 10f);
                        GetComponent<Animator>().SetBool("isClimbing", true);

                        if (playerGrounchCheck == false)
                        {
                            playerGrounchCheck = true;
                            //StartCoroutine(playerGroundedCheckSet());
                        }
                    }
                    else
                    {
                        GetComponent<ccLocks>().rotationSwitch = true;
                        GetComponent<Rigidbody>().useGravity = true;
                        GetComponent<Animator>().SetBool("isClimbing", false);
                    }
                }
            }
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
    }

    void OnCollisionEnter(Collision hit)
    {
        if (jumpActive == true)
        {
            GetComponent<Animator>().SetBool("isJump", false);
            jumpActive = false;
        }

        if (hit.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Vaultable" || other.tag == "VaultDrag")
        {
            vaultArea = true;

            boxAttach = other.gameObject;
        }

        if (other.tag == "Climb")
        {
            GetComponent<ccLocks>().crouchSwitch = false;
            climbArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Vaultable" || other.tag == "VaultDrag")
        {
            vaultArea = false;
            boxAttach = null;
        }

        if (other.tag == "Climb")
        {
            GetComponent<Animator>().SetBool("isClimbing", false);
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<ccLocks>().crouchSwitch = true;
            GetComponent<ccLocks>().rotationSwitch = true;
            climbArea = false;
        }
    }
}
