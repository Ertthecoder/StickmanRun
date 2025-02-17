using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth;
    public Animator animator;
    private SpriteRenderer spriteRenderer;
    public float invincibilityDuration;
    public bool isInvincible { get; private set; }
    private GameObject[] traps;
    private Rigidbody2D body;
    private void Awake()
    {
        currentHealth = startingHealth;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        traps = GameObject.FindGameObjectsWithTag("Trap");
        body = gameObject.GetComponent<Rigidbody2D>(); 
    }
    private void Start()
    {
        isInvincible = false;
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth -= _damage, 0, startingHealth);

        if (currentHealth > 0 && !isInvincible)
        {
            animator.SetTrigger("hurt");
        }
    }

    private void Update()
    {       
        
        if (currentHealth == 0 && !isInvincible)
        {
            // Respawning player with a invincibility for a short period of time
            // to avoid dying again if spawned on a trap

            Invincibility();

        }

    }

    IEnumerator Wait(float Seconds)
    {        
        isInvincible = true;
        animator.SetBool("isInvincible", true);
        yield return new WaitForSeconds(Seconds);
        isInvincible = false;
        animator.SetBool("isInvincible", false);
    }

    void Invincibility()
    {
      foreach (GameObject trap in traps)
            {
                trap.GetComponent<Collider2D>().enabled = false;
            }
            StartCoroutine(Wait(invincibilityDuration)); 


      foreach (GameObject trap in traps)
            {
                trap.GetComponent<Collider2D>().enabled = true;
            }
            currentHealth = startingHealth;
            
    }

}
