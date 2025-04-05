using UnityEngine;

public interface INeedleTracker
{
    Vector3 GetDirection();
    Vector3 GetTipPosition(); 
    Transform NeedleTransform { get; }
}
