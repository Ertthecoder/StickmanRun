using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using Unity.Mathematics;

public class TrapRetract : MonoBehaviour
{
    public Rigidbody2D body;
    private SliderJoint2D slider;
    private float initialY;
    public float lowerLimitY;
    public float upperLimitY;
    public float delayTime = 2.0f; // Delay time in seconds
    public float speed;
    public float motorForce;
    public bool reverse = false;
    private bool isRetracting = false;

    void Awake()
    {
        initialY = transform.position.y; // Store the initial Y position
        slider = body.GetComponent<SliderJoint2D>();
    }

    private void Start()
    {
        if (reverse)
        {
            speed *= -1;
        }
    }

    void Update()
    {


        if (!isRetracting && transform.position.y <= lowerLimitY)
        {            
            slider.motor = new JointMotor2D { motorSpeed = speed * -1, maxMotorTorque = motorForce };
            if (!reverse)
            {
                isRetracting = true;
                StartCoroutine(RetractionDelay());
            }
                                    
        }

        else if (!isRetracting && transform.position.y >= upperLimitY)
        {
            slider.motor = new JointMotor2D { motorSpeed = speed, maxMotorTorque = motorForce };

            if (reverse)
            {
                isRetracting = true;
                StartCoroutine(RetractionDelay());
            }
        }
    }

    // Coroutine to handle retraction delay
    IEnumerator RetractionDelay()
    {
        yield return new WaitForSeconds(delayTime);

        isRetracting = false;
    }
}
