
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {

    public GameObject item;
    public GameObject player;
    bool dragging = false;

    private void Update()
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

// void OnTriggerEnter(Collider other)
  //  {
   //     if (other.tag == ("Grabbable"))
   //     {
    //        if (Input.GetKeyDown("e"))
    //        {
    //            Debug.Log("Object grabbed");
    //            item.transform.parent = player.transform;
    //        }
    //        else if (Input.GetKeyUp("e"))
    //        {
     //           Debug.Log("Object ungrabbed");
    //            item.transform.parent = null;
    //        }
     //   }


   // }
}
