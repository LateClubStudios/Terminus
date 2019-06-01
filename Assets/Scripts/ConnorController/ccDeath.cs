using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ccDeath : MonoBehaviour {

    public Animator transAnim;

    bool gameIsOver;

    public bool ragdollMe = false;


    private void Update()
    {
        RagdollSystem();
        DeathSystem();
    }

    private void RagdollSystem()
    {
        if (GetComponent<ccLocks>().ragdollToggle == true)
        {
            Collider[] bodies = GameObject.Find("Player/PlayerRig/mixamorig:Hips").GetComponentsInChildren<Collider>();

            //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
            foreach (Collider col in bodies)
            {
                col.isTrigger = false;
            }
            GetComponent<Rigidbody>().isKinematic = true;
            ragdollMe = true;
        }
        else if (GetComponent<ccLocks>().ragdollToggle == false)
        {
            Debug.Log("RAGoff");
            Collider[] bodies = GameObject.Find("Player/PlayerRig/mixamorig:Hips").GetComponentsInChildren<Collider>();

            //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
            foreach (Collider rb in bodies)
            {
                rb.isTrigger = true;
            }

            ragdollMe = false;
            GetComponent<Rigidbody>().isKinematic = false;

        }
    }


    private void DeathSystem()
    {
        if (GetComponent<ccLocks>().deathToggle == true)
        {
            gameIsOver = true;
            GetComponentInChildren<Collider>().enabled = false;
            GetComponent<ccLocks>().mainSwitch = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<ccLocks>().ragdollToggle = true;
            
            StartCoroutine(gOverScene());

        }
        else if (GetComponent<ccLocks>().deathToggle == false)
        {
            gameIsOver = false;
            GetComponent<CapsuleCollider>().enabled = true;
            GetComponentInChildren<Collider>().enabled = true;

        }
    }

    IEnumerator gOverScene()
    {
        transAnim.SetBool("animaFadeOut", true);
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene(5);
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Kill")
        {
            GetComponent<ccLocks>().deathToggle = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kill")
        {
            GetComponent<ccLocks>().deathToggle = true;
        }
    }
}
