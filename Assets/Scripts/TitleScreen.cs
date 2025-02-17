using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour
{
    private RectTransform rectTransform;
    public GameObject stickManR;
    public GameObject clickToStartButton;
    public void EnterMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        rectTransform = stickManR.GetComponent<RectTransform>();
        
        if (rectTransform.anchoredPosition.x >= -320)
        {
            Debug.Log("girdi");
            clickToStartButton.SetActive(true);
        }
    }
}

