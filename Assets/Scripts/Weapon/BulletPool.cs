using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private PlayerBullet playerBulletPrefab;
    [SerializeField] private EnemyBullet enemyBulletPrefab;
    [SerializeField] private int poolSize = 25;

    private List<PlayerBullet> playerBulletPool;
    private List<EnemyBullet> enemyBulletPool;

    private void Awake()
    {
        playerBulletPool = new List<PlayerBullet>();
        enemyBulletPool = new List<EnemyBullet>();

        for (int i = 0; i < poolSize; i++)
        {
            PlayerBullet bullet = Instantiate(playerBulletPrefab);
            bullet.gameObject.SetActive(false);
            playerBulletPool.Add(bullet);
        }

        for (int i = 0; i < poolSize; i++)
        {
            EnemyBullet bullet = Instantiate(enemyBulletPrefab);
            bullet.gameObject.SetActive(false);
            enemyBulletPool.Add(bullet);
        }
    }

    public PlayerBullet GetPlayerBullet()
    {
        foreach (var bullet in playerBulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        PlayerBullet newBullet = Instantiate(playerBulletPrefab);
        newBullet.gameObject.SetActive(true);
        playerBulletPool.Add(newBullet);
        return newBullet;
    }

    public EnemyBullet GetEnemyBullet()
    {
        foreach (var bullet in enemyBulletPool)
        {
            if (!bullet.gameObject.activeInHierarchy)
            {
                bullet.gameObject.SetActive(true);
                return bullet;
            }
        }

        EnemyBullet newBullet = Instantiate(enemyBulletPrefab);
        newBullet.gameObject.SetActive(true);
        enemyBulletPool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }
}
