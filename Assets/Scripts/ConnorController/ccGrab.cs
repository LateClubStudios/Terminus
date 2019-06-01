using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ccGrab : MonoBehaviour
{

    GameObject grabObj;
    bool grabArea = false;
    public bool grabbing = false;
    Vector3 startEndPosDifference, boyBeLitAndHoldingTheBox /* playerStartPos */, grabObjStartPos;

    void Update()
    {
        if (GetComponent<ccLocks>().mainSwitch == true && GetComponent<ccLocks>().grabSwitch == true && grabArea == true)
        {
            Debug.Log("PlayerController | GrabSystem - Player in Grab Area");

            if (Input.GetButtonDown("Grab") && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonDown("GrabXbox") && GetComponent<ccLocks>().controllerSwitch == true)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                grabbing = true;

                Debug.Log("PlayerController | GrabSystem - Player Grabed");
                boyBeLitAndHoldingTheBox = transform.position;
                grabObjStartPos = grabObj.transform.position;

                if (transform.position.z < grabObj.transform.position.z)
                {
                    transform.rotation = new Quaternion(0, 0, 0, 0);
                }
                else if (transform.position.z > grabObj.transform.position.z)
                {
                    transform.rotation = new Quaternion(0, 180, 0, 0);
                }

                GetComponent<ccLocks>().rotationSwitch = false;
                GetComponent<ccLocks>().jumpSwitch = false;
                GetComponent<ccLocks>().crouchSwitch = false;
                GetComponent<ccLocks>().coverSwitch = false;
                grabObj.GetComponent<Rigidbody>().mass = 0;
                GetComponent<Animator>().SetBool("isDrag", true);
            }
            else if (Input.GetButtonUp("Grab") && GetComponent<ccLocks>().controllerSwitch == false || Input.GetButtonUp("GrabXbox") && GetComponent<ccLocks>().controllerSwitch == true)
            {
                StartCoroutine("LetItGo");
            }

            if (grabbing == true)
            {
                Debug.Log("PlayerController | GrabSystem - Grab Movment");
                startEndPosDifference.z = boyBeLitAndHoldingTheBox.z - transform.position.z;

                grabObj.transform.position = new Vector3(grabObjStartPos.x, grabObjStartPos.y, grabObjStartPos.z - startEndPosDifference.z);
            }
        }
    }

    IEnumerator LetItGo()
    {
        yield return new WaitForEndOfFrame();
        grabbing = false;
        Debug.Log("PlayerController | GrabSystem - grabObj UnGrabbed");
        GetComponent<Animator>().SetBool("isDrag", false);

        if (grabObj != null)
        {
            grabObj.GetComponent<Rigidbody>().mass = 10;
        }
        GetComponent<ccLocks>().rotationSwitch = true;
        GetComponent<ccLocks>().jumpSwitch = true;
        GetComponent<ccLocks>().crouchSwitch = true;
        GetComponent<ccLocks>().coverSwitch = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Drag" || other.tag == "VaultDrag")
        {
            grabObj = other.gameObject;
            grabArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Drag" || other.tag == "VaultDrag")
        {
            grabArea = false;
            StartCoroutine("LetItGo");
        }
    }
}