using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float _time;
    TextMeshProUGUI timer_TMP;
    GameObject finishLine;
    FinishLine finishLineScript;
    GameObject countdown;
    // Start is called before the first frame update
    void Start()
    {
        timer_TMP = gameObject.GetComponent<TextMeshProUGUI>();
        finishLine = GameObject.Find("FinishLine");
        finishLineScript = finishLine.GetComponent<FinishLine>();
        countdown = GameObject.Find("StartCountdownTMP");
    }

    // Update is called once per frame
    void Update()
    {
        if (!finishLineScript.playerFinished && !countdown.activeInHierarchy)
        {
           _time += Time.deltaTime; 
            DisplayTime(_time);
        }
        
    }

    void DisplayTime(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(_time / 60);
        float seconds = Mathf.FloorToInt(_time % 60);

        timer_TMP.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
