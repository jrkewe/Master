using NUnit.Framework;
using UnityEngine;
using Master.SkinShader;
using System.Reflection;

public class SkinCollidersTests
{
    private GameObject skinColliderGO;
    private SkinColliders skinColliders;
    private SkinDeformer skinDeformer;

    [SetUp]
    public void Setup()
    {
        skinColliderGO = new GameObject("SkinCollider");
        skinColliders = skinColliderGO.AddComponent<SkinColliders>();

        GameObject skinGO = new GameObject("SkinDeformer");
        skinDeformer = skinGO.AddComponent<SkinDeformer>();

        skinColliders.GetType()
            .GetField("skinDeformer", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
            ?.SetValue(skinColliders, skinDeformer);

        skinColliderGO.tag = "1"; // symulujemy tag jako string "1"
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(skinColliderGO);
        Object.DestroyImmediate(skinDeformer.gameObject);
    }

    [Test]
    public void SkinColliders_SetsNeedleHitStateCorrectly()
    {
        Collider dummyCollider = skinColliderGO.AddComponent<BoxCollider>();
        skinColliders.SendMessage("OnTriggerEnter", dummyCollider);

        Assert.AreEqual(1, skinDeformer.needleHitState);
    }

    [Test]
    [TestCase(0, -1f, 0f, 0)] // Downward -> State 0
    [TestCase(1, 1f, 0f, 1)]  // Upward -> State 1
    [TestCase(2, 0f, 1f, 2)]  // Right  -> State 2
    [TestCase(3, 0f, -1f, 3)] // Left   -> State 3
    public void SkinDeformer_SetsNeedleStateCorrectly(int hitState, float forwardY, float forwardX, float expectedShaderValue)
    {
        // Arrange
        var skinGO = new GameObject("Skin");
        var renderer = skinGO.AddComponent<MeshRenderer>();
        var mat = new Material(Shader.Find("Standard"));
        renderer.sharedMaterial = mat;

        var deformer = skinGO.AddComponent<SkinDeformer>();

        var dummyTracker = new GameObject("DummyTracker").AddComponent<DummyNeedleTracker>();
        dummyTracker.NeedleTransform = new GameObject("Needle").transform;
        dummyTracker.NeedleTransform.forward = new Vector3(forwardX, forwardY, 0);

        typeof(SkinDeformer)
            .GetField("needleTrackerSource", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(deformer, dummyTracker);

        deformer.radius = 0.1f;
        deformer.offset = 0.01f;
        deformer.needleHitState = hitState;

        // Act
        deformer.ApplyDeformation();

        // Assert
        Assert.AreEqual(expectedShaderValue, mat.GetFloat("_NeedleState"));
    }
}
