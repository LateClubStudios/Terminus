//* Tracks One Object To Another
//* Morgan Joshua Finney
//* Sep 18
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrack : MonoBehaviour {

    public Transform trackedItem;
    public Transform trackingItem;
	
	// Update is called once per frame
	void Update ()
    {
		trackingItem.transform.position = new Vector3 (trackingItem.transform.position.x, trackedItem.transform.position.y, trackedItem.transform.position.z);
    }

}
