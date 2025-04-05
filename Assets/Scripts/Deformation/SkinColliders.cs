using UnityEngine;

public class SkinColliders : MonoBehaviour
{
    [Header("Reference to skin deformation controller")]
    [SerializeField] private SkinDeformer skinDeformer;

    private void OnTriggerEnter(Collider other)
    {
        if (TryGetNeedleHitState(out int hitState))
        {
            UpdateNeedleHitState(hitState);
        }
        else
        {
            Debug.LogWarning($"SkinColliders: Invalid tag '{gameObject.tag}' – must be an integer (0–3).");
        }
    }

    private bool TryGetNeedleHitState(out int state)
    {
        return int.TryParse(gameObject.tag, out state);
    }

    private void UpdateNeedleHitState(int newState)
    {
        if (skinDeformer != null)
        {
            skinDeformer.SetHitState(newState);
            Debug.Log($"Needle hit state set to: {newState}");
        }
        else
        {
            Debug.LogWarning("SkinColliders: Missing reference to SkinDeformationController.");
        }
    }
}
