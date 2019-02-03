//* To be applied to the drag object - moves the object with the player.
//* Morgan Joshua Finney & Josh Lennon
//* Sep 18 Through Jan 19
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObject : MonoBehaviour {
    
    public GameObject player;  // Variable for getting the player
    public Transform playerPos;
    bool draggable = true;     // Boolean for determining whether the object can be dragged or not
    Quaternion rotation;       // Rotation variable to stop the box from rotating awkwardly - rigid body constraints stop working when useing perant


    void Awake() // Same function as Start()
    {
        rotation = transform.rotation; // Sets rotation as object's rotation
    }
    void LateUpdate() // Calls update at the end of the frame
    {
        transform.rotation = rotation; // Reapplies the rotation to the object
    }

    private void Update() // Calls an update every frame
    {
        if (draggable == false) // If the object is not draggable...
        {
            StartCoroutine("timeOut"); // ...start coroutine for not allowing the box to be grabbed after a second
        }
    }
    
    // Coroutine for temporarily disabiling grabbing mechanic to stop spam - allows press every 1 secnd
    IEnumerator timeOut() 
    {
        yield return new WaitForSeconds(1.0f);
        draggable = true;
    }

    void OnTriggerStay(Collider other) // If the player is in the trigger...
    {
        if (other.tag == ("Player")) // ...and the object's tag is Player...
        {

            Debug.Log("boi a bit less sad");
            if (Input.GetButtonDown("Grab")) // ..and the user presses the button to Grab...
            {
                //PlayerController.rotateSwitch = false; // Turns player rotation off
                //PlayerController.jumpSwitch = false; // Turns player's jump mechanic off

                if (player.transform.rotation.y <= -90 /*|| player.transform.rotation.y >= 90*/) // If the player is facing forwards...
                {
                    player.transform.rotation = new Quaternion(0, 180, 0, 0); // Forces player to face forward
                }

                else if (player.transform.rotation.y >= -90 /*&& player.transform.rotation.y <= 90*/) // Else if the player is facing backwards...
                {
                    player.transform.rotation = new Quaternion(0, 0, 0, 0); // Forces player to face backwards
                }


                Debug.Log("Object grabbed");
                //transform.parent = player.transform; // Parent the object to the player's transformation values
                transform.SetParent(playerPos);
            }

            else if (Input.GetButtonUp("Grab")) // If the user lets go of the button to grab...
            {
                unParent();
            }

            if (transform.position.y < 0.75)
            {
                Debug.Log("Box has fallen through the floor. Resetting.");
                unParent();
                transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);
            }
        }


    }

    //private void OnTriggerExit(Collider other) // If the player left the trigger
    //{
    //    if (other.tag == "Player")
    //    {
    //        unParent();
    //    }
    //}

    void unParent ()
    {
        //PlayerController.rotateSwitch = true; // Turn rotation back on
        //PlayerController.jumpSwitch = true; // Turn jumping back on
        Debug.Log("Object ungrabbed");
        transform.parent = null; // unParent object from player
    }
}