using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class ShopMenu : MonoBehaviour
{
    InventoryMenu inventoryMenu;
    public static double coins = 4000;
    public int NotebookIndex = 0;
    IDictionary<int, int> itemsList = new Dictionary<int, int>() 
    {
        { 01, 150 }, { 02, 250 }, { 03, 250 }, { 04, 250 }, { 05, 250 }, { 06, 250 }, 
        { 07, 500 }, { 08, 250 }, { 09, 500 }, { 10, 250 }, { 11, 250 }, { 12, 250 }
    };
    public List<int> inventory;
    private static List<int> _inventory;
    public TMP_Text CoinText;
    public TMP_Text PriceText;
    private GameObject ItemName;
    public List<GameObject> objectsToReactivate;
    public List<TMP_Text> buttonTextList;
    public List<int> itemIDBeingUsed;
    private static List<int> _itemIDBeingUsed;
    public GameObject uiImageObject;
    public Sprite[] itemSprites;

    //buton textleri sayfa çevirdikçe deðiþecek þekilde itemlist fiyatlarýna baðla

    public TMP_Text buyButtonText;
    private int mannequinID;
    public int itemID;
    private int itemCount = 12;
    private string itemName;

    private void Awake()
    {

    }
    void Start()
    {
        if (_inventory != null)
        {
            inventory = _inventory;
        }
        if (_itemIDBeingUsed != null)
        {
            itemIDBeingUsed = _itemIDBeingUsed;
        }
        Debug.Log("Shop Menu is ACTIVE");        
        CoinText?.SetText(coins.ToString());

        for (int i = 0; i < buttonTextList.Count; i++)
        {
            buttonTextList[i].SetText(itemsList[i + 1 + (NotebookIndex * 12)].ToString());
        }
    }

    public void ShopMenuButton()
    {
        

        if (inventory != null)
        {
            foreach (int item in inventory)
            {               
                buttonTextList[item - 1].SetText("Use");               
            }

            if (itemIDBeingUsed != null)
            {
                buttonTextList[itemIDBeingUsed[0]].SetText("Using");           
            }
        }
    }
    public void ItemShow(GameObject MannequinName)
    {


        mannequinID = int.Parse(MannequinName.name.Substring(MannequinName.name.Length - 2));
        itemID = mannequinID + (NotebookIndex*12);
        itemName = "item" + itemID;
        Debug.Log("item id: "+itemID);
        
        if (inventory?.Contains(itemID) == true && buttonTextList[(itemID - 1) % 12 ].text == "Use")
        {
            buyButtonText.SetText("Use");
        }
        else if (buttonTextList[(itemID - 1) % 12].text == "Using")
        {
            buyButtonText.SetText("Using");
        }

        if (itemName.Length < 6) //itemXX karakter sayýsý
        {
            itemName = "item0" + itemID;
        }
        Debug.Log("item name: " + itemName);
        
        uiImageObject.GetComponent<Image>().sprite = itemSprites[itemID-1];
        
        PriceText?.SetText(itemsList[itemID].ToString());
        
    }
    
    public void ReactivateAllItems()
    {
            buyButtonText.SetText("Buy");
            buyButtonText.fontSize = 15;
            buyButtonText.fontStyle = FontStyles.Normal;
            Debug.Log("item id being used in shop is: " + itemIDBeingUsed[0]);
    }
    public void Buy()
    {
        Debug.Log(buttonTextList[(itemID - 1) % 12]);
        
        if (buyButtonText.text == "Use")
        {
            
            buyButtonText.SetText("Using");
            buyButtonText.fontSize = 12;
            buyButtonText.fontStyle = FontStyles.Italic;
            foreach (TMP_Text text in buttonTextList)
                {
                    if (text.text == "Using")
                    {
                        text.SetText("Use");
                    }
                }
            buttonTextList[(itemID - 1) % 12].SetText("Using");
            itemIDBeingUsed.Clear();
            itemIDBeingUsed.Add((itemID - 1) % 12);
        }
        else if (buyButtonText.text == "Using")
        {
            buyButtonText.fontStyle = FontStyles.Normal;
            buttonTextList[(itemID - 1) % 12].SetText("Use");
            buyButtonText.SetText("Use");
            itemIDBeingUsed.Clear();
        }

        else if (coins >= itemsList[itemID])
        {
            coins -= itemsList[itemID];
            CoinText?.SetText(coins.ToString());
            inventory?.Add(itemID);
            buttonTextList[(itemID - 1) % 12].SetText("Use");
            buyButtonText.SetText("Use");
            Debug.Log("Item's bought");
        }
        else
        {
            Debug.LogWarning("More coins needed for this item.");
        }

    }
   
    void Update()

    {
        //itemIDBeingUsed = inventoryMenu.itemIDBeingUsed;

    }

    private void OnDestroy()
    {
        if (inventory != null)
        {
            _inventory = inventory;
        }

        if (itemIDBeingUsed != null)
        {
            _itemIDBeingUsed = itemIDBeingUsed;
        }
    }

}
