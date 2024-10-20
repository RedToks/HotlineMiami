using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;
    private Weapon currentWeapon;
    private WeaponDisplay weaponDisplay;

    public Weapon[] Weapons => weapons;

    private void Start()
    {
        weaponDisplay = FindObjectOfType<WeaponDisplay>();
        if (weapons.Length > 0)
        {
            EquipWeapon(weapons[0]);
        }
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        currentWeapon = newWeapon;
        weaponDisplay.UpdateWeaponSprite(newWeapon);
    }

    public void AddWeapon(Weapon newWeapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = newWeapon;
                break;
            }
        }
    }

    public void RemoveWeapon(Weapon weaponToRemove)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == weaponToRemove)
            {
                weapons[i] = null;
                break;
            }
        }
    }
}
