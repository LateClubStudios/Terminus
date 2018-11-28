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

	//* function called from button - closes game
    public void QuitGame()
    {
        Application.Quit ();
    }
}