using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject deathMenu;  
    [SerializeField] private float deathMenuDelay = 2f;  

    private void Start()
    {
        deathMenu.SetActive(false); 
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.PlayerDied += OnPlayerDied;
        }
    }

    private void OnPlayerDied()
    {
        StartCoroutine(ShowDeathMenuWithDelay()); 
    }

    private IEnumerator ShowDeathMenuWithDelay()
    {
        yield return new WaitForSeconds(deathMenuDelay); 
        deathMenu.SetActive(true);  
        Time.timeScale = 0f;  
    }
}
