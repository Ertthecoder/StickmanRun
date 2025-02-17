using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WearItem : MonoBehaviour
{
    PlayerMovement playerM;
    public GameObject player;
    public static List<int> itemID;
    private SpriteRenderer spriteRenderer;
    private Sprite[] itemSprites;
    // Start is called before the first frame update

    private void Awake()
    {
        playerM = player.GetComponent<PlayerMovement>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        itemSprites = playerM.sprites;
        itemID = InventoryMenu.itemIDBeingUsed; 
        if (itemID != null)
        {
            spriteRenderer.sprite = itemSprites[itemID[0]+1];
        }  
    }
}
