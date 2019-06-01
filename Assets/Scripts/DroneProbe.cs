using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneProbe : MonoBehaviour
{
    bool playerDetected = false;
    Animator turretAnimator;
    Animator probeAnimator;

    private void Start()
    {
        turretAnimator = GameObject.Find("TurretUpper").GetComponent<Animator>();
        probeAnimator = GameObject.Find("VisionCone").GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerDetected == true)
        {
            GameObject.Find("DroneLight").transform.LookAt(GameObject.Find("Player/PlayerRig/mixamorig:Hips").transform);
            //GameObject.Find("DroneLight").transform.rotation = Quaternion.Euler(GameObject.Find("VisionCone").transform.rotation.x, GameObject.Find("VisionCone").transform.rotation.y + 180f, GameObject.Find("VisionCone").transform.rotation.z);
        }
    }

    IEnumerator DroneFlash()
    {
        //yield return new WaitForEndOfFrame();
        //Debug.Log("ríða");

        yield return new WaitForSeconds(0.2f);
        probeAnimator.SetBool("Gay", false);
        yield return new WaitForSeconds(0.2f);
        probeAnimator.SetBool("Gay", true);
        GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().deathToggle = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            probeAnimator.SetBool("Gay", true);
            Debug.Log("eat my ass");
            turretAnimator.enabled = false;
            playerDetected = true;
            StartCoroutine(DroneFlash());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //StopAllCoroutines();
        }
    }
}
