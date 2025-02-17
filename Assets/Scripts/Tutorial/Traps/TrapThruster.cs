using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapThruster : MonoBehaviour
{
    private Health healthScript;
    public GameObject player;

    private void Start()
    {
        healthScript = player.GetComponent<Health>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !healthScript.isInvincible)
        {
            collision.attachedRigidbody.AddForce(new Vector2(0,5),ForceMode2D.Impulse);
        }
    }
}
