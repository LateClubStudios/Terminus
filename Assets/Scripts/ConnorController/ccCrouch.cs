using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccCrouch : MonoBehaviour {

    public bool crouching;

    private void Update()
    {
        if (GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().crouchSwitch == true)
        {
            if (Input.GetButtonDown("Crouch") && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("CrouchXbox") && GetComponent<ccLocks>().controllerSwitch == true)
            {
                crouching = true;
                GetComponent<ccLocks>().jumpSwitch = false;
                Debug.Log("PlayerController | CrouchSystem - Player Crouched");
                GetComponent<CapsuleCollider>().height = 0.5f;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.3f, 0f);
                GetComponent<Animator>().SetBool("isCrouch", true);
                if (GetComponent<ccMovement>().movementSpeed > 0)
                {
                    GetComponent<Animator>().SetFloat("Direction", 0.5f, 1f, Time.deltaTime * 10f);
                }
                else if (GetComponent<ccMovement>().movementSpeed < 0)
                {
                    GetComponent<Animator>().SetFloat("Direction", -0.5f, 1f, Time.deltaTime * 10f);
                }
                else
                {
                    GetComponent<Animator>().SetFloat("Direction", 0f, 1f, Time.deltaTime * 10f);
                }
            }

            if (Input.GetButtonUp("Crouch") && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonUp("CrouchXbox") && GetComponent<ccLocks>().controllerSwitch == true)
            {
                GetComponent<ccLocks>().jumpSwitch = true;
                crouching = false;
                Debug.Log("PlayerController | CrouchSystem - Player UnCrouched");
                GetComponent<CapsuleCollider>().height = 1.7f;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.85f, 0f);
                GetComponent<Animator>().SetBool("isCrouch", false);
            }
        }
    }
}
