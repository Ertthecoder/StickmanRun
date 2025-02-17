using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingTraps : MonoBehaviour
{
    public float offTime;
    public float onTime;
    private SpriteRenderer sRenderer;
    private Collider2D colliders;
    private Animator animator;
    private bool ignited = false;

    private void Awake()
    {
        sRenderer = GetComponent<SpriteRenderer>();
        colliders = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!ignited)
        {
            StartCoroutine(Delay());

        }

    }

    IEnumerator Delay()
    {
        ignited = true;

        sRenderer.enabled = false;
        colliders.enabled = false;
        animator.enabled = false;
        yield return new WaitForSeconds(offTime);


        sRenderer.enabled = true;
        colliders.enabled = true;
        animator.enabled = true;
        animator.SetTrigger("ignition");
        yield return new WaitForSeconds(onTime);
        ignited = false;

    }
}
