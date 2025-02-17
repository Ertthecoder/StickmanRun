using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float launchForce;
    public float x = -1f;
    public float y = 1f;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.rigidbody.AddForce(new Vector2(x, y) * launchForce, ForceMode2D.Impulse);
        }
    }
}
