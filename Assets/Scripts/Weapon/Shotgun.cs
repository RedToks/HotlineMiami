using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private int shotgunPellets = 5;
    [SerializeField] private float spreadAngle = 15f;

    public override void Attack()
    {
        if (CanShoot())
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 firePointPosition = firePoint.position;
            Vector2 baseDirection = (mousePosition - firePointPosition).normalized;

            for (int i = 0; i < shotgunPellets; i++)
            {
                float angle = Random.Range(-spreadAngle, spreadAngle);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                Vector2 direction = rotation * baseDirection;
                InstantiateBullet(direction);
                PlayShootSound();
            }

            UseAmmo();
            Debug.Log($"Игрок попал с дробовика и нанёс {damage} урона");
            nextFireTime = Time.time + fireRate;
        }
    }

}
