using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCooldown;

    private float nextAttackTime;

    protected override void Update()
    {
        base.Update();
        if (currentState == EnemyState.Chasing)
        {
            Move();
            Attack();
        }
    }

    protected override void Move()
    {
        if (CanSeePlayer())
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.up = directionToPlayer;

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > attackRange)
            {
                agent.SetDestination(player.position);
                animator.SetBool("isMoving", true);
            }
            else
            {
                agent.ResetPath();
                animator.SetBool("isMoving", false);
                Attack();
            }
        }
        else
        {
            currentState = EnemyState.Patrolling;
            agent.ResetPath();
            animator.SetBool("isMoving", false);
        }
    }

    public override void Attack()
    {
        if (Time.time >= nextAttackTime && Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            nextAttackTime = Time.time + attackCooldown;
            animator.SetTrigger("meleeAttack");
            Debug.Log("Ближний враг атакует игрока!");
        }
    }

    public void DealDamage()
    {
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
                Debug.Log($"Игрок получил {attackDamage} урона от ближнего врага.");
            }
        }
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
