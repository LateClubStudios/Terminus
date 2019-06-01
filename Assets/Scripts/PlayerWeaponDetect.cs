using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponDetect : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BanditRagdoll")
        {
            
            Debug.Log("Luigi laughs at your existence");

            if (ccAttack.attackActive == true)
            {
                Debug.Log("The National Homeland of Security have issued a Bruh Moment warning in the following districts: Ligma, Sugma, Bofa, and Sugondese");
                ccAttack.banditScript.banditDeath = true;
            }
        }
    }
}
