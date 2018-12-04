using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTrack : MonoBehaviour {

    public Transform trackedItem;
    public Transform trackingItem;
	
	// Update is called once per frame
	void Update ()
    {
		trackingItem.transform.position = trackedItem.transform.position;
    }

}
