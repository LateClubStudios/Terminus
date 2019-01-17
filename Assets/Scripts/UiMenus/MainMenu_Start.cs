//* Morgan Joshua Finney
//* For NextGen Outnumbered
//* 15-11-18
//* On hover makes the start button bold.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Start : MonoBehaviour {

	public GameObject startText;
	bool overStart = false;
	public Mesh text;
	public Mesh textbold;

	void OnMouseOver()
	{
		if (overStart == false) {
			GetComponent<MeshFilter>().sharedMesh = textbold;
			overStart = true;
		}
	}

	void OnMouseExit()
	{
		if (overStart == true) {
			GetComponent<MeshFilter>().sharedMesh = text;
			overStart = false;
		}
	}
}