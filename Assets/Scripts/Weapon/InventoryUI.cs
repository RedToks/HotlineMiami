using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Image[] weaponSlots;
    [SerializeField] private Sprite emptySlotSprite;


    public void UpdateInventory(Weapon[] weapons, Weapon currentWeapon = null)
    {
        for (int i = 0; i < weaponSlots.Length; i++)
        {
            if (i < weapons.Length)
            {
                if (weapons[i] != null)
                {
                    weaponSlots[i].sprite = weapons[i].WeaponSprite;
                    SetWeaponSlotColor(weaponSlots[i], weapons[i] == currentWeapon);
                }
                else
                {
                    weaponSlots[i].sprite = emptySlotSprite;
                    SetWeaponSlotColor(weaponSlots[i], false);
                }
            }
            else
            {
                weaponSlots[i].sprite = emptySlotSprite;
                SetWeaponSlotColor(weaponSlots[i], false); 
            }
        }
    }

    private void SetWeaponSlotColor(Image weaponSlot, bool isSelected)
    {
        if (isSelected)
        {
            weaponSlot.color = Color.white; 
        }
        else
        {
            weaponSlot.color = new Color(0, 0, 0, 0.3f); 
        }
    }
}
