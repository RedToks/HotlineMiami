using UnityEngine;

public class SniperRifle : Weapon
{
    public override void Attack()
    {
        if (CanShoot())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firePointPosition = firePoint.position;
            Vector2 direction = (mousePosition - firePointPosition).normalized;

            InstantiateBullet(direction);
            PlayShootSound();

            UseAmmo();
            nextFireTime = Time.time + fireRate;

            Debug.Log($"Игрок выстрелил из снайперской винтовки и нанёс {damage} урона");
        }
    }
}
