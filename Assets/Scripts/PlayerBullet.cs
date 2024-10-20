using UnityEngine;

public class PlayerBullet : Bullet
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(damage);
            ReturnToPool();
        }
        if (collision.gameObject.TryGetComponent(out MovingTarget movingTarget))
        {
            movingTarget.TakeDamage(damage);
            ReturnToPool();
        }
        else
        {
            ReturnToPool();
        }
    }
}
