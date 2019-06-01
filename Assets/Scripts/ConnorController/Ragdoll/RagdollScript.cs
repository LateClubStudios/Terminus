//* Turns animplayer into ragdoll
//* Orginally by Perttu Hämäläinen Oct 2010
//* Converted by Morgan Joshua Finney Mar 2019 for Outnumbered Project


using UnityEngine;
using System.Collections;

public class RagdollScript : MonoBehaviour {
	//Helper to set the isKinematc property of all RigidBodies in the children of the 
	//game object that this script is attached to
	void SetKinematic(bool newValue)
	{
		//Get an array of components that are of type Rigidbody
		Rigidbody[] bodies=GetComponentsInChildren<Rigidbody>();

		//For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
		foreach (Rigidbody rb in bodies)
		{
			rb.isKinematic=newValue;
		}
	}
	// Use this for initialization
	void Start () {
		//Set all RigidBodies to kinematic so that they can be controlled with Mecanim
		//and there will be no glitches when transitioning to a ragdoll
		SetKinematic(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find("Player/PlayerRig").GetComponent<ccDeath>().ragdollMe == false)
		{
			//SetKinematic(false);
			GetComponent<Animator>().enabled=false;
		}
	}
}
