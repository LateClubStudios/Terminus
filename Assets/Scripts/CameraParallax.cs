using UnityEngine;
using System.Collections;
using UnityEditor;

public class CameraParallax : MonoBehaviour
{
    

    public float speedH = 0.5f;
    public float speedV = 0.5f;

    private float yaw = 235.0f;
    private float pitch = 5.0f;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector2 mousePos = Input.mousePosition;
    }

    void Update()
    {
        StartCoroutine("CentreMouseCursor");
        //yaw += speedH * Input.GetAxis("Mouse X");
        //pitch -= speedV * Input.GetAxis("Mouse Y");

        //Camera.main.transform.eulerAngles = new Vector3 (Mathf.Clamp(pitch, 0, 10), Mathf.Clamp(yaw, 230, 240), 0);

        


        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;
        float screenX = Screen.width;
        float screenY = Screen.height;

        if (mouseX < 0 || mouseX > screenX || mouseY < 0 || mouseY > screenY)
        {
            Debug.Log("The mouse has left the screen. Resetting mouse cursor to centre.");
            Cursor.lockState = CursorLockMode.Locked;
            yaw = 235.0f;
            pitch = 5.0f;
            StartCoroutine("CentreMouseCursor"); 
        }
            
    }

    IEnumerator CentreMouseCursor()
    {
        yield return new WaitForSeconds(0.5f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        Camera.main.transform.eulerAngles = new Vector3(Mathf.Clamp(pitch, 0, 10), Mathf.Clamp(yaw, 230, 240), 0);

    }
}