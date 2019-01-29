//* Main Menu UI Controller
//* Morgan Joshua Finney
//* Sep 18 Through Jan 19
//* For NextGen Synoptic Project Game Outnumbered

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


	public static bool optionsIsOn = false;
	public GameObject optionsMenuUI;


    public static bool startButtonIsOn = false;
    public GameObject startButtonUI;


    public static bool exitCheckIsOn = false;
    public GameObject exitCheckUI;



    public Animator transAnim;



    // Attached to button Options
	public void LoadOptions()
	{
		Debug.Log ("loading option");

		if (optionsIsOn)
		{
			OptionsOn();
            StartOff();

        }
		else
		{
			OptionsOff();
            ExitCheckOn();
            StartOn();
        }
	}

    //* function called from button - closes game
    public void QuitGame()
    {
        Debug.Log("loading exit check");
        if (exitCheckIsOn)
        {
            ExitCheckOn();
            StartOff();
        }
        else
        {
            ExitCheckOff();
            OptionsOn();
            StartOn();
        }
    }

    // function to turn off options when on
    void OptionsOn()
	{
		optionsMenuUI.SetActive(false);
		optionsIsOn = false;
	}

    // function to turn on options when off
    void OptionsOff()
	{
		optionsMenuUI.SetActive(true);
		optionsIsOn = true;
	}



    // function to turn off exit when on
    void ExitCheckOn()
    {
        Debug.Log("turning off");
        exitCheckUI.SetActive(false);
        exitCheckIsOn = false;
    }

    // function to turn on exit when off
    void ExitCheckOff()
    {
        Debug.Log("turning on");
        exitCheckUI.SetActive(true);
        exitCheckIsOn = true;
    }

    // function to turn off exit when on
    void StartOn()
    {
        Debug.Log("turning off start");
        startButtonUI.SetActive(false);
        startButtonIsOn = false;
        MainMenu_Start.UiOver = true;
    }

    // function to turn on exit when off
    void StartOff()
    {
        Debug.Log("turning on start");
        startButtonUI.SetActive(true);
        startButtonIsOn = true;
        MainMenu_Start.UiOver = false;
    }




    public void startGame()
	{
		StartCoroutine (fadeLoad ());
	}

	IEnumerator fadeLoad()
	{
		transAnim.SetBool ("Started", true);
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene(1);
	}
}