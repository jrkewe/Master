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
        [SerializeField] private MonoBehaviour needleTrackerSource;
        private INeedleTracker needleTracker;

        [SerializeField] public int needleHitState = 0;

        private bool IsValid()
        {
            bool valid = needleTracker != null && skinMaterial != null;
            Debug.Log($"IsValid: {valid}, Tracker: {needleTracker}, Material: {skinMaterial}");
            return valid;
        }

        private void Awake()
        {
            needleTracker = needleTrackerSource as INeedleTracker;
            if (needleTracker == null)
            {
                Debug.LogError("SkinDeformer: Missing or invalid needle tracker.");
            }
        }

        private void Start()
        {
            if (skinMaterial == null)
                skinMaterial = GetComponent<Renderer>().material;

            Debug.Log("Material at runtime: " + skinMaterial.shader.name);

            if (needleTrackerSource is INeedleTracker tracker)
            {
                needleTracker = tracker;
                Debug.Log("Needle tracker assigned successfully.");
            }
            else
            {
                Debug.LogWarning("needleTrackerSource does not implement INeedleTracker!");
            }
        }

        private void Update()
        {
            if (!IsValid()) return;

            Transform needle = needleTracker.NeedleTransform;

            Vector3 impactPoint = needle.position - needle.forward * offset;
            skinMaterial.SetFloat("_Radius", radius);
            skinMaterial.SetVector("_ImpactPoint", impactPoint);

            Debug.Log("Impact Point: " + impactPoint);
            Debug.Log("Needle Forward: " + needle.forward);

            UpdateNeedleState(needle.forward);
        }

        private void UpdateNeedleState(Vector3 dir)
        {
            if (needleHitState == 0 && dir.y < 0)
                skinMaterial.SetFloat("_NeedleState", 0);
            else if (needleHitState == 1 && dir.y > 0)
                skinMaterial.SetFloat("_NeedleState", 1);
            else if (needleHitState == 2 && dir.x > 0)
                skinMaterial.SetFloat("_NeedleState", 2);
            else if (needleHitState == 3 && dir.x < 0)
                skinMaterial.SetFloat("_NeedleState", 3);
        }

        public void SetHitState(int newState) => needleHitState = newState;

        public void ApplyDeformation()
        {
            if (!IsValid()) return;

            Transform needle = needleTracker.NeedleTransform;
            Vector3 impactPoint = needle.position - needle.forward * offset;

            skinMaterial.SetFloat("_Radius", radius);
            skinMaterial.SetVector("_ImpactPoint", impactPoint);
            UpdateNeedleState(needle.forward);

            Debug.Log("Deformation applied");
        }
    }
}
