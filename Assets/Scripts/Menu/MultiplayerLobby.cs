using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerLobby : MonoBehaviour
{
    ShopMenu shopMenu;
    public TMP_Text CoinText;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        shopMenu = canvas.GetComponent<ShopMenu>();
    }
    public void Run()
    {
       /* SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);*/
    }
    // Update is called once per frame
    void Update()
    {
        CoinText.text = shopMenu.CoinText.text;
    }
}
