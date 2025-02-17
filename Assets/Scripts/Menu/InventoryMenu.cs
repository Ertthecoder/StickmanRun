using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class InventoryMenu : MonoBehaviour
{
    ShopMenu shopMenu;
    public TMP_Text CoinText;
    public List<int> inventory { get; private set; }
    public static List<int> itemIDBeingUsed;
    public GameObject canvas;
    public List<GameObject> shopMannequinsList;
    public List<GameObject> shopMannequinButtonList;
    public List<GameObject> activeInventoryMannequinsList;
    public List<GameObject> inventoryMannequinsList;
    public List<GameObject> inventoryMannequinButtonList;
    private GameObject inventoryMannequin;
    private GameObject inventoryMannequinButton;
    private TMP_Text inventoryMannequinButtonText;
    public List<TMP_Text> inventoryButtonTextList;
    private List<TMP_Text> shopButtonTextList;
    private int mannequinID;
    private int itemID;
    private int notebookIndex;
    private int inventoryIndex;

    public void Start()
    {
        shopMenu = canvas.GetComponent<ShopMenu>();               
        notebookIndex = shopMenu.NotebookIndex;
        shopButtonTextList = shopMenu.buttonTextList;
    }
    public void ShowInventory()
    {
        inventory = shopMenu.inventory;
        itemIDBeingUsed = shopMenu.itemIDBeingUsed;

        CoinText.text = shopMenu.CoinText.text;
        
        for (int i = 0; i < inventoryButtonTextList.Count; i++)
        {
            inventoryButtonTextList[i].SetText("Use");
        }

        if (inventory?.Count > 0)
            
        {
            Debug.Log("inventory isnt EMPTY");
            Debug.Log("item id used is: " + itemIDBeingUsed);    
            
            for (int i = 0; i < inventory.Count; i++)
            {
                inventoryIndex = (inventory[i] - 1) % 12;
                Debug.Log("i = "+i);
                inventoryMannequin = inventoryMannequinsList[inventoryIndex];
                inventoryMannequin.GetComponent<RectTransform>().position= 
                    shopMannequinsList[i].GetComponent<RectTransform>().position;
                inventoryMannequin.GetComponent<RectTransform>().pivot =
                    shopMannequinsList[i].GetComponent<RectTransform>().pivot;
                inventoryMannequin.GetComponent<RectTransform>().anchoredPosition=
                    shopMannequinsList[i].GetComponent<RectTransform>().anchoredPosition;
                inventoryMannequin.GetComponent<RectTransform>().rotation =
                    shopMannequinsList[i].GetComponent<RectTransform>().rotation;

                inventoryMannequinButton = inventoryMannequinButtonList[inventoryIndex];
                inventoryMannequinButton.GetComponent<RectTransform>().position =
                    shopMannequinButtonList[i].GetComponent<RectTransform>().position;
                inventoryMannequinButton.GetComponent<RectTransform>().pivot =
                    shopMannequinButtonList[i].GetComponent<RectTransform>().pivot;
                inventoryMannequinButton.GetComponent<RectTransform>().anchoredPosition =
                    shopMannequinButtonList[i].GetComponent<RectTransform>().anchoredPosition;
                inventoryMannequinButton.GetComponent<RectTransform>().right =
                    shopMannequinButtonList[i].GetComponent<RectTransform>().right;
                inventoryMannequinButton.GetComponent<RectTransform>().rotation =
                    shopMannequinButtonList[i].GetComponent<RectTransform>().rotation;

                
                if (i > 5)
                {
                    Debug.Log("mannequin is: " + inventoryMannequin);
                    Debug.Log("x position BEFORE adjustment:" + inventoryMannequin.transform.position);
                    inventoryMannequin.transform.position += new Vector3(-10, 0, 0);
                    Debug.Log("x position AFTER adjustment:" +inventoryMannequin.transform.position);
                    inventoryMannequinButton.transform.position += new Vector3(-11, 1, 0);
                }
                
                    if (activeInventoryMannequinsList.Contains(inventoryMannequin) == false)
                {
                    Debug.Log(inventoryMannequin);
                    activeInventoryMannequinsList.Add(inventoryMannequin);
                    inventoryMannequin.SetActive(true);                   
                    inventoryMannequinButton.SetActive(true);
                }
            }
        }
        
        if (itemIDBeingUsed != null)
        {
            foreach (TMP_Text text in inventoryButtonTextList)
            {
                if (text.text == "Using")
                {
                    text.SetText("Use");
                }
            }
            inventoryButtonTextList[itemIDBeingUsed[0]].SetText("Using");

            Debug.Log(inventoryButtonTextList[itemIDBeingUsed[0]] + " text set to: "
                + inventoryButtonTextList[itemIDBeingUsed[0]].text);
        }
    }
    
    public void Use(TMP_Text inventoryButtonText)
    {
        mannequinID = int.Parse(inventoryButtonText.name.Substring(inventoryButtonText.name.Length - 2));
        itemID = mannequinID + (notebookIndex * 12);
        Debug.Log("item id: " + itemID);
        
        foreach (TMP_Text text in inventoryButtonTextList)
        {
            if (text == inventoryButtonText)
            {
                continue;
            }
            if (text.text == "Using")
            {
                text.SetText("Use");
            }
        }
        if (inventoryButtonText.text == "Use")
        {
            inventoryButtonText.SetText("Using");
            itemIDBeingUsed.Clear();
            itemIDBeingUsed.Add((itemID - 1) % 12);
        }
        else
        {
            inventoryButtonText.SetText("Use");
            itemIDBeingUsed.Clear();
        }
        
    }

    private void OnDestroy()
    {
        itemIDBeingUsed = shopMenu.itemIDBeingUsed;
    }

}
