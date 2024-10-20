using UnityEngine;
using System;

public class PlayerAttack : MonoBehaviour
{
    public event Action OnAttack;

    private PlayerInventory playerInventory;
    private Weapon[] weapons;
    private WeaponDisplay weaponDisplay;
    private Weapon currentWeapon;
    private InventoryUI inventoryUI;

    private void Start()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        weapons = playerInventory.Weapons;
        weaponDisplay = FindObjectOfType<WeaponDisplay>();
        inventoryUI = FindObjectOfType<InventoryUI>();

        if (weapons.Length > 0)
        {
            SwitchWeapon(0);
        }
        else
        {
            Debug.LogError("У игрока нет оружия в инвентаре!");
        }
    }

    private void Update()
    {
        HandleAttack();
        HandleReload();
        HandleWeaponSwitch();

        if (currentWeapon.CurrentAmmo <= 0)
        {
            currentWeapon.Reload();
        }

        currentWeapon.UpdateReload();
        weaponDisplay.UpdateAmmoUI(currentWeapon);
    }

    private void HandleAttack()
    {
        if (currentWeapon == null) return;

        if (currentWeapon.CanShoot() && Input.GetKey(KeyCode.Mouse0))
        {
            currentWeapon.Attack();
            OnAttack?.Invoke();
            weaponDisplay.UpdateAmmoUI(currentWeapon);
        }
    }

    private void HandleReload()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            currentWeapon.Reload();
            weaponDisplay.UpdateAmmoUI(currentWeapon);
        }
    }

    private void HandleWeaponSwitch()
    {
        if (currentWeapon.IsReloading) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
    }

    private void SwitchWeapon(int index)
    {
        if (index >= 0 && index < weapons.Length)
        {
            currentWeapon = weapons[index];
            inventoryUI.UpdateInventory(weapons, currentWeapon);
            weaponDisplay.UpdateWeaponSprite(currentWeapon);
            weaponDisplay.UpdateAmmoUI(currentWeapon);
            Debug.Log($"Выбрано оружие: {currentWeapon.name}");
        }
        else
        {
            Debug.LogWarning($"Оружие с индексом {index} не существует.");
        }
    }
}
