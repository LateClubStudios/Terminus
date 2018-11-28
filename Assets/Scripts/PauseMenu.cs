//* 22 NOV 2018
//* Morgan Joshua Finney
//* Outnumbered
//* used for the games pause menu functions


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool gameIsPaused = false;
    public static bool optionsIsOn = false;

    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;

    // Update is called once per frame
    void Update () {

		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gameIsPaused == true && optionsIsOn == false) {
				Resume ();
			} else if (gameIsPaused == false && optionsIsOn == false) {
				Pause ();
			} else {
				LoadOptions ();
			}
		
		}

	}


	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		gameIsPaused = false;
	}
	void Pause()
	{
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		gameIsPaused = true;
	}

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
        pauseMenuUI.SetActive(true);
        optionsMenuUI.SetActive(false);
        optionsIsOn = false;
    }

    void OptionsOff()
    {
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(true);
        optionsIsOn = true;
    }

	public void QuitGame()
	{
		Debug.Log ("loading quit");
		Time.timeScale = 1f;
		SceneManager.LoadScene ("MainMenu");
	}
}
