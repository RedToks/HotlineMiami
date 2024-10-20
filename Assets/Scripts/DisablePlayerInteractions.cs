using UnityEngine;

public class DisablePlayerInteractions : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CapsuleCollider2D capsuleCollider;
    private PlayerRotation playerRotation;
    private PlayerAnimation playerAnimation;
    private PlayerAttack playerAttack;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        playerRotation = GetComponent<PlayerRotation>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerAttack = GetComponent<PlayerAttack>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void DisableInteractions()
    {
        playerMovement.enabled = false;
        capsuleCollider.enabled = false;
        playerRotation.enabled = false;
        playerAnimation.enabled = false; 
        playerAttack.enabled = false;
        playerHealth.enabled = false; 
    }
}
