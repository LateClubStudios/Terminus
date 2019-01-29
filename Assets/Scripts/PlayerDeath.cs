//* Global death - death bool to be set true from other scripts. 
//* Morgan Joshua Finney & Josh Lennon 
//* Jan 19
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    public Animator playerAnim;
    public static bool death;
    public GameObject hips;
    // Use this for initialization

    private void Start()
    {
        death = false;
        // playerAnim.enabled = true;


    }
    void Update ()
    {
        if (death == true)
        {
            PlayerController.gameIsOver = true;
            playerAnim.enabled = false;
            // turn on all child rbs and coliders and joints
            hips.SetActive(true);
            StartCoroutine(gOverScene());
            
        }
        else if (death == false)
        {
            PlayerController.gameIsOver = false;
            playerAnim.enabled = true;
            // turn off all child rbs and coliders and joints
            hips.SetActive(false);
        }
    }

    IEnumerator gOverScene()
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(4);
    }

}
