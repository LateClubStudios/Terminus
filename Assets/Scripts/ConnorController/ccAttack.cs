using System.Collections;
using UnityEngine;

public class ccAttack : MonoBehaviour
{

    public static Bandits banditScript;
    public static bool attackActive = false;
    bool bandit = false;
    public static GameObject banditObj;
    bool attackCooldown = false;

    private void Update()
    {
        Debug.Log(string.Format("Bandit: {0}", banditObj));

        if (Input.GetButtonDown("Attack") && attackCooldown == false)
        {
            attackActive = true;
            GetComponent<ccLocks>().crouchSwitch = false;
            GetComponent<ccLocks>().jumpSwitch = false;
            GetComponent<ccLocks>().rotationSwitch = false;
            GetComponent<Animator>().SetBool("isAttack", true);
            attackCooldown = true;
            Invoke("ResetCooldown", 0.1f);
            float floatRand = Random.Range(1f, 3f);
            float intRound = Mathf.Round(floatRand);

            GetComponent<Animator>().SetFloat("Attack", intRound);

            if (bandit == true && banditObj != null)
            {
                banditScript = banditObj.GetComponent<Bandits>();
            }
        }

        if (Input.GetButtonUp("Attack"))
        {
            GetComponent<Animator>().SetBool("isAttack", false);
            StartCoroutine("AttackTimer");
        }
    }

    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(1.0f);
        attackActive = false;
        //movementSwitch = true;
        GetComponent<ccLocks>().crouchSwitch = true;
        GetComponent<ccLocks>().jumpSwitch = true;
        GetComponent<ccLocks>().rotationSwitch = true;
    }

    void ResetCooldown()
    {
        attackCooldown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bandit")
        {
            banditObj = other.gameObject;
            Debug.Log("Player At Bandit");
            bandit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.tag == "Bandit")
        {
            banditObj = null;
            Debug.Log("Player At Bandit");
            bandit = false;
        }
    }
}
