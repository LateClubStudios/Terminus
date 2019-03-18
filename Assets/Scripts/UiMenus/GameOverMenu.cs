//* Morgan Joshua Finney
//* For Project Hiro
//* 11jan19
//* gameover menu scripts

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour {

    public static bool exitCheckIsOn = false;

    public GameObject exitCheckUI;

    public Animator transAnim;

    public void QuitGame()
    {
        Debug.Log("loading ecit check");
        if (exitCheckIsOn)
        {
            ExitCheckOn();
        }
        else
        {
            ExitCheckOff();
        }
    }

    void ExitCheckOn()
    {
        Debug.Log("turning off");
        exitCheckUI.SetActive(false);
        exitCheckIsOn = false;
    }

    void ExitCheckOff()
    {
        Debug.Log("turning on");
        exitCheckUI.SetActive(true);
        exitCheckIsOn = true;
    }

    public void startGame()
    {
        StartCoroutine(fadeLoad());
    }

    IEnumerator fadeLoad()
    {
        PlayerController.death = false;
        transAnim.SetBool("animaFadeOut", true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(1);
    }
}
