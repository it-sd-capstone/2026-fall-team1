using UnityEngine;
using UnityEngine.UI;

public class ActiveInventoryHUD : MonoBehaviour
{
    public WeaponManager weaponManager;

    public Image[] weaponImages;

    private void Start() { UpdateHUD(); }

    public void UpdateHUD()
    {
        if (weaponManager != null) return;

        for (int i = 0; i < weaponImages.Length; i++)
        {
            if (weaponImages[i] == null) continue;

            bool available =
                i < weaponManager.unlockedWeapons.Length &&
                i < weaponManager.weaponImage.Length &&
                weaponManager.unlockedWeapons[i] &&
                weaponManager.weaponImage[i] != null;

            weaponImages[i].enabled = available;

            if (available) weaponImages[i].sprite = weaponManager.weaponImage[i];
        }
    }

}
