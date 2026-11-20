using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] weapons;
    public bool[] unlockedWeapons;
    public int currentWeapon = -1;

    public Sprite[] weaponImage;
    public ActiveInventoryHUD activeInventoryHUD;

    private void Start()
    {
        EquipWeapon(0);
        RefreshHUD();
    }

    public void EquipWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length) { return; }
        if (weapons[index] == null) { return; }

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null) weapons[i].SetActive(false);
        }

        weapons[index].SetActive(true);
        currentWeapon = index;
        RefreshHUD();
    }

    public GameObject GetCurrentWeapon()
    {
        if (currentWeapon < 0 || currentWeapon >= weapons.Length)
            return null;
        return weapons[currentWeapon];  
    }

    public void UnlockWeapon(int index)
    {
        if (index < 0 || index >= unlockedWeapons.Length) return;
        unlockedWeapons[index] = true;
    }

    public void RefreshHUD()
    {
        if (activeInventoryHUD != null) activeInventoryHUD.UpdateHUD();
    }
}
