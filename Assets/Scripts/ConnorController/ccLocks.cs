using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccLocks : MonoBehaviour {

    string[] controllers;
    public bool controllerSwitch = false, mainSwitch = false, movementSwitch = true, rotationSwitch = true, jumpSwitch = true, crouchSwitch = true, grabSwitch = true, coverSwitch = true, deathToggle = false, ragdollToggle = false, musicSwitch = false;

    void Start () {
        controllerSwitch = false;
        //mainSwitch = true;
        movementSwitch = true;
        rotationSwitch = true;
        jumpSwitch = true;
        crouchSwitch = true;
        grabSwitch = true;
        coverSwitch = true;

        deathToggle = false;
        //ragdollToggle = true;
    }

	void Update () {

        controllers = Input.GetJoystickNames();
        
        if (string.IsNullOrEmpty(controllers[0]))
        {
            Debug.Log("Character Controller | Controller Disconnected");
            controllerSwitch = false;
        }
        else
        {
            Debug.Log("Character Controller | Controller Connected");
            controllerSwitch = true;
        }

    }
}
