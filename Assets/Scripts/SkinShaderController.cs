using UnityEngine;

public class SkinShaderController : MonoBehaviour
{
    public Transform needleTransform;     // Obiekt igly
    public Material skinMaterial;         // Jeden material dla wszystkiego

    public float radius = 0.1f;           // Zasieg glownego wgniotu
    public Transform entryPoint;
    public Transform exitPoint;

    public float deformationRadius = 0.1f;


    void Update()
    {
        // Wejscie igly (wgniot)
        skinMaterial.SetVector("_ImpactPointIn", entryPoint.position);
        skinMaterial.SetFloat("_RadiusIn", deformationRadius);

        // Wyjscie igly (wybrzuszenie)
        skinMaterial.SetVector("_ImpactPointOut", exitPoint.position);
        skinMaterial.SetFloat("_RadiusOut", deformationRadius);

        // Glowna deformacja igly (srodek)
        if (skinMaterial != null && needleTransform != null)
        {
            Vector3 impactPoint = needleTransform.position;
            skinMaterial.SetVector("_ImpactPoint", impactPoint);
            skinMaterial.SetFloat("_Radius", radius);
        }
    }
}
