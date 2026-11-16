using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform startPosition;
    // add inventory when its ready
    // public Inventory playerInventory;

    private SaveData saveData;
    private const string SaveKey = "GameSave";

    public void SaveGame()
    {
        if (player == null) { Debug.LogError("Player reference is missing."); return; }

        saveData = new SaveData();

        saveData.health = player.CurrentHealth;
        saveData.lives = player.Lives;
        saveData.coins = player.Coins;

        saveData.currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        Vector3 position = player.transform.position;
        saveData.posX = position.x;
        saveData.posY = position.y;
        saveData.posZ = position.z;

        Vector3 rotation = player.transform.eulerAngles;
        saveData.rotX = rotation.x;
        saveData.rotY = rotation.y;
        saveData.rotZ = rotation.z;

        // current inventory, add when ready
        // saveData.inventoryItems = playerInventory.GetItemList();

        string json = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString(SaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (player == null) { Debug.LogError("Player reference is missing."); return; }
        
        if (!PlayerPrefs.HasKey(SaveKey)) { Debug.Log("No save file found."); return; }

        string json = PlayerPrefs.GetString(SaveKey);
        saveData = JsonUtility.FromJson<SaveData>(json);

        if (saveData == null) { Debug.LogError("Save data could not load."); return; }

        player.CurrentHealth = saveData.health;
        player.Lives = saveData.lives;
        player.Coins = saveData.coins;

        Vector3 position = new Vector3(saveData.posX, saveData.posY, saveData.posZ);

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null) { controller.enabled = false; }

        player.transform.position = position;
        player.transform.eulerAngles = new Vector3(saveData.rotX, saveData.rotY, saveData.rotZ);

        // apply inventory, add when ready
        // playerInventory.LoadItemList(saveData.inventoryItems);

        if (controller != null) { controller.enabled = true; }
        Debug.Log("Game Loaded");
      
    }

    public void NewGame()
    {
        if (player == null) { Debug.LogError("Player reference is missing."); return; }
        
        player.CurrentHealth = 100;
        player.Lives = 0;
        player.Coins = 0;
        // add when ready
        // playerInventory.ClearInventory();

        if (startPosition != null) { CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null) { controller.enabled = false; }

            player.transform.SetPositionAndRotation(startPosition.position, startPosition.rotation);

            if (controller != null) { controller.enabled = true; }
        }
        Debug.Log("New game");
    }

}