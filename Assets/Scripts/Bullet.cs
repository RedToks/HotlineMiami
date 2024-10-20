using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected float lifetime = 5f;

    private BulletPool bulletPool;
    protected int damage;

    protected virtual void OnEnable()
    {
        bulletPool = FindObjectOfType<BulletPool>();        
        Invoke(nameof(ReturnToPool), lifetime);
    }

    protected virtual void OnDisable()
    {
        CancelInvoke(nameof(ReturnToPool));
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    protected void ReturnToPool()
    {
        bulletPool.ReturnBullet(this);
    }

    public void SetDirection(Vector2 direction)
    {
        transform.up = direction;
    }

    public void SetDamage(int damageValue)
    {
        damage = damageValue;
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
