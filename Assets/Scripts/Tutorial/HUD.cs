using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HUD : MonoBehaviour
{
    public GameObject player;
    public GameObject healthFilling;
    private PlayerMovement pMoveScript;
    public GameObject respawnC;
    public GameObject respawnTint;
    public GameObject powerUpSymbol;
    public SpriteRenderer pUDRenderer;
    //private float healthFillAmount;
    // Start is called before the first frame update
    void Awake()
    {
        //healthFillAmount = healthFilling.GetComponent<Image>().fillAmount;
        pMoveScript = player.GetComponent<PlayerMovement>();
        pUDRenderer = powerUpSymbol.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (pMoveScript.spriteR.enabled && respawnC.activeInHierarchy)
        {
            respawnC.SetActive(false);
            respawnTint.SetActive(false);
            respawnC.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
        }
        if (!pMoveScript.hasPowerUp)
        {
            pUDRenderer.sprite = null;
        }
    }

    private void FixedUpdate()
    {
        if (!pMoveScript.spriteR.enabled && pMoveScript.setBackPlayer)
        {
            respawnC.SetActive(true);
            respawnTint.SetActive(true);
            respawnC.GetComponent<UnityEngine.UI.Image>().fillAmount -= Time.deltaTime / pMoveScript.frozenTime;
        }
    }

    public void QuitTutorial()
    {
        Debug.Log("QUIT!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
