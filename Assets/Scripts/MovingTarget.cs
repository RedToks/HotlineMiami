using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovingTarget : MonoBehaviour
{
    [SerializeField] private Transform pointA; 
    [SerializeField] private Transform pointB; 
    [SerializeField] private float speed = 2f; 
    private Vector3 targetPosition;
    private int health = 100000;

    [SerializeField] private GameObject damageTextPrefab;

    private void Start()
    {
        targetPosition = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Bullet playerBullet))
        {
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        ShowDamageText(damage);
    }

    private void ShowDamageText(float damage)
    {
        if (damageTextPrefab != null)
        {
            GameObject damageText = Instantiate(damageTextPrefab, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            damageText.GetComponent<DamageText>().Initialize(Mathf.RoundToInt(damage));
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(pointA.position, pointB.position); 
            Gizmos.DrawSphere(pointA.position, 0.1f); 
            Gizmos.DrawSphere(pointB.position, 0.1f); 
        }
    }
}
