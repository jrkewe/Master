using UnityEngine;

public class InjectionPointMarker : MonoBehaviour
{
    [Header("Color on needle hit")]
    public Color hitColor = Color.green;

    private Renderer pointRenderer;

    void Start()
    {
        pointRenderer = GetComponent<Renderer>();
        if (pointRenderer == null)
        {
            Debug.LogWarning("InjectionPoint: Missing Renderer component.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsNeedle(other))
        {
            OnNeedleHit();
        }
    }

    bool IsNeedle(Collider other)
    {
        return other.CompareTag("Needle");
    }

    void OnNeedleHit()
    {
        Debug.Log("Needle hit the injection point!");
        if (pointRenderer != null)
        {
            pointRenderer.material.color = hitColor;
        }
    }
}