using UnityEngine;

public class SkinDeformer : MonoBehaviour
{
    [Header("Deformation Parameters")]
    [SerializeField] private Material skinMaterial;
    [SerializeField] private float radius = 0.0045f;
    [SerializeField] private float offset = 0.003f;

    [Header("Needle Tracking")]
    [SerializeField] private MonoBehaviour needleTrackerSource; // przypisujemy NeedleTracker
    private INeedleTracker needleTracker;

    [SerializeField] private int needleHitState = 0;

    private void Awake()
    {
        needleTracker = needleTrackerSource as INeedleTracker;
        if (needleTracker == null)
        {
            Debug.LogError("SkinDeformer: Missing or invalid needle tracker.");
        }
    }

    private void Update()
    {
        if (needleTracker == null || skinMaterial == null) return;

        Vector3 direction = needleTracker.GetDirection();
        Vector3 tip = needleTracker.GetTipPosition();
        Vector3 impactPoint = tip - direction * offset;

        skinMaterial.SetFloat("_Radius", radius);
        skinMaterial.SetVector("_ImpactPoint", impactPoint);

        if (needleHitState == 0 && direction.y < 0)
            skinMaterial.SetFloat("_NeedleState", 0);
        else if (needleHitState == 1 && direction.y > 0)
            skinMaterial.SetFloat("_NeedleState", 1);
        else if (needleHitState == 2 && direction.x > 0)
            skinMaterial.SetFloat("_NeedleState", 2);
        else if (needleHitState == 3 && direction.x < 0)
            skinMaterial.SetFloat("_NeedleState", 3);
    }

    public void SetHitState(int newState)
    {
        needleHitState = newState;
    }
}
