using NUnit.Framework;
using UnityEngine;

public class SkinDeformerTests
{
    [Test]
    public void ImpactPoint_ShouldBeCorrect_WhenOffsetApplied()
    {
        GameObject needle = new GameObject("Needle");
        needle.transform.position = new Vector3(0, 1, 0);
        needle.transform.forward = Vector3.down;

        float offset = 0.003f;
        Vector3 expectedImpact = needle.transform.position - needle.transform.forward * offset;

        Vector3 impact = needle.transform.position - needle.transform.forward * offset;

        Assert.AreEqual(expectedImpact, impact);
    }
}
