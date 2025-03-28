using UnityEngine;

public class BottomTrigger : MonoBehaviour
{
    public SkinShaderController skinShader;
    public Collider thisCollider;
    public Collider topTrigger;

    private void OnTriggerEnter(Collider other)
    {
            skinShader.needleState = SkinShaderController.NeedleState.Exiting;

            thisCollider.enabled = false;
            topTrigger.enabled = true;

            Debug.Log("BottomTrigger -> wypychanie");
        
    }
}
