using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
[RequireComponent(typeof(Rigidbody2D))] 
[RequireComponent(typeof(CapsuleCollider2D))]
public class Door : MonoBehaviour
{
    public float openForce = 5f; 
    private bool isOpen = false;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            isOpen = true; 
            playerMovement = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            isOpen = false; 
            playerMovement = null;
        }
    }

    private void FixedUpdate()
    {
        if (isOpen && playerMovement != null)
        {
            Vector2 playerMovementDirection = playerMovement.Movement;
            if (playerMovementDirection != Vector2.zero)
            {
                float rotationAmount = Vector2.SignedAngle(Vector2.right, playerMovementDirection);
                rb.MoveRotation(rb.rotation + rotationAmount * openForce * Time.fixedDeltaTime);
            }
        }
    }
}
