using UnityEngine;
using UnityEngine.SceneManagement;

public class GreenZone : MonoBehaviour
{
    [SerializeField] private GameObject winMenuUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement))
        {
            LevelProgression.SetLevelCompleted(SceneManager.GetActiveScene().buildIndex);
            ShowWinMenu();
        }
    }

    private void ShowWinMenu()
    {
        Time.timeScale = 0f;
        winMenuUI.SetActive(true);
    }
}
