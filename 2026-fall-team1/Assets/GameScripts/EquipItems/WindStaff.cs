using UnityEngine;

public class WindStaff : MonoBehaviour
{
    [Header("Settings")]
    public GameObject windGustPrefab;
    public float spawnDistanceBelow = 1.1f; // Adjust based on player height relative to item
    public float cooldown = 2f;
    public CharacterController characterController;

    private float nextUseTime = 0f;

    // Update is called once per frame
    private void OnWindUse()
    {
        Debug.Log("Wind staff was used!!!");
        //Input to use item
        if (Time.time > nextUseTime)
        {
            SpawnWindGust();
            nextUseTime = Time.time + cooldown;
        }
    }

    void SpawnWindGust()
    {
        if (windGustPrefab == null)
        {
            Debug.Log("Wind Gust Prefab was not selected!");
        }
        //Calculate position beneath the player
        Vector3 spawnPos = characterController.transform.position + (Vector3.down * spawnDistanceBelow);

        //Spawn the object
        Instantiate(windGustPrefab, spawnPos, Quaternion.identity);

    }
}
