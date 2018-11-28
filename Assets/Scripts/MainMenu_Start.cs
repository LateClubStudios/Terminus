//* Morgan Joshua Finney
//* For Project Hiro
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
			// startText.transform.localScale += new Vector3(0.5f, 0.5f, 1f);
			Debug.Log ("mouse over start");
			overStart = true;
		}
	}

	void OnMouseExit()
	{
		if (overStart == true) {
			GetComponent<MeshFilter>().sharedMesh = text;
			// startText.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
			Debug.Log ("mouse over not start");
			overStart = false;
		}
	}
}