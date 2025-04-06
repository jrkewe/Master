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

        typeof(SkinColliders)
            .GetField("skinDeformer", BindingFlags.Public | BindingFlags.Instance)
            ?.SetValue(skinColliders, skinDeformer);

        skinColliderGO.tag = "1";
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
        skinDeformer.SetHitState(1);
        Assert.AreEqual(1, skinDeformer.needleHitState);
    }

    [Test]
    [TestCase(0,-1, 0, 0)] // down
    [TestCase(1,1,0, 0)] // up
    [TestCase(2,0, -1,0)] // right
    [TestCase(3,0, 1, 0)] // left
    public void SkinDeformer_SetsNeedleStateCorrectly(int hitState, float forwardY, float forwardX, float expectedShaderValue)
    {
        var skinGO = new GameObject("Skin");
        var renderer = skinGO.AddComponent<MeshRenderer>();

        var mat = new Material(Shader.Find("Shader Graphs/SkinElasticShader")); // your custom shader
        renderer.sharedMaterial = mat;

        var deformer = skinGO.AddComponent<SkinDeformer>();

        var needle = new GameObject("Needle").transform;
        needle.forward = new Vector3(forwardX, forwardY, 0);

        var dummyTracker = needle.gameObject.AddComponent<DummyNeedleTracker>();
        dummyTracker.NeedleTransform = needle;

        typeof(SkinDeformer)
            .GetField("needleTrackerSource", BindingFlags.NonPublic | BindingFlags.Instance)
            ?.SetValue(deformer, dummyTracker);

        deformer.skinMaterial = mat;
        deformer.radius = 0.1f;
        deformer.offset = 0.01f;
        deformer.needleHitState = hitState;

        deformer.ApplyDeformation();

        Assert.AreEqual(expectedShaderValue, mat.GetFloat("_NeedleState"));
    }
}
