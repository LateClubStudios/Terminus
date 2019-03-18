using System.Collections;
using UnityEngine;

public class MaintenanceFunctionController : MonoBehaviour {

    //* 0.9216721 players y hight

    public Animator playerAnimator;
    public GameObject playerRagdollHips;
    public CapsuleCollider playerCollider;
    public Rigidbody playerRB;

    bool skip = false;

	// Use this for initialization
	void Start () {
        PlayerController.lockOutAll = true;
        playerAnimator.enabled = false;
        playerRagdollHips.SetActive(false);
        StartCoroutine(Delay00());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            skip = true;
        }

    }

    IEnumerator Delay00()
    {
        yield return new WaitForSeconds(1.0f);
        if (skip == true)
        {
            StartCoroutine(Delay01());
        }
        else
        {
            playerCollider.enabled = true;
            playerRB.isKinematic = false;
            playerRagdollHips.SetActive(false);
            playerAnimator.enabled = true;
            playerAnimator.SetBool("animaWakeUp", false);
            PlayerController.lockOutAll = false;

        }
    }

    IEnumerator Delay01()
    {
        yield return new WaitForSeconds(3.0f);
        playerRB.isKinematic = true;
        playerCollider.enabled = false;
        playerAnimator.enabled = false;
        playerRagdollHips.SetActive(true);
        StartCoroutine(Delay02());
    }

    IEnumerator Delay02()
    {
        yield return new WaitForSeconds(2.0f);
        playerCollider.enabled = true;
        playerRB.isKinematic = false;
        playerRagdollHips.SetActive(false);
        playerAnimator.enabled = true;
        playerAnimator.SetBool("animaWakeUp", true);
        StartCoroutine(Delay03());
    }

    IEnumerator Delay03()
    {
        yield return new WaitForSeconds(6.0f);
        playerAnimator.SetBool("animaWakeUp", false);
        yield return new WaitForSeconds(6.0f);
        PlayerController.lockOutAll = false;
    }

}
