using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRamp : MonoBehaviour
{
    private Health healthScript;
    private PlayerMovement playerMovement;
    public GameObject player;
    public float xBoost;
    private Vector2 launchForce;
    private void Start()
    {
        healthScript = player.GetComponent<Health>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        other.rigidbody.AddForce(new Vector2(xBoost, -1), ForceMode2D.Impulse);
    }
}
