using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/318766/use-gamepad-right-analog-stick-instead-of-mouse-to.html
// http://wiki.unity3d.com/index.php/Xbox360Controller

 
 public class MouseJoystickOveride : MonoBehaviour {

	public float speed;
 
	public Transform target;

    private void Update()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X");
        float mouseVertical = Input.GetAxis("Mouse Y");

        Vector3 mouseMovement = new Vector3(0.0f, mouseVertical, mouseHorizontal);

        transform.Translate(mouseMovement * speed);
    }

    void FixedUpdate() {

		float moveHorizontal = Input.GetAxis ("xboxl");
		float moveVertical = Input.GetAxis ("xboxr");

		Vector3 movement = new Vector3 (0.0f, moveVertical, -moveHorizontal);

		transform.Translate(movement * speed);

        OnDrawGizmosSelected ();
	}  

    void OnDrawGizmosSelected()
	{
		if (target != null) {
			// Draws a blue line from this transform to the target
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (transform.position, target.position);
		}
	}



 }
