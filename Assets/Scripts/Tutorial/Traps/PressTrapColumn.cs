using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressTrapColumn : MonoBehaviour
{
    private EdgeCollider2D trigCollider;
    private EdgeCollider2D edgeCollider;
    private Rigidbody2D body;
    private float startY;
    public bool isReverse;
    private Vector2[] points;
    private float upperLim;
    private float lowerLim;
    public float incRate;
    public float decRate;
    private float upperOffset;
    public float lowerOffset;
    public float posSpeed;
    public float negSpeed;
    private bool ToF;
    private bool ToF1;
    private bool inCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        inCoroutine = false;
        startY = gameObject.transform.localPosition.y;
        body = GetComponent<Rigidbody2D>();
        foreach (var collider in gameObject.GetComponents<EdgeCollider2D>())
        {
            if (collider.isTrigger)
            {
                trigCollider = collider;
            }
            else
            {
                edgeCollider = collider;
            }
        }
        points = edgeCollider.points;
        if (isReverse)
        {
            upperLim = 2f;
            lowerLim = -3.8f;
            incRate = 0.065f;
            decRate = 1.3f;
            upperOffset = -0.4f;
            lowerOffset = -8.09f;
            posSpeed = 20f;
            negSpeed = -1f;
            ToF = true;
            ToF1 = false;
        }
        else
        {
            upperLim = 3.8f;
            lowerLim = -2f;
            incRate = 1.3f;
            decRate = 0.065f;
            upperOffset = 8.09f;
            lowerOffset = 0.4f;
            posSpeed = 1f;
            negSpeed = -20f;
            ToF = false;
            ToF1 = true;
        }
    }

    private void Update()
    {

        if (body.velocity.y < 0 && points[1].y < upperLim)
        {
            points[1] += new Vector2(0, incRate);
            points[2] += new Vector2(0, incRate);
            edgeCollider.points = points;
        }
        else if (body.velocity.y >= 0 && points[1].y > lowerLim)
        {
            points[1] -= new Vector2(0, decRate);
            points[2] -= new Vector2(0, decRate);
            edgeCollider.points = points;
        }
   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inCoroutine)
        {
            if (gameObject.transform.localPosition.y <= startY + lowerOffset)
            {
                if (isReverse)
                {
                    StartCoroutine(Wait(ToF, posSpeed));
                }
                else
                {
                    trigCollider.enabled = ToF;
                    body.velocity = new Vector2(0, posSpeed);
                }


            }
            else if (gameObject.transform.localPosition.y >= startY + upperOffset)
            {
                if (!isReverse)
                {
                    StartCoroutine(Wait(ToF1, negSpeed));
                }
                else
                {
                    trigCollider.enabled = ToF1;
                    body.velocity = new Vector2(0, negSpeed);
                }

            }
        }

        

    }

    IEnumerator Wait(bool _tof, float _speed)
    {
        inCoroutine = true;
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        trigCollider.enabled = _tof;
        body.velocity = new Vector2(0, _speed);
        if (isReverse)
        {
            yield return new WaitUntil(() => gameObject.transform.localPosition.y >= startY + upperOffset);
        }
        else
        {
            yield return new WaitUntil(()=> gameObject.transform.localPosition.y <= startY + lowerOffset);    
        }
        inCoroutine = false;
    }

}
