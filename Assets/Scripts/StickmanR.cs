
using UnityEngine;
using UnityEngine.UI;


public class StickmanR : MonoBehaviour
{
    RectTransform rectTransform;

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float fillAmount = 0.5f;
    [SerializeField] private float slowingRate;
    public Image speedEffect;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

    }
    // Update is called once per frame
    void Update()
    {
        speed -= slowingRate;
        if (rectTransform.anchoredPosition.x < -320)
        {
            rectTransform.anchoredPosition += new Vector2(speed, 0);
        }

        if (rectTransform.anchoredPosition.x >= -575)
        {
            speedEffect.fillAmount += fillAmount;
        }
    }
}
