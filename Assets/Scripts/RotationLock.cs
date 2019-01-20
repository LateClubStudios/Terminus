//* Legacy sccript used to stop player falling over 
//* Morgan Joshua Finney & Josh Lennon 
//* Sep 18 Through Nov 18
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour {

    float rotationBad = 0;
    float rotationBadTimeout = 0;
    bool rotationBadBool = false;
    public GameObject cTest;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (cTest.transform.rotation != Quaternion.Euler(0, 0, 0))
        {
            rotationBad += 1;
            rotationBadTimeout = 11;
            rotationBadBool = true;
        }

        if (rotationBadBool == true)
        {
            rotationBadTimeout -= 1;
        }

        if (rotationBadTimeout < 0)
        {
            rotationBad = 0;
        }

        if (rotationBad > 10)
        {
            Debug.Log("Fixed Rotation");
            cTest.transform.rotation = Quaternion.Euler(0, 0, 0);
            rotationBad = 0;
        }


        cTest.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);

        float i;
        for (i = 15; i == 15; i++)
        {
            if (cTest.transform.rotation == Quaternion.Euler(i, i, i))
            {
                cTest.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (i == 15)
            {
                i = 15;
            }
        }


    }
}
