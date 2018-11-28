using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPosition : MonoBehaviour {

    Vector3 originalPos;
	// Use this for initialization
	void Start () {
        originalPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
        if (other.gameObject.tag == ("Kill Plane"))
        {
            gameObject.transform.position = originalPos;
        }
	}
}
