using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour
{
    public GameObject player;
    public GameObject HUD;
    private HUD hudScript;
    PlayerMovement playerM;
    Health health;
    private Rigidbody2D playerBody;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public Sprite[] spritesBoots;
    private BoxCollider2D boxCollider;
    private int powerUpNo;
    private Dictionary<int, string> powerUps = new Dictionary<int, string>()
    {
        { 0, "flashBoots" }, { 1, "shield" }, { 2, "rocket" }
    };
    public List<Sprite> PowerUpSprites;
    [SerializeField] private float bootsCoolDowntime = 3;
    [SerializeField] private float boxCoolDowntime = 5;
    private bool Pickable = true;
    private List<int> itemIDs;
    private int itemID;
    private Vector2 regularSpeed;
    public GameObject hologram;
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        playerM = player.GetComponent<PlayerMovement>();
        health = player.GetComponent<Health>();
        playerBody = player.GetComponent<Rigidbody2D>();
        hudScript = HUD.GetComponent<HUD>();
    }

    private void Start()
    {        
        sprites = playerM.sprites;
        spritesBoots = playerM.spritesWBoots;
        StartCoroutine(GetItemId());
    }

    // Update is called once per frame
    void Update()
    {

        if (playerM.hasShield && health.currentHealth<2)
            {
                StartCoroutine(ShieldCrack());
            }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            regularSpeed = collision.attachedRigidbody.velocity;

            if (Pickable && !playerM.hasPowerUp && !health.isInvincible)
            {
                Pickable = false;
                Debug.Log("picked up power up");
                playerM.hasPowerUp = true;
                hologram.GetComponent<SpriteRenderer>().enabled = false;
                spriteRenderer.enabled = false;
                boxCollider.enabled = false;
                StartCoroutine(BoxCooldown());
                powerUpNo = Random.Range(0, 2);
                hudScript.pUDRenderer.sprite = PowerUpSprites[powerUpNo];

                switch (powerUpNo)
                {
                    case 0:
                        Debug.Log("speed boots ready");
                        playerM.hasPowerUp = true;
                        playerM.hasBoots = true;
                        playerM.spriteR.sprite = spritesBoots[itemID];                         
                        StartCoroutine(BootsCooldown());
                        break;
                        
                    case 1:
                        Debug.Log("shield ready");
                        playerM.hasPowerUp = true;
                        playerM.hasShield = true;
                        health.currentHealth += 1;
                        break;
                        
                    case 2:
                        Debug.Log("rocket ready");
                        playerM.hasPowerUp = true;
                        break;
                }
            }

        }
    }
    IEnumerator GetItemId()
    {
        yield return new WaitForSeconds(1);
        itemIDs = WearItem.itemID;
        if (itemIDs != null)
        {
            itemID = itemIDs[0] + 1;
        }
        else
        {
            itemID = 0;
        }
    }

    IEnumerator BootsCooldown()
    {
        yield return new WaitForSeconds(bootsCoolDowntime);
        playerM.spriteR.sprite = sprites[itemID];
        playerM.hasPowerUp = false;
        playerM.hasBoots = false;
        Debug.Log("speed boots over");
    }

    IEnumerator BoxCooldown()
    {
        yield return new WaitForSeconds(boxCoolDowntime);
        hologram.GetComponent<SpriteRenderer>().enabled = true;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        Pickable = true;        
    }

    IEnumerator ShieldCrack()
    {
        Debug.Log("shield over");
        playerM.shield.GetComponent<Animator>().SetBool("isCracked", true);
        yield return new WaitForSeconds(playerM.shield.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        playerM.shield.GetComponent<Animator>().SetBool("isCracked", false);
        playerM.hasShield = false;
        playerM.hasPowerUp = false;
    }
}
