using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class InjectionPointMarkerTests
{
    private GameObject point;
    private InjectionPointMarker marker;
    private Renderer renderer;


    [SetUp]
    public void SetUp()
    {
        point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        point.tag = "Untagged";

        marker = point.AddComponent<InjectionPointMarker>();
        renderer = point.GetComponent<Renderer>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(point);
    }

    [Test]
    public void OnNeedleHit_ChangesColorToHitColor()
    {
        var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        var marker = go.AddComponent<InjectionPointMarker>();
        marker.hitColor = Color.green;

        var renderer = go.GetComponent<Renderer>();
        renderer.sharedMaterial = new Material(Shader.Find("Standard"));
        renderer.sharedMaterial.color = Color.red;

        typeof(InjectionPointMarker)
            .GetField("pointRenderer", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(marker, renderer);

        // Act
        var method = typeof(InjectionPointMarker)
                     .GetMethod("OnNeedleHit", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        method.Invoke(marker, null);

        // Assert
        Assert.AreEqual(Color.green, renderer.sharedMaterial.color);

        Object.DestroyImmediate(go);
    }

    [Test]
    public void IsNeedle_ReturnsTrue_WhenTagIsNeedle()
    {
        GameObject needle = new GameObject();
        needle.tag = "Needle";
        Collider dummyCollider = needle.AddComponent<BoxCollider>();

        Assert.IsTrue(marker.GetType()
            .GetMethod("IsNeedle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(marker, new object[] { dummyCollider }) as bool? ?? false);

        Object.DestroyImmediate(needle);
    }

    [Test]
    public void IsNeedle_ReturnsFalse_WhenTagIsNotNeedle()
    {
        GameObject notNeedle = new GameObject();
        notNeedle.tag = "Untagged";
        Collider dummyCollider = notNeedle.AddComponent<BoxCollider>();

        Assert.IsFalse(marker.GetType()
            .GetMethod("IsNeedle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.Invoke(marker, new object[] { dummyCollider }) as bool? ?? true);

        Object.DestroyImmediate(notNeedle);
    }
}
