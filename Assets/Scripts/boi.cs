using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boi : MonoBehaviour {
    public float thrust = 3;
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        this.rb.AddRelativeForce(this.transform.forward * thrust);
    }
}
