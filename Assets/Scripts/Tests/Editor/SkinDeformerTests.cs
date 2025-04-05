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

    [Test]
    public void SkinDeformer_UpdatesNeedleStateCorrectly()
    {
        // Arrange
        var skin = new GameObject("Skin");
        var renderer = skin.AddComponent<MeshRenderer>();
        var mat = new Material(Shader.Find("Standard"));
        renderer.sharedMaterial = mat;

        var deformer = skin.AddComponent<SkinDeformer>();

        var needle = new GameObject("Needle").transform;
        var dummyTracker = new GameObject("DummyTracker").AddComponent<DummyNeedleTracker>();
        dummyTracker.NeedleTransform = needle;

        deformer.GetType()
            .GetField("needleTrackerSource", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(deformer, dummyTracker);

        deformer.radius = 0.1f;
        deformer.offset = 0.01f;
        deformer.needleHitState = 0;

        // Needle points downward (Y-)
        needle.forward = Vector3.down;

        // Act
        deformer.ApplyDeformation();

        // Assert
        Assert.AreEqual(0f, mat.GetFloat("_NeedleState"));
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

        deformer.needleHitState = 0;
        deformer.ApplyDeformation();

        // Check if shader parameters are correctly set
        Assert.AreEqual(0.1f, testMaterial.GetFloat("_Radius"));
        Assert.AreEqual(new Vector4(1f, 2.01f, 3f, 0f), testMaterial.GetVector("_ImpactPoint"));
        Assert.AreEqual(0f, testMaterial.GetFloat("_NeedleState"));
    }
}
