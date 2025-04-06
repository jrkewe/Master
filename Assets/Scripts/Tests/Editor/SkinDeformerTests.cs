using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using Master.SkinShader;

public class SkinDeformerTests
{
    private GameObject skinObject;
    private SkinDeformer deformer;
    private Material testMaterial;
    private DummyNeedleTracker dummyTracker;

    [SetUp]
    public void SetUp()
    {
        // Create skin object with renderer and material
        skinObject = new GameObject("Skin");
        var renderer = skinObject.AddComponent<MeshRenderer>();
        testMaterial = new Material(Shader.Find("Standard"));
        renderer.sharedMaterial = testMaterial;

        // Add SkinDeformer component
        deformer = skinObject.AddComponent<SkinDeformer>();

        // Create dummy needle and tracker
        var needleObj = new GameObject("Needle");
        dummyTracker = needleObj.AddComponent<DummyNeedleTracker>();
        dummyTracker.NeedleTransform = needleObj.transform;

        // Inject tracker into private field
        var trackerField = typeof(SkinDeformer).GetField("needleTrackerSource", BindingFlags.NonPublic | BindingFlags.Instance);
        trackerField?.SetValue(deformer, dummyTracker);

        // Assign public parameters
        deformer.radius = 0.1f;
        deformer.offset = 0.01f;
        deformer.skinMaterial = testMaterial;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(skinObject);
        Object.DestroyImmediate(dummyTracker.gameObject);
    }

    [Test]
    public void ApplyDeformation_SetsCorrectShaderParameters()
    {
        // Set needle position and direction
        dummyTracker.NeedleTransform.position = new Vector3(1f, 2f, 3f);
        dummyTracker.NeedleTransform.forward = Vector3.down;

        // Manual injection for test context
        typeof(SkinDeformer)
            .GetField("needleTracker", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(deformer, dummyTracker);

        deformer.needleHitState = 0;
        deformer.ApplyDeformation();

        Assert.AreEqual(0.1f, testMaterial.GetFloat("_Radius"));
        Assert.AreEqual(new Vector4(1f, 2.01f, 3f, 0f), testMaterial.GetVector("_ImpactPoint"));
        Assert.AreEqual(0f, testMaterial.GetFloat("_NeedleState"));
    }

    [TestCase(0, 0f, -1f, 0f, 0f)] // Down
    [TestCase(1, 0f, 1f, 0f, 1f)]  // Up
    [TestCase(2, 1f, 0f, 0f, 2f)]  // Right
    [TestCase(3, -1f, 0f, 0f, 3f)] // Left
    public void SkinDeformer_SetsNeedleStateCorrectly(int state, float x, float y, float z, float expected)
    {
        // Set up direction and state
        dummyTracker.NeedleTransform.forward = new Vector3(x, y, z).normalized;
        deformer.needleHitState = state;

        // Manual injection for test context
        typeof(SkinDeformer)
            .GetField("needleTracker", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(deformer, dummyTracker);

        // Act
        deformer.ApplyDeformation();

        // Assert
        Assert.AreEqual(expected, testMaterial.GetFloat("_NeedleState"));
    }
}
