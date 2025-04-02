using UnityEngine;

public class NewSkinShaderController : MonoBehaviour
{
    public Transform needleTransform;      // Obiekt igly
    public int needleHitState = 0;         // Zderzenie z colliderem
    public Material skinMaterial;          // Material skory (jeden wspolny)

    public float radius = 0.05f;            // Promien glownej deformacji

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Current Shader: " + skinMaterial.shader.name);

        if (needleTransform == null || skinMaterial == null) return;

        float offset = 0.003f;
        Vector3 entry = needleTransform.position - needleTransform.forward * offset;

        skinMaterial.SetFloat("_Radius", radius);
        skinMaterial.SetVector("_ImpactPoint", entry);

        //Needle state
        if (needleHitState == 0 && needleTransform.forward.y < 0)
        {
            skinMaterial.SetFloat("_NeedleState", 0);
            //Debug.Log("State: " + needleHitState);
        }
        if (needleHitState == 1 && needleTransform.forward.y > 0)
        {
            skinMaterial.SetFloat("_NeedleState", 1);
            //Debug.Log("State: " + needleHitState);
        }
        if (needleHitState == 2 && needleTransform.forward.x > 0)
        {
            skinMaterial.SetFloat("_NeedleState", 2);
            //Debug.Log("State: " + needleHitState);
        }
        if (needleHitState == 3 && needleTransform.forward.x < 0)
        {
            skinMaterial.SetFloat("_NeedleState", 3);
            //Debug.Log("State: " + needleHitState);
        }

        Debug.Log("Radius:" +radius);
        //Debug.Log("Entry:" + entry);
        Debug.Log("State:" + needleHitState);

    }
}
