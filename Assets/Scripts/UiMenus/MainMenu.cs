//* Morgan Joshua Finney
//* For Project Hiro
//* 15-11-18
//* main menu scripts

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

	bool SettingsActive = false;

	public static bool optionsIsOn = false;

	public GameObject optionsMenuUI;

    public static bool exitCheckIsOn = false;

    public GameObject exitCheckUI;

    public Animator transAnim;

	public void LoadOptions()
	{
		Debug.Log ("loading option");

		if (optionsIsOn)
		{
			OptionsOn();
		}
		else
		{
			OptionsOff();
            ExitCheckOn();
        }
	}


	void OptionsOn()
	{
		optionsMenuUI.SetActive(false);
		optionsIsOn = false;
	}

	void OptionsOff()
	{
		optionsMenuUI.SetActive(true);
		optionsIsOn = true;
	}

    //* function called from button - closes game
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
            OptionsOn();
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

	void Start()
	{
		SettingsActive = false;
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