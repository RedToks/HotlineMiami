using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class DiscoBallLights : MonoBehaviour
{
    public Light2D[] lights; 
    public Color[] colors;   
    public float colorChangeDuration = 1f; 

    private void Awake()
    {
        foreach (Light2D light in lights)
        {
            StartCoroutine(SmoothChangeLightColor(light)); 
        }
    }

    private IEnumerator SmoothChangeLightColor(Light2D light)
    {
        while (true)
        {
            Color startColor = light.color;
            Color targetColor = colors[Random.Range(0, colors.Length)];
            targetColor.a = 1f; 

            float elapsedTime = 0f;
            while (elapsedTime < colorChangeDuration)
            {
                light.color = Color.Lerp(startColor, targetColor, elapsedTime / colorChangeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            light.color = targetColor;
            light.color = new Color(light.color.r, light.color.g, light.color.b, 1f); 
            yield return new WaitForSeconds(0.5f);
        }
    }
}
