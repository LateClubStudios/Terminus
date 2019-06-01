//* Stops the box going to far, used becuse of collider issue
//* Morgan Joshua Finney
//* Feb 19
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLock : MonoBehaviour {

    public float maxZ, minZ;

	// Update is called once per frame
	void Update () {
		
        if (transform.position.z > maxZ)
        {
            Debug.Log("BoxLock | Locked on Max");
            transform.position = new Vector3(transform.position.x, transform.position.y, maxZ);
            //GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().grabSwitch = false;
            StartCoroutine("Timer01");
        }
        else if(transform.position.z < minZ)
        {
            Debug.Log("BoxLock | Locked on Min");
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);
            //GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().grabSwitch = false;
            StartCoroutine("Timer01");
        }
        else
        {
            Debug.Log("BoxLock | Not Locked");
        }

	}

    IEnumerator Timer01()
    {
        Debug.Log("BoxLock | timer");
        yield return new WaitForSeconds(1.0f);
        //GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().grabSwitch = true;
    }
}
