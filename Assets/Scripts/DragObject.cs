//* Allows player to drag objects 
//* Morgan Joshua Finney
//* Sep 18 Through Dec 18
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

    public GameObject item;
    public GameObject player;
    //bool dragging = false;

    //private void Update()
    //{
    //    if (Input.GetButtonDown("Grab"))
    //    {
    //        Debug.Log("Object grabbed");
    //        item.transform.parent = player.transform;
    //    }
    //    else if (Input.GetButtonUp("Grab"))
    //    {
    //        Debug.Log("Object ungrabbed");
    //        item.transform.parent = null;
    //    }
    //}


    Quaternion rotation;
    void Awake()
    {
        rotation = item.transform.rotation;
    }
    void LateUpdate()
    {
        item.transform.rotation = rotation;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == ("Grabbable"))
        {
            if (Input.GetButtonDown("Grab"))
            {
                Debug.Log("Object grabbed");
                item.transform.parent = player.transform;
            }
            else if (Input.GetButtonUp("Grab"))
            {
                Debug.Log("Object ungrabbed");
                item.transform.parent = null;
            }
        }


    }
}
