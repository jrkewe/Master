using UnityEngine;

public class SkinShaderController : MonoBehaviour
{
    public Transform needleTransform;      // Obiekt igly
    public int needleHitState;             // Zderzenie z colliderem
    public Material skinMaterial;          // Material skory (jeden wspolny)

    public float radius = 0.1f;            // Promien glownej deformacji
    public float deformationRadius = 0.1f; // Promien deformacji na wejsciu/wyjsciu

    public enum NeedleState { Inserting, Exiting }
    public NeedleState needleState = NeedleState.Inserting;

    public Transform entryPoint;           // (Opcjonalnie) punkty referencyjne
    public Transform exitPoint;

    public float offset = 0.01f;           // Przesuniecie od czubka igly

    void Update()
    {
        if (needleTransform == null || skinMaterial == null) return;

        // Ustal kierunek igly
        Vector3 needleDir = needleTransform.forward;

        // Wyznacz pozycje czubka igly + wejscie/wyjscie
        Vector3 needleTip = needleTransform.position;
        Vector3 entry = needleTip - needleDir * offset;
        Vector3 exit = needleTip + needleDir * offset;

        // Debug: pokaz punkt deformacji (czubek igly)
        Debug.DrawRay(needleTip, Vector3.up * 0.02f, Color.red);

        // Automatyczne rozpoznanie kierunku - opcjonalnie
        float dot = Vector3.Dot(needleDir, Vector3.up);
        if (dot < 0f)
            Debug.Log("Igla wbija sie od gory (Inserting)");
        else
            Debug.Log("Igla wychodzi od spodu (Exiting)");


        //Debug.Log("State: " + needleHitState);
        if (needleHitState==0 && needleTransform.forward.y < 0) {
            skinMaterial.SetFloat("_NeedleState", 0);
            Debug.Log("State: " + needleHitState);
        }
        if (needleHitState == 1 && needleTransform.forward.y > 0)
        {
            skinMaterial.SetFloat("_NeedleState", 1);
            Debug.Log("State: " + needleHitState);
        }
        if (needleHitState == 2 && needleTransform.forward.x > 0)
        {
            skinMaterial.SetFloat("_NeedleState", 2);
            Debug.Log("State: " + needleHitState);
        }
        if (needleHitState == 3 && needleTransform.forward.x < 0)
        {
            skinMaterial.SetFloat("_NeedleState", 3);
            Debug.Log("State: " + needleHitState);
        }
        //skinMaterial.SetFloat("_NeedleState", needleState == NeedleState.Inserting ? 0f : 1f);
        skinMaterial.SetVector("_ImpactPoint", needleTip);
        skinMaterial.SetFloat("_Radius", radius);
        skinMaterial.SetVector("_ImpactPointIn", entry);
        skinMaterial.SetFloat("_RadiusIn", deformationRadius);
        skinMaterial.SetVector("_ImpactPointOut", exit);
        skinMaterial.SetFloat("_RadiusOut", deformationRadius);

        skinMaterial.SetVector("_NeedleDirection", needleTransform.up);
        //Debug.Log("FORWARD: " + needleTransform.forward);
        //Debug.Log("UP: " + needleTransform.up);
        //Debug.Log("RIGHT: " + needleTransform.right);
    }
}
