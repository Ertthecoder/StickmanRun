using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuillotineBlade : MonoBehaviour
{
    float upPos;
    float downPos;
    Rigidbody2D body;
    bool isWaiting;
    // Start is called before the first frame update
    void Start()
    {
        isWaiting = false;
        upPos = 5.28f;
        downPos = -0.7f;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.localPosition.y <= downPos && !isWaiting)
        {
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        isWaiting = true;
        body.velocity = new Vector2(0,1);
        yield return new WaitUntil(()=> gameObject.transform.localPosition.y >= upPos);
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        body.velocity = new Vector2(0, -9.81f);
        isWaiting = false;
    }
}
