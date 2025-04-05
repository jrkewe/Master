using UnityEngine;
namespace Master.SkinShader
{
    public class SkinDeformer : MonoBehaviour
    {
        [Header("Deformation Parameters")]
        [SerializeField] public Material skinMaterial;
        [SerializeField] public float radius = 0.0045f;
        [SerializeField] public float offset = 0.003f;

        [Header("Needle Tracking")]
        [SerializeField] private MonoBehaviour needleTrackerSource; // przypisujemy NeedleTracker
        private INeedleTracker needleTracker;

        [SerializeField] public int needleHitState = 0;

        private bool IsValid()
        {
            return needleTracker != null && skinMaterial != null;
        }

        private Vector3 GetNeedleTip()
        {
            return needleTracker.NeedleTransform.position;
        }

        private Vector3 GetImpactPoint(Vector3 needleTip)
        {
            return needleTip - needleTracker.NeedleTransform.forward * offset;
        }

        private void ApplyShaderParameters(Vector3 impactPoint)
        {
            skinMaterial.SetFloat("_Radius", radius);
            skinMaterial.SetVector("_ImpactPoint", impactPoint);
        }

        private void UpdateNeedleState()
        {
            var dir = needleTracker.NeedleTransform.forward;

            if (needleHitState == 0 && dir.y < 0)
                skinMaterial.SetFloat("_NeedleState", 0);
            else if (needleHitState == 1 && dir.y > 0)
                skinMaterial.SetFloat("_NeedleState", 1);
            else if (needleHitState == 2 && dir.x > 0)
                skinMaterial.SetFloat("_NeedleState", 2);
            else if (needleHitState == 3 && dir.x < 0)
                skinMaterial.SetFloat("_NeedleState", 3);
        }

        private void Awake()
        {
            needleTracker = needleTrackerSource as INeedleTracker;
            if (needleTracker == null)
            {
                Debug.LogError("SkinDeformer: Missing or invalid needle tracker.");
            }
        }

        private void Update()
        {
            if (needleTracker == null || skinMaterial == null) return;

            Vector3 direction = needleTracker.GetDirection();
            Vector3 tip = needleTracker.GetTipPosition();
            Vector3 impactPoint = tip - direction * offset;

            skinMaterial.SetFloat("_Radius", radius);
            skinMaterial.SetVector("_ImpactPoint", impactPoint);

            UpdateNeedleState();
        }

        public void SetHitState(int newState)
        {
            needleHitState = newState;
        }

        public void ApplyDeformation()
        {
            if (!IsValid()) return;

            Vector3 needleTip = GetNeedleTip();
            Vector3 impactPoint = GetImpactPoint(needleTip);

            ApplyShaderParameters(impactPoint);
            UpdateNeedleState();
        }

    }
}