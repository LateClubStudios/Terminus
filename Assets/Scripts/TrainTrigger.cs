//* Starts the train when the player falls
//* Josh Lennon 
//* Nov 18 Through Dec 18
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour {
    // Public variables. Gets the player and the train animator.
    public GameObject player;
    public Animator trainAnim;

    // Values for starting player position

    void Start()
    {
        trainAnim = GetComponent<Animator>();               // Gets the animator
        trainAnim.SetBool("isOnTracks", false);             // Parameter for the animator is set to true. If set to false, the train animation will play once and won't play again unless the player steps into the trigger.
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            trainAnim.SetBool("isOnTracks", true);          // Sets boolean to true so animation can play
        }
        if (PlayerController.death == true)
        {
            trainAnim.SetBool("isOnTracks", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            PlayerController.death = true;
            trainAnim.SetBool("isOnTracks", false);
        }
    }
}
