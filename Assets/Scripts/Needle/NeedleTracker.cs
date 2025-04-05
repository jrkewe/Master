using UnityEngine;

[RequireComponent(typeof(Transform))]
public class NeedleTracker : MonoBehaviour, INeedleTracker
{
    public Vector3 GetDirection()
    {
        return transform.forward;
    }

    public Vector3 GetTipPosition()
    {
        return transform.position;
    }
}