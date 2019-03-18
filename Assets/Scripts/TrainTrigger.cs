//* Starts the train when the player falls
//* Morgan Finney & Josh Lennon 
//* Nov 18 Through Mar 19
//* For NextGen Synoptic Project Game Outnumbered`  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour {
    // Public variables. Gets the player and the train animator.
    public GameObject player;

    // Values for starting player position

    void Start()
    {
        GetComponent<Animator>().SetBool("isOnTracks", false);             // Parameter for the animator is set to true. If set to false, the train animation will play once and won't play again unless the player steps into the trigger.
    }

    private void Update()
    {
        if (PlayerController.death == true)
        {
            GetComponent<Animator>().SetBool("isOnTracks", false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player") && PlayerController.death == false)
        {
            StartCoroutine(TrainWait());
        }
    }

    IEnumerator TrainWait()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("isOnTracks", true);          // Sets boolean to true so animation can play
        yield return new WaitForEndOfFrame();
        GetComponent<Animator>().SetBool("isOnTracks", false);          // Sets boolean to true so animation can play
    }
}
