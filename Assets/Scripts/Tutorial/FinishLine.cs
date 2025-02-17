using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    public bool playerFinished;
    private GameObject pauseButton;
    // Start is called before the first frame update
    void Start()
    {
        playerFinished = false;
        pauseButton = GameObject.Find("PauseButton");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.transform.localScale.x < 0)
        {
            playerFinished = true;
            pauseButton.GetComponent<Button>().onClick.Invoke();
        }
    }
}
