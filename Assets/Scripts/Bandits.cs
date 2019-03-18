using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandits : MonoBehaviour
{

    bool attackReady = false;
    float time = 0.0f;
    bool timerOn = false;
    static public bool banditDeath = false;
    public GameObject hips;

    private void Start()
    {
        //GetComponent<CapsuleCollider>().enabled = true;
        //GetComponent<Animator>().enabled = true;
        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;

        //GetComponent<SphereCollider>().enabled = false;
        //hips.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Bandit Script Running");
        if (attackReady == true && timerOn == false)
        {
            StartCoroutine(AttackTimer());
        }

        if (banditDeath == true)
        {
            GetComponent<CapsuleCollider>().enabled = false;
    
            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            GetComponent<SphereCollider>().enabled = true;
            hips.SetActive(true);

            GetComponent<Animator>().enabled = false;
        }
    }

    IEnumerator AttackTimer()
    {
        Debug.Log("Bandit Attack Timer Start");
        timerOn = true;
        time = Random.Range(00.0f, 05.0f);
        yield return new WaitForSeconds(time);
        Debug.Log(string.Format("Bandit Attacked Player with {0} secs", time));
        //Attack anim of bandit
        PlayerController.death = true;
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = null;
        GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().agent.SetDestination(transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Bandit Reached Player");
            attackReady = true;
        }
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Player")
        {
            Debug.Log("Bandit Reached Player");
            attackReady = true;
        }
    }
}