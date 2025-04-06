using UnityEngine;

public class DummyNeedleTracker : MonoBehaviour, INeedleTracker
{
    public Transform NeedleTransform { get; set; }

    public Vector3 GetDirection()
    {
        return NeedleTransform != null ? NeedleTransform.forward : Vector3.forward;
    }

    public Vector3 GetTipPosition()
    {
        return NeedleTransform != null ? NeedleTransform.position : Vector3.zero;
    }
}