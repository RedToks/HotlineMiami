using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(damage);
            ReturnToPool();
        }
        else
        {
            ReturnToPool();
        }
    }
}
