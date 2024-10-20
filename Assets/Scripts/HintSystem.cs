using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintSystem : MonoBehaviour
{
    [SerializeField] private Canvas hintCanvas;
    [SerializeField] private TextMeshProUGUI hintText;
    [SerializeField] private Button understoodButton;

    private bool isHintActive = false;
    private bool hasHintBeenShown = false;

    private void Start()
    {
        hintCanvas.enabled = false;

        understoodButton.onClick.AddListener(CloseHint);
    }

    public void ShowHint()
    {
        Time.timeScale = 0f;

        hintCanvas.enabled = true;
        isHintActive = true;
    }

    private void CloseHint()
    {
        Time.timeScale = 1f;

        hintCanvas.enabled = false;
        isHintActive = false;

        Destroy(hintCanvas.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement playerMovement) && !isHintActive && !hasHintBeenShown)
        {
            ShowHint();
            hasHintBeenShown = true;
        }
    }
}
