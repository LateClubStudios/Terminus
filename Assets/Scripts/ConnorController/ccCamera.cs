using UnityEngine;

public class ccCamera : MonoBehaviour
{
    float FovSpeed = 0.015f, trackSpeed = 0.1f, cameraPosX, cameraPosY, cameraPosZ;
    Vector3 lastFramePosition;

    void Update()
    {
        CameraTrackSystem();
        CameraFovSystem();
    }

    void CameraTrackSystem()
    {
        //* Makes Camera Follow the player with delay
        //cameraPosX = Mathf.Lerp(cameraPosX, GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position.x, trackSpeed);
        cameraPosY = Mathf.Lerp(cameraPosY, GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position.y, trackSpeed);
        cameraPosZ = Mathf.Lerp(cameraPosZ, GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform.position.z, trackSpeed);

        GameObject.Find("Player/MainCameraHolder").transform.position = new Vector3(GameObject.Find("Player/MainCameraHolder").transform.position.x, cameraPosY - 1, cameraPosZ);
    }

    void CameraFovSystem()
    {
        //* Makes camera zoom in with player
        if (transform.position.x < 1 && transform.position.x > -1)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 65, FovSpeed);
        }
        else if (transform.position.x > 2)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 75, FovSpeed);
        }
        else if (transform.position.x < -3.5)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30, FovSpeed);
        }

        if (transform.position.x < lastFramePosition.x - 0.0005)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 30, FovSpeed);
        }
        else if (transform.position.x > lastFramePosition.x + 0.0005)
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 75, FovSpeed);
        }
        lastFramePosition = transform.position;
    }
}