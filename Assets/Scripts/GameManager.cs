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

    public GameObject gameOverMenu;


    // Start is called before the first frame update
    //public RectTransform dis;
    //public RectTransform coin;
    public RectTransform rectTransform;
    public CoinDetector coinDetector;
    //public Vector2 vec;

    public GameData gameData;
    public LightingManager lightingManager;
    public List<Material> materials;

    public GameObject speedUI;
    public GameObject bubbleUI;
    public GameObject MegnetUI;

    private void Awake()
    {
        gameData = SaveSystem.Load();
    }

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

        if (lightingManager.TimeOfDay >= 0 && lightingManager.TimeOfDay <= 6) 
        {
            RenderSettings.skybox = materials[0];            
        } else if (lightingManager.TimeOfDay >= 6 && lightingManager.TimeOfDay <= 12)
        {
            RenderSettings.skybox = materials[1];
        } else if (lightingManager.TimeOfDay >= 12 && lightingManager.TimeOfDay <= 18)
        {
            RenderSettings.skybox= materials[0];
        } else
        {
            RenderSettings.skybox = materials[2];
        }


        int distance = Mathf.RoundToInt(player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";
        uiCoins.text = playerCoins.ToString() + " x";
        //float x = rectTransform.anchoredPosition.x;
        //float y = rectTransform.anchoredPosition.y;
        //uiDistance.transform.position = new Vector2(x * 1.57f, y * 1.8f);
        //uiCoins.transform.position = new Vector2(x * 1.55f, y * 1.7f);

        
        //Debug.Log(x);

        //dis.position = new Vector2(x, y);
         //   rectTransform.anchoredPosition.y - 120);

        //uiDistance.transform.position = new Vector2(rectTransform.anchoredPosition.x - 180,
        //    rectTransform.anchoredPosition.y - 150);

        if (coinDetector.timer > 0)
        {
            //uiCoins.text += "\n\nMagnet: " + ((int)coinDetector.timer).ToString() + " Sec";
            MegnetUI.SetActive(true);
            MegnetUI.GetComponentInChildren<Text>().text = ((int)coinDetector.timer).ToString() + " s";
        }
        else{ MegnetUI.SetActive(false); }

        if (player.GetComponent<TestCharController>().speeding > 0)
        {
            speedUI.SetActive(true);
            speedUI.GetComponentInChildren<Text>().text = ((int)player.GetComponent<TestCharController>().speeding) + " s";
        } else { speedUI.SetActive(false); }

        if (player.GetComponent<TestCharController>().bubbled > 0)
        {
            bubbleUI.SetActive(true);
            bubbleUI.GetComponentInChildren<Text>().text = ((int)player.GetComponent<TestCharController>().bubbled) + " s";
        }
        else { bubbleUI.SetActive(false); }
    }

    public void CoinCollected()
    {
        playerCoins++;
    }

    public void GameOver()
    {
        gameData.totalCoins += playerCoins;
        gameData.totalDistance += Mathf.RoundToInt(player.transform.position.z);
        SaveSystem.Save(gameData);
        gameOverMenu.SetActive(true); 
    }
}
