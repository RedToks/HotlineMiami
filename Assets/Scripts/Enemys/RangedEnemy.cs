using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackRange;
    [SerializeField] private int bulletDamage = 10;
    [SerializeField] private AudioClip shootSound;

    private float nextAttackTime;
    private BulletPool bulletPool;

    protected override void Start()
    {
        base.Start();
        bulletPool = FindObjectOfType<BulletPool>();
    }

    protected override void Move()
    {
        if (player != null)
        {
            if (CanSeePlayer())
            {
                Vector2 directionToPlayer = (player.position - transform.position).normalized;
                transform.up = directionToPlayer;

                if (Vector2.Distance(transform.position, player.position) > attackRange)
                {
                    agent.SetDestination(player.position);
                    animator.SetBool("isMoving", true);
                }
                else
                {
                    agent.ResetPath();
                    animator.SetBool("isMoving", false);
                }
            }
            else
            {
                currentState = EnemyState.Patrolling;
                agent.ResetPath();
                animator.SetBool("isMoving", false);
            }
        }
    }

    public override void Attack()
    {
        if (CanSeePlayer() && Vector2.Distance(transform.position, player.position) <= attackRange && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("rangedAttack");

        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        EnemyBullet bullet = bulletPool.GetEnemyBullet();
        bullet.transform.position = transform.position;
        bullet.SetDirection((player.position - transform.position).normalized);
        bullet.SetDamage(bulletDamage);
        Debug.Log("Враг стреляет в игрока");
    }

    protected override void Patrol()
    {
      
    }

    protected override void SearchForPlayer()
    {
        if (Vector2.Distance(transform.position, lastSeenPosition) > 0.1f)
        {
            agent.SetDestination(lastSeenPosition);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            currentState = EnemyState.Patrolling;

            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.up = directionToPlayer;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
