using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public SkinShaderController skinShader;

    private void OnTriggerEnter(Collider other)
    {
        int needleHitState = int.Parse(gameObject.tag);

        if (skinShader != null)
        {
            skinShader.needleHitState = needleHitState;
        }

    }
}
