using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D.Animation;

public class StartingCountdown : MonoBehaviour
{
    TextMeshProUGUI m_TextMeshPro;
    public GameObject player;
    // Start is called before the first frame update
    void Awake()
    {
        m_TextMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
    }

    public void StartCountdown()
    {
        Time.timeScale = 1.0f;
        player.GetComponent<PlayerMovement>().enabled = false;
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        m_TextMeshPro.text = "3";
        yield return new WaitForSeconds(1);
        m_TextMeshPro.text = "2";
        yield return new WaitForSeconds(1);
        m_TextMeshPro.text = "1";
        yield return new WaitForSeconds(1);
        m_TextMeshPro.text = "Run!";
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerMovement>().enabled = true;
        gameObject.SetActive(false);
    }


}
