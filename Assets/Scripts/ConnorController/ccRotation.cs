using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccRotation : MonoBehaviour {

    int rotationLoop;
    float turnVal, xboxRHor, xboxRVer, rotSpeed = 200000;

    void Update () {

        //* Xbox Input Movment
        if (GetComponent<ccLocks>().controllerSwitch == true && GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().rotationSwitch == true)
        {
            xboxRHor = Input.GetAxis("RightHorizontal") + 1;
            xboxRVer = Input.GetAxis("RightVertical") + 1;

            if (xboxRHor > 0.1 || xboxRVer > 0.1)
            {

                float playerX, playerZ;
                playerX = transform.position.x + 1;
                playerZ = transform.position.z - 1;
                GameObject.Find("Player/MovementTargetXboxHolder/MovementTargetXbox").transform.position = new Vector3(playerX - xboxRVer, transform.position.y, playerZ + xboxRHor);

                Vector3 targetDir = GameObject.Find("Player/MovementTargetXboxHolder/MovementTargetXbox").transform.position - transform.position;

                // The step size is equal to speed times frame time.
                float step = rotSpeed * Time.deltaTime;

                Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                Debug.DrawRay(transform.position, newDir, Color.red);

                // Move our position a step closer to the target.
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }

        //* Keyboard Input Movment
        if (GetComponent<ccLocks>().controllerSwitch == false && GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().rotationSwitch == true)
        {
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
    }
}
