using NUnit.Framework;
using UnityEngine;

public class NeedleTrackerTests
{
    [Test]
    public void NeedleForward_ShouldPointDown_WhenRotatedProperly()
    {
        GameObject needle = new GameObject("Needle");
        needle.transform.rotation = Quaternion.Euler(90, 0, 0);

        Vector3 forward = needle.transform.forward;

        Assert.Less(forward.y, 0); // np. powinno byc skierowane w dol
    }
}
