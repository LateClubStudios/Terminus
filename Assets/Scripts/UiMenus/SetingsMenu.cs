// Morgan Joshua Finney
// 16-11-18
// For Project Hiro
// Controlles the settings of the game.
// https://youtu.be/YOaYQrN1oYQ

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetingsMenu : MonoBehaviour {

	public AudioMixer masterMixer;
	Resolution[] resolutions;
	public Dropdown resolutionDropDown;

	void Start ()
	{
		resolutions = Screen.resolutions;
		resolutionDropDown.ClearOptions();

		List<string> options = new List<string> ();
		int currentResolutionIndex = 0;
		for (int i = 0; i < resolutions.Length; i++)
		{
			string option = resolutions[i].width + "x" + resolutions[i].height;
			options.Add(option);

			if (resolutions [i].width == Screen.currentResolution.width && resolutions [i].height == Screen.currentResolution.height) {
				currentResolutionIndex = i;
			}

		}

		resolutionDropDown.AddOptions(options);
		resolutionDropDown.value = currentResolutionIndex;
		resolutionDropDown.RefreshShownValue();
	}

	public void SetResolution (int resolutionIndex)
	{
		Resolution resolution = resolutions [resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}

	public void SetVolumeMaster(float volumeMaster)
    {
		Debug.Log("Volume master is set to - " + volumeMaster);
		masterMixer.SetFloat("masterVol", volumeMaster);
    }

	public void SetVolumeAtmospheric(float volumeAtmospheric)
	{
		Debug.Log("Volume atmospheric is set to - " + volumeAtmospheric);
		masterMixer.SetFloat("atmosphericVol", volumeAtmospheric);
	}


	public void SetVolumeMusic(float volumeMusic)
	{
		Debug.Log("Volume music is set to - " + volumeMusic);
		masterMixer.SetFloat("musicVol", volumeMusic);
	}


	public void SetQuality(int quality)
	{
		QualitySettings.SetQualityLevel(quality);
		Debug.Log("Quality is set to - " + quality);
	}

	public void SetFullScreen(bool isFullScreen)
	{
		Screen.fullScreen = isFullScreen;
		Debug.Log("FullScreen is set to - " + isFullScreen);
	}
}
