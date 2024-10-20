using UnityEngine;

public class Pistol : Weapon
{
    public override void Attack()
    {
        if (CanShoot())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firePointPosition = firePoint.position;
            Vector2 direction = (mousePosition - firePointPosition).normalized;

            InstantiateBullet(direction);
            UseAmmo();
            PlayShootSound();
            nextFireTime = Time.time + fireRate;

            Debug.Log($"Игрок попал с пистолета и нанёс {damage} урона");
        }
    }
}
