using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AudioClip[] walkingSounds;
    [SerializeField] private float walkingSoundInterval = 0.5f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private AudioSource audioSource;
    private float nextWalkingSoundTime;
    private bool isWalking;

    public Vector2 Movement => movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;

        if (movement.magnitude > 0)
        {
            if (!isWalking)
            {
                PlayRandomWalkingSound();
                isWalking = true;
            }
            if (Time.time >= nextWalkingSoundTime)
            {
                PlayRandomWalkingSound();
                nextWalkingSoundTime = Time.time + walkingSoundInterval;
            }
        }
        else
        {
            isWalking = false;
        }
    }

    private void PlayRandomWalkingSound()
    {
        if (walkingSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, walkingSounds.Length);
            audioSource.PlayOneShot(walkingSounds[randomIndex]);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}