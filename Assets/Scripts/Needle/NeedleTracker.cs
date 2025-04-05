using UnityEngine;

[DisallowMultipleComponent]
public class NeedleTracker : MonoBehaviour, INeedleTracker
{
    [Tooltip("Optional: Assign a separate transform. If left empty, this object's own transform will be used.")]
    [SerializeField] private Transform needleTransform;

    public Transform NeedleTransform => needleTransform != null ? needleTransform : transform;

    public Vector3 GetDirection()
    {
        return NeedleTransform.forward;
    }

    public Vector3 GetTipPosition()
    {
        return NeedleTransform.position;
    }
}
