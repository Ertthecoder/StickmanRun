using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TreeEditor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    [DoNotSerialize] public SpriteRenderer spriteR;
    private float playerScaleX;
    private float playerScaleY;
    private float playerScaleZ;
    public float playerPosX;
    public float playerPosY { get; private set; }
    public float playerPosZ { get; private set; }
    private Rigidbody2D body;
    private float playerScaleXPositive;
    private float playerScaleXNegative;

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float bootV;
    private static float _walkSpeed;
    private static float _runSpeed;
    private static float _jumpForce;
    private static float _bootV;

    private Animator animator;
    private bool grounded;
    private bool ramped;

    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public Vector3 offset;

    private bool isCrouchedWalking;
    public bool hasPowerUp;
    public bool hasBoots;
    public bool hasShield;
    private float playerStartX;
    private float playerStartY;
    private bool isSliding;
    public Sprite[] sprites;
    public Sprite[] spritesWBoots;
    private Health healthScript;
    public bool setBackPlayer;
    private float invincibilityDur;
    public float frozenTime;
    private float boundaryL;
    private float boundaryR;

    public GameObject shield;


    public void Awake()
    {    
        playerScaleX = gameObject.GetComponent<Transform>().localScale.x;
        playerScaleY = gameObject.GetComponent<Transform>().localScale.y;
        playerScaleZ = gameObject.GetComponent<Transform>().localScale.z;
        playerScaleXPositive = playerScaleX;
        playerScaleXNegative = playerScaleX * (-1);

        body = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        spriteR = gameObject.GetComponent<SpriteRenderer>();
        healthScript = gameObject.GetComponent<Health>();

        boundaryL = GameObject.Find("BoundaryL").transform.position.x;
        boundaryR = GameObject.Find("BoundaryR").transform.position.x;

        if (_walkSpeed != 0 && _runSpeed != 0 && _jumpForce != 0 && _bootV != 0)
        {
            walkSpeed = _walkSpeed;
            runSpeed = _runSpeed;
            jumpForce = _jumpForce;
            bootV = _bootV;            
        }

    }

    public void Start()
    {
        hasPowerUp = false;
        hasBoots = false;
        hasShield = false;
        isSliding = false;
        setBackPlayer = false;
        
        invincibilityDur = healthScript.invincibilityDuration;
        playerStartX = gameObject.transform.position.x;
        playerStartY = gameObject.transform.position.y;

        shield.GetComponent<Animator>().enabled = true;
    }
    public void Update()
    {
        playerPosX = gameObject.GetComponent<Transform>().localPosition.x;
        playerPosY = gameObject.GetComponent<Transform>().localPosition.y;
        playerPosZ = gameObject.GetComponent<Transform>().localPosition.z;

        // Get the keyboard input for horizontal movement

        horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
        verticalInput = UnityEngine.Input.GetAxisRaw("Vertical");

        // Flip player when turning rigth/left.
        
        if (horizontalInput > 0.01f && !isSliding)
        {
            transform.localScale = new Vector3(playerScaleXPositive, playerScaleY, playerScaleZ);
        }

        else if (horizontalInput < -0.01f && !isSliding)
        {
            transform.localScale = new Vector3(playerScaleXNegative, playerScaleY, playerScaleZ);
        }

        // Preventing player from getting out of the frame

        if (playerPosX < boundaryL)
        {
            transform.position = new Vector3(boundaryL + 0.1f, playerPosY, playerPosZ);
        }

        if (playerPosX > boundaryR)
        {
            transform.position = new Vector3(boundaryR - 0.1f, playerPosY, playerPosZ);
        }

        if (playerPosY < -8)
        {
            if (gameObject.transform.localRotation.z == 0)
            {
                transform.position = new Vector3(playerPosX, -7.359f, playerPosZ);
            }
            else
            {
                transform.position = new Vector3(playerPosX, -8f, playerPosZ);
            }
        }

        if (playerPosY > 4.5f)
        {
            transform.position = new Vector3(playerPosX, 4.5f, playerPosZ);
        }


        if (healthScript.isInvincible && !setBackPlayer)
        {
            if (transform.localScale.x > 0)
            {
                StartCoroutine(SetBackPenalty(-1f));
            }
            else if(transform.localScale.x < 0)
            {
                StartCoroutine(SetBackPenalty(+1f));
            }
            
        }

        // Crouch-ing/ed Walking

        animator.SetBool("isCrouching", verticalInput < -0.1f && isGrounded() && !isCrouchedWalking && !animator.GetBool("isRunning"));
        animator.SetBool("isCrouchedWalking", verticalInput < -0.1f && horizontalInput != 0 && isGrounded() && !animator.GetBool("isRunning"));
        isCrouchedWalking = animator.GetBool("isCrouchedWalking");

        // Setting player animation transitions and speed changes depending the input magnitude 

        animator.SetBool("isWalking", horizontalInput < -0.1f && !isCrouchedWalking || horizontalInput > 0.1f && !isCrouchedWalking);
        animator.SetBool("isRunning", (horizontalInput < -0.9f || horizontalInput > 0.9f) && !isCrouchedWalking);
        animator.SetBool("isGrounded", isGrounded());

            
            // To slide

        if (animator.GetBool("isRunning") && (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow) || UnityEngine.Input.GetKeyDown(KeyCode.S)) && isGrounded() && !isSliding) 
        {

            animator.SetTrigger("slideTrigger");
            if (horizontalInput < -0.01f)
            {
                StartCoroutine(SlideRotation(1f, -90f));
            }
            else if(horizontalInput > 0.01f)
            {
                StartCoroutine(SlideRotation(1f, 90f));
            }            

        }

        if (hasShield)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
        }


    }
    void FixedUpdate()
    {
        if ((horizontalInput < -0.1f && body.velocity.x > -walkSpeed || 
            horizontalInput > 0.1f && body.velocity.x < walkSpeed) && isGrounded())
        {
            body.AddForce(new Vector2(horizontalInput * walkSpeed, 0f), ForceMode2D.Impulse);
        }
        if ((horizontalInput < -0.9f && body.velocity.x > -runSpeed) && isGrounded() && !isCrouchedWalking || 
            (horizontalInput > 0.9f && body.velocity.x < runSpeed) && isGrounded() && !isCrouchedWalking)
        {
            body.AddForce(new Vector2(horizontalInput * runSpeed, 0f), ForceMode2D.Impulse);
        }
        if (verticalInput == 1 && isGrounded())
        {
            // Checking whether player is on the ground or not to prevent infinite jumping
            
            animator.SetTrigger("jumpTrigger");

            body.AddForce(new Vector2(horizontalInput * 0.2f, 1f) * jumpForce, ForceMode2D.Impulse);
                       
        }
        if (hasBoots)
        {
            if ((horizontalInput < -0.1f && body.velocity.x > -walkSpeed * bootV) || (horizontalInput > 0.1f && body.velocity.x < walkSpeed * bootV) && isGrounded())
            {
                body.AddForce(new Vector2(horizontalInput * walkSpeed * bootV, 0f), ForceMode2D.Impulse);
            }
            if ((horizontalInput < -0.9f && body.velocity.x > -runSpeed * bootV) || (horizontalInput > 0.9f && body.velocity.x < runSpeed * bootV) && isGrounded() && !isCrouchedWalking)
            {
                body.AddForce(new Vector2(horizontalInput * runSpeed * bootV, 0f), ForceMode2D.Impulse);
            }
        }


        //if (isGrounded())
        //{
        //    body.velocity *= 0.9f;
        //}
    }

    // Checking when player touches the ground to prevent infinite jumping

    private bool isGrounded()
    {
        if (transform.localScale.x < -0.01f)
        {
            offset.x = -0.125f;
        }
        else
        {
            offset.x = 0.125f;
        }
        if (Physics2D.BoxCast(transform.position + offset, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + offset - transform.up * castDistance, boxSize);
    }

    IEnumerator SlideRotation(float seconds, float zDegree)
    {
        isSliding = true;
        body.constraints &= ~RigidbodyConstraints2D.FreezeRotation;
        body.rotation = zDegree;
        boxSize.x = 0.72f;
        castDistance = -0.14f;        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        base.gameObject.transform.position += new Vector3(0, 0.4f, 0);
        body.rotation = 0f;
        boxSize.x = 0.18f;
        castDistance = 0.38f;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
        isSliding = false;
    }

    IEnumerator SetBackPenalty(float distance)
    {
        // Setting back and freezing player for a time as a penalty 
        setBackPlayer = true;
        spriteR.enabled = false;
        transform.position += new Vector3(distance, 0, 0);
        body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;        
        yield return new WaitForSeconds(frozenTime);
        body.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
        spriteR.enabled = true;
        yield return new WaitForSeconds(invincibilityDur-frozenTime);
        setBackPlayer = false;
    }

    private void OnDestroy()
    {
        _walkSpeed = walkSpeed;
        _runSpeed = runSpeed;
        _jumpForce = jumpForce;
        _bootV = bootV;
    }


}

