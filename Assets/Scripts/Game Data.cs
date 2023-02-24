using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class GameData
{

    public int totalCoins;
    public int totalDistance;

    public bool[] levelUnlocked;
    public bool[] charUnlocked;

    public int language;
    public int qualitySettings;

    public GameData()
    {
        totalCoins = 0;
        totalDistance = 0;
        levelUnlocked = new bool[5];
        levelUnlocked[0] = true;
        charUnlocked = new bool[5];
        charUnlocked[0] = true;
        language = 0; 
        qualitySettings = 1;
    }
}
