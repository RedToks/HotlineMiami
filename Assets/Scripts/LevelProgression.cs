using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelProgression
{
    private const string LevelKeyPrefix = "LevelCompleted_";

    public static void SetLevelCompleted(int levelIndex)
    {
        PlayerPrefs.SetInt(LevelKeyPrefix + levelIndex, 1);
        PlayerPrefs.Save();
    }

    public static bool IsLevelCompleted(int levelIndex)
    {
        return PlayerPrefs.GetInt(LevelKeyPrefix + levelIndex, 0) == 1;
    }

    public static void UnlockNextLevel(int levelIndex)
    {
        if (levelIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt(LevelKeyPrefix + (levelIndex + 1), 1);
            PlayerPrefs.Save();
        }
    }
}
