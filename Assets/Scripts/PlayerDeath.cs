using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour {

    public Animator playerAnim;
    public static bool death;
    // Use this for initialization
    public Animator gOverAnim;

    private void Start()
    {
        death = false;
    }
    void Update ()
    {
        if (death == true)
        {
            PlayerController.gameIsOver = true;
            playerAnim.SetBool("isDead", true);
            StartCoroutine(gOverScene());
            
        }
        else if (death == false)
        {
            PlayerController.gameIsOver = false;
            playerAnim.SetBool("isDead", false);
        }
    }

    IEnumerator gOverScene()
    {
        gOverAnim.SetBool("Started", true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(4);
    }

}
