using UnityEngine;

public class InjectionPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Needle"))
        {
            Debug.Log("Trafiono punkt wklucia!");
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
