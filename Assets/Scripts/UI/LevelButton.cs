using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        if (levelIndex > 1 && !LevelProgression.IsLevelCompleted(levelIndex))
        {
            button.interactable = false;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.gray;
        }

        button.onClick.AddListener(OnLevelButtonClicked);
    }

    private void OnLevelButtonClicked()
    {
        SceneManager.LoadScene(levelIndex);
    }
}
