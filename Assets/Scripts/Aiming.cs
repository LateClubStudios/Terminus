using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://answers.unity.com/questions/318766/use-gamepad-right-analog-stick-instead-of-mouse-to.html
// http://wiki.unity3d.com/index.php/Xbox360Controller

 
 public class MouseJoystickOveride : MonoBehaviour {
 
	public Transform target;
    public float distance = 1.5f;

    private void Update()
    {
		OnDrawGizmosSelected ();
        ObjectOnCursor();
    }

    void OnDrawGizmosSelected()
	{
		if (target != null) {
			// Draws a blue line from this transform to the target
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (transform.position, target.position);
		}
	}

    void ObjectOnCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 point = ray.origin + (ray.direction * distance);

        target.position = point;
    }


 }
