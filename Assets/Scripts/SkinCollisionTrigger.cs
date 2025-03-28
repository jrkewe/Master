using UnityEngine;

public class SkinCollisionTrigger : MonoBehaviour
{
    public SkinShaderController skinShader;
    public Collider thisCollider;
    public Collider otherCollider;

    public enum TriggerType { Top, Bottom }
    public TriggerType triggerSide;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Zderzenie: Trigger={triggerSide}, Obiekt={other.name}");

        if (triggerSide == TriggerType.Top)
        {
            skinShader.needleState = SkinShaderController.NeedleState.Inserting;
            thisCollider.enabled = false;
            otherCollider.enabled = true;
            Debug.Log("Top -> wgniatanie");
        }
        else if (triggerSide == TriggerType.Bottom)
        {
            skinShader.needleState = SkinShaderController.NeedleState.Exiting;
            thisCollider.enabled = false;
            otherCollider.enabled = true;
            Debug.Log("Bottom -> wypychanie");
        }
    }
}