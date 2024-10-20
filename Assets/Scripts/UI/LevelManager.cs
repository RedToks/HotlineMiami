using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Enemy[] enemies; 
    public GameObject victoryMenu;

    private void Start()
    {
        Time.timeScale = 1f;
        enemies = FindObjectsOfType<Enemy>();

        victoryMenu.SetActive(false);
    }

    private void Update()
    {
        CheckForVictory();
    }

    private void CheckForVictory()
    {
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy.IsAlive) 
            {
                return; 
            }
        }

        Victory();
    }


    private void Victory()
    {
        Time.timeScale = 0;
        LevelProgression.SetLevelCompleted(SceneManager.GetActiveScene().buildIndex);
        LevelProgression.UnlockNextLevel(SceneManager.GetActiveScene().buildIndex);
        victoryMenu.SetActive(true);
    }
}
