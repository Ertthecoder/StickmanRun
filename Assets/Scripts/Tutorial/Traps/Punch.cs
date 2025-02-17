using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    private Rigidbody2D body;
    public float punchSpeed;
    public float retractSpeed;
    bool isRetracted;
    Time punchTime;
    bool timerStart;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.velocity = new Vector2(-punchSpeed,0);
        isRetracted = false;
        timerStart = false;
    }
    private void FixedUpdate()
    {
        
        if (gameObject.transform.localPosition.x >= 0.404f && !isRetracted)
        {               
            StartCoroutine(Wait());           
        }
        if (gameObject.transform.localPosition.x <= -1.5f)
        {
            body.velocity = new Vector2(retractSpeed, 0);
        }
    }

    IEnumerator Wait()
    {
        isRetracted = true;
        body.velocity = new Vector2(0,0);        
        yield return new WaitForSeconds(3);
        body.velocity = new Vector2(-punchSpeed, 0);
        gameObject.transform.localPosition = new Vector3(0.403f, 0, 0);
        isRetracted = false;        
    }
}
