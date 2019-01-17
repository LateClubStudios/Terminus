//* Morgan Joshua Finney
//* For Outnumbered aka ProjectHiro
//* 10jan19
//* Exit Check UI

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitCheck : MonoBehaviour
{

    public Animator transAnim;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Restart()
    {
        StartCoroutine(fadeLoad());
    }

    IEnumerator fadeLoad()
    {
        transAnim.SetBool("Started", true);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
