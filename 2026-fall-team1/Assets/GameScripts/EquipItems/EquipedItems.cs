using UnityEngine;

public class EquipedItems : MonoBehaviour
{

    [SerializeField]
    GameObject[] Equipment;
    private int NumButtons = 4;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Equipment = new GameObject[NumButtons - 1];
    }

    // Update is called once per frame

}
