using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingBullet : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody2D rBody;
    private bool isStill;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = gameObject.transform.localPosition;
        rBody = gameObject.GetComponent<Rigidbody2D>();
        isStill = true;
        rBody.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStill)
        {
            if (gameObject.transform.localScale.y == -1f)
            {
                StartCoroutine(Shoot(1f));
            }
            else
            {
                StartCoroutine(Shoot(-1f));
            }            
        }

        if (!isStill && math.abs(startPos.y - gameObject.transform.localPosition.y) > 12)
        {            
            StartCoroutine(Respawn());   
        }
    }
    IEnumerator Shoot(float direction)
    {
        isStill = false;
        
        gameObject.transform.localPosition += new Vector3(0f,direction * 0.45f,0f);
        yield return new WaitForSeconds(0.5f);
        rBody.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        rBody.AddForce(new Vector2(0f, direction * speed), ForceMode2D.Impulse);                
    }
    IEnumerator Respawn()
    {
        gameObject.transform.localPosition = startPos;
        rBody.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(1.5f);
        isStill = true;
    }
}
