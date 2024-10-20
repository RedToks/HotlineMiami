using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearSpeed = 2f;
    private float moveSpeed = 1f;
    private Color textColor;

    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
    }

    public void Initialize(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
    }

    private void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        textColor.a -= disappearSpeed * Time.deltaTime;
        textMesh.color = textColor;

        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
