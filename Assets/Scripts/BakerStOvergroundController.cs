using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakerStOvergroundController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(OvergroundStart());
	}

    IEnumerator OvergroundStart()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().mainSwitch = true;
        GameObject.Find("PlayerVisionCone").SetActive(true);
        GameObject.Find("PlayerRig").GetComponent<ccLocks>().musicSwitch = true;
    }
	
}
