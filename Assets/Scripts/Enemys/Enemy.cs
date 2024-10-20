using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    protected enum EnemyState { Patrolling, Chasing, Searching }
    protected EnemyState currentState;

    protected Vector2 lastSeenPosition;

    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected Transform player;
    [SerializeField] protected float detectionRadius = 5f;
    [SerializeField] private LayerMask obstaclesLayer;
    [SerializeField] private GameObject damageTextPrefab;
    [SerializeField] protected float viewAngle = 120f;

    [Header("Audio Clips")]
    [SerializeField] protected AudioClip[] hurtSounds;
    [SerializeField] protected AudioClip deathSound;

    protected AudioSource audioSource;
    protected Animator animator;
    protected NavMeshAgent agent;
    private Rigidbody2D rb;
    private Collider2D enemyCollider;

    public bool IsAlive { get; protected set; } = true;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = 8f;
        agent.angularSpeed = 120f;
        agent.stoppingDistance = 0;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.drag = 1f;
        rb.angularDrag = 1f;
        rb.interpolation = RigidbodyInterpolation2D.None;

        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        currentState = EnemyState.Patrolling;
    }

    protected virtual void Update()
    {
        switch (currentState)
        {
            case EnemyState.Patrolling:
                Patrol();
                if (CanSeePlayer())
                {
                    lastSeenPosition = player.position;
                    currentState = EnemyState.Chasing;
                }
                break;

            case EnemyState.Chasing:
                Move();
                Attack();
                if (!CanSeePlayer())
                {
                    currentState = EnemyState.Searching;
                }
                break;

            case EnemyState.Searching:
                SearchForPlayer();
                break;
        }
    }

    protected virtual void Patrol() { }

    protected virtual void Move()
    {
        if (player != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            transform.up = directionToPlayer;

            agent.SetDestination(player.position);
            animator.SetBool("isMoving", true);
        }
    }

    protected virtual bool CanSeePlayer()
    {
        if (player == null) return false;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRadius) return false;

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float angleToPlayer = Vector2.Angle(transform.up, directionToPlayer);
        if (angleToPlayer > viewAngle / 2f) return false;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstaclesLayer);

        return hit.collider == null || hit.collider.transform == player;
    }

    protected virtual void TurnTowardsPlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        transform.up = directionToPlayer;
    }

    protected virtual void SearchForPlayer()
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
        }
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (hurtSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hurtSounds.Length);
            audioSource.PlayOneShot(hurtSounds[randomIndex], 3f);
        }

        ShowDamageText(damage);
        TurnTowardsPlayer();

        if (health <= 0)
        {
            Die();
        }
    }
    public abstract void Attack();
    protected virtual void Die()
    {
        IsAlive = false;
        animator.SetBool("isDie", true);
        audioSource.PlayOneShot(deathSound);
        agent.isStopped = true;
        agent.enabled = false;
        enemyCollider.enabled = false;
        enabled = false;
        rb.simulated = false;
    }

    private void ShowDamageText(float damage)
    {
        if (damageTextPrefab != null)
        {
            float randomX = Random.Range(-0.5f, 0.5f);
            float randomY = Random.Range(0.4f, 0.6f);
            Vector3 randomPosition = transform.position + new Vector3(randomX, randomY, 0);
            GameObject damageText = Instantiate(damageTextPrefab, randomPosition, Quaternion.identity);
            damageText.GetComponent<DamageText>().Initialize(Mathf.RoundToInt(damage));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 leftBoundary = Quaternion.Euler(0, 0, -viewAngle / 2) * transform.up * detectionRadius;
        Vector3 rightBoundary = Quaternion.Euler(0, 0, viewAngle / 2) * transform.up * detectionRadius;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary);
    }
}
