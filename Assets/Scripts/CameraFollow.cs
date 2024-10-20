using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float posY;
    private readonly float minY = int.MinValue; 
    private readonly float maxY = int.MaxValue;
    private readonly float posZ = -10;

    private Vector3 pos;



    private void Update()
    {
        pos = player.position;
        pos.z = posZ;
        pos.y = Mathf.Clamp(pos.y + posY, minY, maxY);
        transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * smoothSpeed);
    }
}
