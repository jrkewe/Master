using UnityEngine;

public class SkinShaderController : MonoBehaviour
{
    public Transform needleTransform;     // Obiekt igly
    public Material skinMaterial;         // Jeden material dla wszystkiego

    public float radius = 0.1f;           // Zasieg glownego wgniotu
    public Transform entryPoint;
    public Transform exitPoint;

    public float deformationRadius = 0.1f;

    public enum NeedleState { Inserting, Exiting }
    public NeedleState needleState = NeedleState.Inserting;


    void Update()
    {
        skinMaterial.SetFloat("_NeedleState", needleState == NeedleState.Inserting ? 0f : 1f);



        if (needleTransform != null && skinMaterial != null)
        {
            Vector3 needleDir = needleTransform.forward; // kierunek igly
            Vector3 normal = Vector3.up; // zakladamy ze plane ma normalna do gory

            float dot = Vector3.Dot(needleDir, normal);

            if (dot < 0f)
                Debug.Log("Igla wbija sie od gory (Inserting)");
            else
                Debug.Log("Igla wychodzi od spodu (Exiting)");

            // Mozesz nadal ustawiac needleState recznie w Inspectorze do testow

            // To tylko wysylanie do shader'a
            skinMaterial.SetFloat("_NeedleState", needleState == NeedleState.Inserting ? 0f : 1f);

            ///////////////////////
            Vector3 needleTip = needleTransform.position;

            float offset = 0.01f; // dlugosc od wejscia do wyjscia (mozesz dopasowac)
            Vector3 entry = needleTip - needleDir * offset;
            Vector3 exit = needleTip + needleDir * offset;

            // Przekazanie danych do shader'a
            skinMaterial.SetVector("_ImpactPoint", needleTip);
            skinMaterial.SetFloat("_Radius", radius);

            skinMaterial.SetVector("_ImpactPointIn", entry);
            skinMaterial.SetFloat("_RadiusIn", deformationRadius);

            skinMaterial.SetVector("_ImpactPointOut", exit);
            skinMaterial.SetFloat("_RadiusOut", deformationRadius);
        }
    }
}
