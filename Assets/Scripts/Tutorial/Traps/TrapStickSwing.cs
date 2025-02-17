using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapStickSwing : MonoBehaviour
{
    private Rigidbody2D body;
    public float speed;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.angularVelocity = speed;        
    }
    private void FixedUpdate()
    {
        if (body.rotation >= gameObject.GetComponent<HingeJoint2D>().limits.max)
        {
            body.angularVelocity = -speed;
        }
        if(body.rotation <= gameObject.GetComponent<HingeJoint2D>().limits.min)
        {
            body.angularVelocity = speed;
        }
    }
}

    














