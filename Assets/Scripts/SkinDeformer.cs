using UnityEngine;

public class SkinDeformer : MonoBehaviour
{
    [Header("References")]
    public Transform needleTransform;
    public Material skinMaterial;

    [Header("Deformation Settings")]
    public float radius = 0.0045f;
    public float offset = 0.003f;

    [Header("Needle State")]
    public int needleHitState = 0;

    void Update()
    {
        if (!IsValid()) return;

        Vector3 needleTip = GetNeedleTip();
        Vector3 impactPoint = GetImpactPoint(needleTip);

        ApplyShaderParameters(impactPoint);
        UpdateNeedleState();

        DebugLog(impactPoint);
    }

    bool IsValid()
    {
        return needleTransform != null && skinMaterial != null;
    }

    Vector3 GetNeedleTip()
    {
        return needleTransform.position;
    }

    Vector3 GetImpactPoint(Vector3 needleTip)
    {
        return needleTip - needleTransform.forward * offset;
    }

    void ApplyShaderParameters(Vector3 impactPoint)
    {
        skinMaterial.SetFloat("_Radius", radius);
        skinMaterial.SetVector("_ImpactPoint", impactPoint);
    }

    void UpdateNeedleState()
    {
        Vector3 dir = needleTransform.forward;

        if (needleHitState == 0 && dir.y < 0)
            skinMaterial.SetFloat("_NeedleState", 0);
        else if (needleHitState == 1 && dir.y > 0)
            skinMaterial.SetFloat("_NeedleState", 1);
        else if (needleHitState == 2 && dir.x > 0)
            skinMaterial.SetFloat("_NeedleState", 2);
        else if (needleHitState == 3 && dir.x < 0)
            skinMaterial.SetFloat("_NeedleState", 3);
    }

    void DebugLog(Vector3 impactPoint)
    {
        Debug.Log($"Shader: {skinMaterial.shader.name}");
        Debug.Log($"Radius: {radius}");
        Debug.Log($"Impact Point: {impactPoint}");
        Debug.Log($"Needle State: {needleHitState}");
    }
}
