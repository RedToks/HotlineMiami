using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleMovementAnimation();
    }

    private void HandleMovementAnimation()
    {
        Vector2 movement = playerMovement.Movement;
        bool isMoving = movement.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("MeleeAttack");
    }

    public void PlayDeathAnimation()
    {
        animator.SetBool("isDead", true);
    }
}
