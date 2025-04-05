using UnityEngine;

/// <summary>
/// Interface for applying deformation effects to a mesh or material.
/// </summary>
public interface IDeformable
{
    void ApplyDeformation(Vector3 impactPoint, float radius, int direction);
}