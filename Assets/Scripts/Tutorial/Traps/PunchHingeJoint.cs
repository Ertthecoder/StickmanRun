using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PunchHingeJoint : MonoBehaviour
{
    public Rigidbody2D conBody;
    HingeJoint2D hinge;
    JointMotor2D motor;
    public bool isRectracted {  get; private set; }
    public bool isReverseJoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        isRectracted = false;
        foreach (var hingeJ in gameObject.GetComponents<HingeJoint2D>())
        {
            if (hingeJ.connectedBody == conBody)
            {
                hinge = hingeJ;
                motor = hingeJ.motor;
                speed = motor.motorSpeed;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hinge.jointAngle <= hinge.limits.min + 0.1f)
        {
            if (isReverseJoint)
            {
                motor.motorSpeed = -speed/8;                
            }
            else if(!isRectracted)
            {
                StartCoroutine(Wait());
                motor.motorSpeed = speed;
            }           
            hinge.motor = motor;            
        }
        else if (hinge.jointAngle >= hinge.limits.max - 0.1f)
        {
            if (isReverseJoint && !isRectracted)
            {
                StartCoroutine(Wait());
                motor.motorSpeed = speed;
            }
            else
            {
                motor.motorSpeed = -speed/8;
            }
            hinge.motor = motor;
        }
    }

    IEnumerator Wait()
    {
        isRectracted = true;
        yield return new WaitForSeconds(3);
        isRectracted = false;
    }
}
