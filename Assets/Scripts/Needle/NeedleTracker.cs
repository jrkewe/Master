using UnityEngine;

[RequireComponent(typeof(Transform))]
public class NeedleTracker : MonoBehaviour, INeedleTracker
{
    [SerializeField] private Transform needleTransform;

    public Transform NeedleTransform => needleTransform;

    public Vector3 GetDirection()
    {
        return needleTransform != null ? needleTransform.forward : Vector3.forward;
    }

    public Vector3 GetTipPosition()
    {
        return needleTransform != null ? needleTransform.position : Vector3.zero;
    }
}