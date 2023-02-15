using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject player;
    private int playerCoins = 0;
    public Text uiDistance;
    public Text uiCoins;

    // Start is called before the first frame update
    //public RectTransform dis;
    //public RectTransform coin;
    public RectTransform rectTransform;
    public CoinDetector coinDetector;
    //public Vector2 vec;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // rectTransform = GetComponent<RectTransform>();
        // vec = new Vector2(rectTransform.anchoredPosition[0], rectTransform.anchoredPosition[1]);
        //dis.position = new Vector3(rectTransform.anchoredPosition.x * 1.8f, rectTransform.anchoredPosition.y * 1.8f, 0f) ;
        //dis.RectTransform.sizeDelta
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";
        uiCoins.text = playerCoins.ToString() + " Coins";
        float x = rectTransform.anchoredPosition.x;
        float y = rectTransform.anchoredPosition.y;
        uiDistance.transform.position = new Vector2(x * 1.55f, y * 1.8f);
        uiCoins.transform.position = new Vector2(x * 1.55f, y * 1.65f);

        
        //Debug.Log(x);

        //dis.position = new Vector2(x, y);
         //   rectTransform.anchoredPosition.y - 120);

        //uiDistance.transform.position = new Vector2(rectTransform.anchoredPosition.x - 180,
        //    rectTransform.anchoredPosition.y - 150);

        if (coinDetector.timer > 0)
        {
            uiCoins.text += "\n\nMagnet:" + ((int)coinDetector.timer).ToString() + " Sec";
        }

    }

    public void CoinCollected()
    {
        playerCoins++;
    }
}
