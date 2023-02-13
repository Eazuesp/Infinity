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
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.RoundToInt(player.transform.position.z);
        uiDistance.text = distance.ToString() + " m";
        uiCoins.text = playerCoins.ToString() + " Coins";
    }

    public void CoinCollected()
    {
        playerCoins++;
    }
}
