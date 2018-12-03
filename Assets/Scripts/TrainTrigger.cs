using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour {
    // Public variables. Gets the player and the train animator.
    public GameObject player;
    public Animator trainAnim;

    // Values for starting player position
    Vector3 playerOriginalPos;

    private void Start()
    {
        trainAnim = GetComponent<Animator>(); // Gets the animator
        trainAnim.SetBool("isOnTracks", false); // Parameter for the animator is set to true. If set to false, the train animation will play once and won't play again unless the player steps into the trigger.
        playerOriginalPos = player.transform.position; // Stores values for player's starting position
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            trainAnim.SetBool("isOnTracks", true); // Sets boolean to true so animation can play
            trainAnim.Play("Train"); // Train animation is played so the player encounters the train immediately
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            player.transform.position = playerOriginalPos; // If the train hits the player, the player is reset to the start
            trainAnim.SetBool("isOnTracks", false);
        }
    }
}
