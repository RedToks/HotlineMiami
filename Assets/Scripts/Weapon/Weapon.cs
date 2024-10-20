using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected Transform firePoint;
    [SerializeField] protected float fireRate;
    [SerializeField] protected Sprite weaponSprite;
    [SerializeField] protected int damage;
    [SerializeField] protected float bulletSpeed;

    [SerializeField] protected int maxAmmo;
    protected int currentAmmo;
    protected float nextFireTime;

    private AudioSource audioSource;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float shootVolume = 1f;
    [SerializeField] protected float reloadTime;
    [SerializeField] private float reloadVolume = 1f;
    protected float reloadProgress;
    private bool isReloading = false;

    public Sprite WeaponSprite => weaponSprite;
    public float Damage => damage;
    public int MaxAmmo => maxAmmo;
    public int CurrentAmmo => currentAmmo;
    public float FireRate => fireRate;
    public float NextFireTime => nextFireTime;
    public bool IsReloading => isReloading;

    protected void Start()
    {
        audioSource = GetComponent<AudioSource>();
        firePoint.Rotate(0, 0, -90);
        currentAmmo = maxAmmo;
    }

    public abstract void Attack();

    protected void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound, shootVolume);
        }
    }

    protected void InstantiateBullet(Vector2 direction)
    {
        Bullet bullet = FindObjectOfType<BulletPool>().GetPlayerBullet();
        bullet.transform.position = firePoint.position;
        bullet.SetDirection(direction);
        bullet.SetSpeed(bulletSpeed);

        if (bullet is PlayerBullet playerBullet)
        {
            playerBullet.SetDamage(damage);
        }
    }

    public virtual bool CanShoot()
    {
        return Time.time >= nextFireTime && currentAmmo > 0 && !isReloading;
    }

    public virtual void Reload()
    {
        if (currentAmmo < maxAmmo && !isReloading)
        {
            currentAmmo = 0;
            isReloading = true;
            reloadProgress = 0f;
            PlayReloadSound();
        }
    }

    protected void PlayReloadSound()
    {
        if (reloadSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(reloadSound, reloadVolume);
        }
    }

    protected void UseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;
        }
    }

    public void UpdateReload()
    {
        if (isReloading)
        {
            reloadProgress += Time.deltaTime / reloadTime;
            if (reloadProgress >= 1f)
            {
                currentAmmo = maxAmmo;
                isReloading = false;
                reloadProgress = 1f;
            }
        }
    }

    public float ReloadProgress => reloadProgress;

    public float GetFireCooldownProgress()
    {
        return Mathf.Clamp01((Time.time - nextFireTime + fireRate) / fireRate);
    }
}
