using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private AudioClip[] hitSounds; 
    [SerializeField] private float hitSoundVolume = 1.0f; 

    private float currentHealth;
    private Animator animator;
    private PlayerAnimation playerAnimation;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    [SerializeField] private Color damageColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;
    private Coroutine flashCoroutine;
    private bool isFlashing = false;

    public delegate void OnPlayerDeath(); 
    public event OnPlayerDeath PlayerDied;

    private bool isDead = false;

    [SerializeField] private Slider healthBar;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>(); 
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        Debug.Log($"Игрок получил {damage} урона. Текущее здоровье: {currentHealth}");

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
        else
        {
            if (!isFlashing) 
            {
                flashCoroutine = StartCoroutine(FlashRed());
            }
            PlayHitSound();
        }
    }


    private void PlayHitSound()
    {
        if (hitSounds.Length > 0) 
        {
            int randomIndex = Random.Range(0, hitSounds.Length);
            audioSource.PlayOneShot(hitSounds[randomIndex], hitSoundVolume); 
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Игрок погиб");
        playerAnimation.PlayDeathAnimation();
        PlayerDied?.Invoke();
        DisablePlayerInteractions disableInteractions = GetComponent<DisablePlayerInteractions>();
        disableInteractions.DisableInteractions();
    }

    private IEnumerator FlashRed()
    {
        isFlashing = true; 
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(flashDuration);

        spriteRenderer.color = originalColor;
        isFlashing = false; 
    }
}
