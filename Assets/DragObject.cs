using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public GameObject item;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Grabbable" && Input.GetKeyDown("E"))
        {
            Debug.Log("Object grabbed");
            item.transform.parent = player.transform;
        }
    }
}


