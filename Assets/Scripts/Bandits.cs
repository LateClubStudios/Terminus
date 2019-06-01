using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandits : MonoBehaviour
{ 
    bool attackReady = false;
    float time = 0.0f;
    bool timerOn = false;
    public bool banditDeath = false;
    public GameObject hips;
    private void Start()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Animator>().enabled = true;
        GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        GetComponent<SphereCollider>().enabled = false;
        ragdollToggle = false;
    }
    bool ragdollToggle = false;
    // Update is called once per frame
    void Update()
    {

        if (ragdollToggle == true)
        {
            Collider[] bodies = hips.GetComponentsInChildren<Collider>();

            //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
            foreach (Collider rb in bodies)
            {
                rb.isTrigger = false;
            }
        }

        else if (ragdollToggle == false)
        {
            Collider[] bodies = hips.GetComponentsInChildren<Collider>();
            Rigidbody[] bodiesMore = hips.GetComponentsInChildren<Rigidbody>();

            //For each of the components in the array, treat the component as a Rigidbody and set its isKinematic property
            foreach (Collider rb in bodies)
            {
                rb.isTrigger = true;
            }
            foreach (Rigidbody col in bodiesMore)
            {
                col.velocity = Vector3.zero;
                col.angularVelocity = Vector3.zero;

            }
        }


        //Debug.Log("Bandit Script Running");
        if (attackReady == true && timerOn == false)
        {
            StartCoroutine(AttackTimer());
        }

        if (banditDeath == true && ccAttack.banditObj != null)
        {
            if (ragdollToggle == false)
            {
                ccAttack.banditObj = null;
            }
            GetComponentInChildren<MeshCollider>().enabled = false;

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            GetComponent<CapsuleCollider>().enabled = false;
            

            GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            GetComponent<SphereCollider>().enabled = true;
            ragdollToggle = true;

            GetComponent<Animator>().enabled = false;

            
        }
    }

    IEnumerator AttackTimer()
    {
        Debug.Log("Bandit Attack Timer Start");
        timerOn = true;
        time = Random.Range(05.0f, 10.0f);
        //yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));
        
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().SetBool("isAttacking", true);
        yield return new WaitForSeconds(2.0f);
        GetComponent<Animator>().SetBool("isAttacking", false);
        //Attack anim of bandit
        if (banditDeath == false)
        {
            Debug.Log(string.Format("Bandit Attacked Player with {0} secs", time));
            GameObject.Find("Player/PlayerRig").GetComponent<ccLocks>().deathToggle = true;
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = null;
            GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().agent.SetDestination(transform.position);
        }
        else
        {
            Debug.Log(string.Format("Player fuking legend killed bandit first."));
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Bandit Reached Player");
            attackReady = true;
        }

        //if (other.tag == "PlayerWeapon")
        //{
        //    banditDeath = true;
        //}
    }

    void OnCollisionEnter(Collision hit)
    {
        if (hit.transform.gameObject.tag == "Player")
        {
            Debug.Log("Bandit Reached Player");
            attackReady = true;
        }

        //if (hit.transform.tag == "PlayerWeapon")
        //{
        //    banditDeath = true;
        //}
    }
}

// -- TODO --
// *** Make the player face the bandit to attack the bandit *** FIXED
// Use enumerators to delay the player's attack so it looks more realistic, as well as the bandit's attack, and the bandit's death
// Make the weapons and tag them, then make the bandits/player die when the weapon collides with them AND if attack is active