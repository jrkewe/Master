using UnityEngine;

public class NeedleDetector : MonoBehaviour
{
    private string lastHit = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("We"))
        {
            lastHit = other.name;
            Debug.Log("Wklucie w punkt: " + lastHit);
            other.GetComponent<Renderer>().material.color = Color.green;
        }

        if (other.name.Contains("Wy") && lastHit.Replace("We", "") == other.name.Replace("Wy", ""))
        {
            Debug.Log("Wyszycie przez pare: " + lastHit + " -> " + other.name);
            other.GetComponent<Renderer>().material.color = Color.green;

            // TODO: zarejestrowac szycie, punktacja, linia itd.
        }
    }
}
