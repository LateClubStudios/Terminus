using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public Rigidbody rbCamera;
    Vector3 originalPos;
    // Use this for initialization
    void Start()
    {
        rbCamera = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Floor"))
        {
            rbCamera.constraints = RigidbodyConstraints.FreezeRotationY;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if (other.gameObject.tag == ("Floor"))
        {
            rbCamera.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
