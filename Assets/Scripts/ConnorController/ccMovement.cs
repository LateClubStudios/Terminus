using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccMovement : MonoBehaviour {

    private float movementSpeedMax, movementSpeedWalk = 6000, movementSpeedSprint = 8000, movementSpeedMaxWalk = 0.75f, movementSpeedMaxSprint = 1.75f;
    public float movementSpeed;
    public Vector3 movementClamp;

    void Update () {
        if (GetComponent<ccLocks>().mainSwitch == true  && GetComponent<ccLocks>().movementSwitch == true)
        {
            if (GetComponent<ccLocks>().controllerSwitch == true)
            {
                //* Makes the target move with the player
                GameObject.Find("Player/MovementTargetXboxHolder").transform.position = transform.position;
                GameObject.Find("Player/MovementTargetXboxHolder").transform.rotation = transform.rotation;
            }
            else if (GetComponent<ccLocks>().controllerSwitch == false)
            { 
                //* Makes the target move with the player
                GameObject.Find("Player/MovementTargetHolder").transform.position = transform.position;
                GameObject.Find("Player/MovementTargetHolder").transform.rotation = transform.rotation;
            }

            //* Sets speed of player 
            if (Input.GetButton("Sprint") && Input.GetAxis("Horizontal") > 0f && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButton("SprintXbox") && Input.GetAxis("HorizontalXbox") > 0f && GetComponent<ccLocks>().controllerSwitch == true)
            {
                if (GetComponent<ccCrouch>().crouching == false && GetComponent<ccGrab>().grabbing == false)
                {
                    movementSpeed = movementSpeedSprint;
                    movementSpeedMax = movementSpeedMaxSprint;
                }
                else if (GetComponent<ccCrouch>().crouching == true || GetComponent<ccGrab>().grabbing == true)
                {
                    movementSpeed = movementSpeedWalk;
                    movementSpeedMax = movementSpeedMaxWalk;
                }
            }
            else if (Input.GetButton("Sprint") && Input.GetAxis("Horizontal") < 0f && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButton("SprintXbox") && Input.GetAxis("HorizontalXbox") < 0f && GetComponent<ccLocks>().controllerSwitch == true)
            {
                movementSpeed = movementSpeedWalk;
                movementSpeedMax = movementSpeedMaxWalk;
            }
            else if (Input.GetButton("Sprint") == false && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButton("SprintXbox") == false && GetComponent<ccLocks>().controllerSwitch == true)
            {
                movementSpeed = movementSpeedWalk;
                movementSpeedMax = movementSpeedMaxWalk;
            }

            //* Adds force to the player in the target direction based on the speed
            //* Also sets thew correct animations
            if (Input.GetAxis("Horizontal") > 0 && GetComponent<ccLocks>().controllerSwitch == false || Input.GetAxis("HorizontalXbox") > 0 && GetComponent<ccLocks>().controllerSwitch == true)
            {
                GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * Time.deltaTime);
                GetComponent<Animator>().SetBool("isWalk", true);
                if (movementSpeed == movementSpeedSprint)
                {
                    GetComponent<Animator>().SetFloat("Direction", 1, 1f, Time.deltaTime * 10f);
                }
                else
                {
                    GetComponent<Animator>().SetFloat("Direction", 0.5f, 1f, Time.deltaTime * 10f);
                }
            }
            else if (Input.GetAxis("Horizontal") < 0 && GetComponent<ccLocks>().controllerSwitch == false || Input.GetAxis("HorizontalXbox") < 0 && GetComponent<ccLocks>().controllerSwitch == true)
            {
                GetComponent<Rigidbody>().AddForce(-transform.forward * movementSpeedWalk * Time.deltaTime);
                GetComponent<Animator>().SetBool("isWalk", true);
                GetComponent<Animator>().SetFloat("Direction", -0.5f, 1f, Time.deltaTime * 10f);
            }
            else if (Input.GetAxis("Horizontal") == 0 && GetComponent<ccLocks>().controllerSwitch == false || Input.GetAxis("HorizontalXbox") == 0 && GetComponent<ccLocks>().controllerSwitch == true)
            {
                GetComponent<Animator>().SetBool("isWalk", false);
                GetComponent<Animator>().SetFloat("Direction", 0, 1f, Time.deltaTime * 10f);
            }

            //* Clamps the spped of the player
            movementClamp = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity, movementSpeedMax);
            GetComponent<Rigidbody>().velocity = new Vector3(movementClamp.x, GetComponent<Rigidbody>().velocity.y, movementClamp.z);
        }  
    }
}