using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    // player stats
    public float health;
    public int lives;
    public int coins;
    public int score;

    // inventory
    // public List<string> inventoryItems;

    public string currentScene;

    // player position
    public float posX, posY, posZ;

    public float rotX, rotY, rotZ;


    // level progress
    public int currentLevel;
    public bool[] completedLevels;
    
}
