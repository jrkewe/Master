using UnityEngine;

public class ColliderDetection : MonoBehaviour
{
    public NewSkinShaderController newSkinShader;

    private void OnTriggerEnter(Collider other)
    {
        int needleHitState = int.Parse(gameObject.tag);

        if (newSkinShader != null)
        {
            newSkinShader.needleHitState = needleHitState;
        }

    }
}
