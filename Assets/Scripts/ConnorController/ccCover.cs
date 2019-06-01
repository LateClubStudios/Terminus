using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccCover : MonoBehaviour {

    bool coverAllowed = false;
    public GameObject hidePlane;
    private Vector3 playerPos;
    public bool isCovered = false;
    bool covered;

    private void Update()
    {
        if (GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().coverSwitch == true)
        {

            if (Input.GetButtonDown("Cover") && GetComponent<ccLocks>().controllerSwitch == false && hidePlane != null && covered == false || Input.GetButtonDown("CoverXbox") && GetComponent<ccLocks>().controllerSwitch == true && hidePlane != null && covered == false)
            {
                covered = true;
                GetComponent<ccLocks>().rotationSwitch = false;
                GetComponent<ccLocks>().movementSwitch = false;
                GetComponent<ccLocks>().jumpSwitch = false;
                GetComponent<ccLocks>().crouchSwitch = false;
                GetComponent<ccLocks>().grabSwitch = false;
                GetComponent<CapsuleCollider>().height = 1.0f;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.6f, 0f);
                transform.rotation = Quaternion.Euler(0, 90, 0);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                Debug.Log("PlayerController | CoverSystem - player covered");
                GetComponent<Animator>().SetBool("isCovered", true);
                transform.position = hidePlane.transform.position;
            }
            if (Input.GetButtonUp("Cover") && GetComponent<ccLocks>().controllerSwitch == false && hidePlane != null && covered == true || Input.GetButtonUp("CoverXbox") && GetComponent<ccLocks>().controllerSwitch == true && hidePlane != null && covered == true)
            {
                covered = false;
                GetComponent<ccLocks>().rotationSwitch = true;
                GetComponent<ccLocks>().movementSwitch = true;
                GetComponent<ccLocks>().jumpSwitch = true;
                GetComponent<ccLocks>().crouchSwitch = true;
                GetComponent<ccLocks>().grabSwitch = true;
                GetComponent<CapsuleCollider>().height = 1.7f;
                GetComponent<CapsuleCollider>().center = new Vector3(0f, 0.85f, 0f);
                Debug.Log("PlayerController | CoverSystem - Cover system off");
                GetComponent<Animator>().SetBool("isCovered", false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cover")
        {
            hidePlane = other.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cover")
        {
            hidePlane = null;
        }
    }
}
