//* Rotates a player based on the mouses position on screen.
//* Morgan Joshua Finney
//* Jan 19
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour {

    float turnVal;
    public Quaternion debug;
    int loop;
    public Vector3 target;
    public GameObject player;
    public GameObject playerHolder;
    Vector3 PosOverride;

    void Update ()
    {
        if (PlayerDeath.death == false && PlayerController.gameIsOver == false)
        {
            RotatePlayer();
        }
    }

    void RotatePlayer()
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
            if (mouseX < screenXCut  && transform.rotation != Quaternion.Euler(0, 180, 0))
            {
                Debug.Log("mouse in lock range left");
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            //* if mouse is in last 8th player walks straight rigth
            else if (mouseX > screenXCut * 7 && transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                Debug.Log("mouse in lock range right");
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            //* if mouse is between first and last 8th player walks on angle dependent on position between the 8th's
            else if (mouseX > screenXCut && mouseX < screenXCut * 7 && mouseX < screenSpaceUpper && mouseX > screenSpaceLower && turnVal != loop)
            {
                Debug.Log("mouse in dynamic range");

                    if (mouseY < screenY)
                    {
                        turnVal = 180 - loop;
                    }
                    else if (mouseY > screenY)
                    {
                        turnVal = 180 + loop;
                    }

                    Debug.Log(string.Format("turnVal = {0}", turnVal));
                    Debug.Log(string.Format("loop = {0}", loop));

                    debug = new Quaternion(0, turnVal, 0, 0);
                    Debug.Log("player do the big rotate");
                    transform.rotation = Quaternion.Euler(0, turnVal, 0);
            }
            //* if mouse is not in an area or has not moved nothing happens
            else
            {
                Debug.Log("mouse null");
            }
        }

        if (loop > 180)
        {
            loop = 0;
        }

        

    }
}
