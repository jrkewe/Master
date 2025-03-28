using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopTrigger : MonoBehaviour
{
    public SkinShaderController skinShader;
    public Collider thisCollider;
    public Collider bottomTrigger;

    private void OnTriggerEnter(Collider other)
    {
        
            skinShader.needleState = SkinShaderController.NeedleState.Inserting;

            thisCollider.enabled = false;
            bottomTrigger.enabled = true;

            Debug.Log("TopTrigger -> wgniatanie");
        
    }
}
