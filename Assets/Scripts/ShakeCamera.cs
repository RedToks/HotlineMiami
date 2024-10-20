using System.Collections;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private float shakeDuration = 0.1f;
    [SerializeField] private float shakeMagnitude = 0.1f;

    private Vector3 originalOffset;
    private CameraFollow cameraFollow; 

    private void Start()
    {
        mainCamera = Camera.main;

        originalOffset = mainCamera.transform.localPosition - FindObjectOfType<PlayerAttack>().transform.position;

        cameraFollow = mainCamera.GetComponent<CameraFollow>();

        PlayerAttack playerAttack = FindObjectOfType<PlayerAttack>();

    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            Vector3 shakeOffset = new Vector3(x, y, 0);

            mainCamera.transform.localPosition = cameraFollow.transform.position + originalOffset + shakeOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }


        mainCamera.transform.localPosition = cameraFollow.transform.position + originalOffset;
    }
}
