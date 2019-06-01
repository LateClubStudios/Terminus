using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSounds : MonoBehaviour
{
    AudioSource footstep;
    BoxCollider footstepCollider;
    public bool sprinting;

    private void Start()
    {
        footstep = GetComponent<AudioSource>();
        footstepCollider = GetComponent<BoxCollider>();
        sprinting = false;
    }

    private void Update()
    {
        if (Input.GetButton("Sprint") && Input.GetAxis("Horizontal") > 0f)
        {
            sprinting = true;
            GetComponent<BoxCollider>().size = new Vector3(0.5f, 1f, 0.5f);
        }
        else
        {
            sprinting = false;
            GetComponent<BoxCollider>().size = new Vector3(0.5f, 1.65f, 0.5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor" && Input.GetAxis("Horizontal") != 0)
        {
            //GetComponent<BoxCollider>().size = new Vector3(0.5f, 1.65f, 0.5f);
            footstep.Play(0);
            Debug.Log("boi's big timbs be smacking that floor");
        }

        if (other.tag == "Floor" && Input.GetAxis("Horizontal") != 0 && sprinting == true)
        {
            //GetComponent<BoxCollider>().size = new Vector3(0.5f, 1f, 0.5f);
            footstep.Play(0);
            Debug.Log("enter your social security number here");
        }
        else
        {
            GetComponent<BoxCollider>().size = new Vector3(0.5f, 1.65f, 0.5f);
        }
    }

}
