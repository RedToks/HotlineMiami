using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private SpriteRenderer weaponSpriteRenderer;
    [SerializeField] private Slider reloadSlider;
    [SerializeField] private Slider fireRateSlider;
    [SerializeField] private TextMeshProUGUI ammoText; 

    public void UpdateWeaponSprite(Weapon weapon)
    {
        if (weapon != null)
        {
            weaponSpriteRenderer.sprite = weapon.WeaponSprite;
            UpdateAmmoUI(weapon);
        }
    }

    public void UpdateAmmoUI(Weapon weapon)
    {
        reloadSlider.value = weapon.ReloadProgress;
        ammoText.text = $"{weapon.CurrentAmmo}/{weapon.MaxAmmo}";

        if (weapon.CanShoot())
        {
            fireRateSlider.value = 1f;
        }
        else
        {
            float timeUntilNextShot = weapon.NextFireTime - Time.time;
            fireRateSlider.value = Mathf.Clamp01(1f - (timeUntilNextShot / weapon.FireRate));
        }
    }
}
