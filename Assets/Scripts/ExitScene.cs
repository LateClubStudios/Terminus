using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour {

    bool exitActive = false;
    Animator transAnim;

    private void Start()
    {
        transAnim = GameObject.Find("FadePanel").GetComponent<Animator>();
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Inside quit area.");
            exitActive = true;
            ExitCoroutine();
        }
    }

    private void ExitCoroutine()
    {
        if (exitActive == true && Input.GetButtonDown("Grab"))
        {
            Debug.Log("Exiting scene.");
            StartCoroutine(SceneExit());
        }
    }

    IEnumerator SceneExit()
    {
        transAnim.SetBool("animaFadeOut", true);
        Destroy(GameObject.Find("PlayerRig").GetComponent<ccLocks>());
        GameObject.Find("PlayerRig").GetComponent<Animator>().SetFloat("Direction", 0);
        yield return new WaitForSeconds(5.0f);
        Debug.Log("The National Homeland of Security have issued a bruh moment warning");
        SceneManager.LoadScene(2);
    }
}



