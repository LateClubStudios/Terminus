using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour {

    public GameObject player;
    public Animator trainAnim;

    Vector3 playerOriginalPos;
    // Use this for initialization

    private void Start()
    {
        trainAnim = GetComponent<Animator>();
        trainAnim.SetBool("isOnTracks", true);
        playerOriginalPos = player.transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            trainAnim.SetBool("isOnTracks", true);
            trainAnim.Play("Train");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            player.transform.position = playerOriginalPos;
        }
    }
}
