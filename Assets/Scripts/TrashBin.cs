using UnityEngine;

public class TrashBin : MonoBehaviour
{
    [SerializeField] private GameObject hiddenIconPrefab;
    [SerializeField] private float hiddenIconPosition;

    private GameObject player;
    private GameObject hiddenIconInstance;

    public bool IsPlayerHidden { get; private set; } = false;
    private bool isPlayerInTrigger = false;

    private KeyCode interactKey = KeyCode.F;

    private void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(interactKey))
        {
            HidePlayer();
        }
        else if (IsPlayerHidden && Input.GetKeyDown(interactKey)) 
        {
            RevealPlayer();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            player = other.gameObject; 
            isPlayerInTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            isPlayerInTrigger = false; 
        }
    }

    private void HidePlayer()
    {
        if (player != null) 
        {
            IsPlayerHidden = true;
            player.transform.position = transform.position; 
            player.SetActive(false);
        }

        hiddenIconInstance = Instantiate(hiddenIconPrefab, transform.position + Vector3.up * hiddenIconPosition, Quaternion.identity);
    }

    private void RevealPlayer()
    {
        if (player != null) 
        {
            IsPlayerHidden = false;
            player.SetActive(true); 
            player.transform.position = transform.position;
            Destroy(hiddenIconInstance);
        }
    }
}
