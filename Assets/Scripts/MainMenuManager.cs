using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private GameData gameData;

    public Text uiCoins;
    public Text uiDistance;

    public List<Material> materials;
    public LightingManager lightingManager;

    private GameObject player;

    private void Awake()
    {
        gameData = SaveSystem.Load();
        RefreshUI();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        if (lightingManager.TimeOfDay >= 0 && lightingManager.TimeOfDay <= 6)
        {
            RenderSettings.skybox = materials[0];

            Sleep();
        }
        else if (lightingManager.TimeOfDay >= 6 && lightingManager.TimeOfDay <= 12)
        {
            RenderSettings.skybox = materials[1];

            Sit();

        }
        else if (lightingManager.TimeOfDay >= 12 && lightingManager.TimeOfDay <= 18)
        {
            RenderSettings.skybox = materials[0];

            Sit();

        }
        else
        {
            RenderSettings.skybox = materials[2];

            Sleep();

        }
        //Debug.Log(Application.persistentDataPath);
    }

    void RefreshUI()
    {
        uiCoins.text = gameData.totalCoins.ToString() + " x";
        uiDistance.text = gameData.totalDistance.ToString() + " m";
    }

    void Sleep()
    {
        player.GetComponent<Animator>().ResetTrigger("Sit");
        player.GetComponent<Animator>().SetTrigger("Sleep");
    }
    void Sit()
    {
        player.GetComponent<Animator>().ResetTrigger("Sleep");
        player.GetComponent<Animator>().SetTrigger("Sit");
    }

}
