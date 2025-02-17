using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSideways : MonoBehaviour
{
    [SerializeField] private float movementDistance;
    [SerializeField] private float speed;
    private bool movingLeft;
    private float leftEdge;
    private float rightEdge;
    private float x;
    private float y;
    private float z;

    private void Awake()
    {
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    private void Update()
    {
        x = GetComponent<Transform>().position.x;
        y = GetComponent<Transform>().position.y;
        z = GetComponent<Transform>().position.z;
            
        transform.position = new Vector3(x, y, z);    

            if (movingLeft)
            {   
            
                if (transform.position.x > leftEdge)
                {
                    transform.position = new Vector3(x - speed * Time.deltaTime, y, transform.position.z);
                }
                else
                {
                    movingLeft = false;
                }
            }
            else 
            {
            if (transform.position.x < rightEdge)
                {
                    transform.position = new Vector3(x + speed * Time.deltaTime, y, transform.position.z);
                }
                else
                {
                    movingLeft = true;
                }
            }
        

    }



}
